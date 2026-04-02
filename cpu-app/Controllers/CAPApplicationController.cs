using Database.Model;
using Gov.Cscp.Victims.Public.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    public class CAPApplicationController : Controller
    {
        private readonly IOrganizationServiceAsync _organizationService;
        private readonly ILogger _logger;

        public CAPApplicationController(IOrganizationServiceAsync organizationService)
        {
            _organizationService = organizationService;
            _logger = Log.Logger;
        }

        [HttpGet("{businessBceid}/{userBceid}/{scheduleFId}")]
        [ProducesResponseType(typeof(CAPApplicationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CAPApplicationDto>> GetCAPApplication(
            string businessBceid, string userBceid, string scheduleFId)
        {
            if (string.IsNullOrWhiteSpace(businessBceid))
                return BadRequest("businessBceid parameter is required.");
            if (string.IsNullOrWhiteSpace(userBceid))
                return BadRequest("userBceid parameter is required.");
            if (!Guid.TryParse(scheduleFId, out var contractGuid))
                return BadRequest("scheduleFId must be a valid GUID.");

            try
            {
                var request = new Vsd_GetCpuScheduleF_CapRequest
                {
                    Target = new EntityReference("vsd_contract", contractGuid),
                    UserBcEId = userBceid,
                    BusinessBcEId = businessBceid
                };

                var response = (Vsd_GetCpuScheduleF_CapResponse)
                    await _organizationService.ExecuteAsync(request);

                var dto = new CAPApplicationDto
                {
                    IsSuccess = response.IsSuccess,
                    Result = response.Result ?? string.Empty,
                    Businessbceid = response.BusinessBcEId,
                    Userbceid = response.UserBcEId,
                    Contract = EntityToDtoMapper.ToContractDto(response.Contract),
                    Organization = EntityToDtoMapper.ToOrganizationDto(response.Organization),
                    BoardContact = EntityToDtoMapper.ToContactDto(response.BoardContact),
                    ExecutiveContact = EntityToDtoMapper.ToContactDto(response.ExecutiveContact),
                    ProgramManager = EntityToDtoMapper.ToContactDto(response.ProgramManager),
                    PortalRoles = response.PortalRoles?.Entities
                        .Select(EntityToDtoMapper.ToPortalRoleDto).ToArray()
                        ?? Array.Empty<PortalRoleDto>(),
                    ProgramCollection = response.ProgramCollection?.Entities
                        .Select(EntityToDtoMapper.ToProgramDto).ToArray()
                        ?? Array.Empty<ProgramDto>(),
                    ProgramContactCollection = response.ProgramContactCollection?.Entities
                        .Select(EntityToDtoMapper.ToProgramContactDto).ToArray()
                        ?? Array.Empty<ProgramContactDto>(),
                    ProgramTypeCollection = response.ProgramTypeCollection?.Entities
                        .Select(EntityToDtoMapper.ToProgramTypeDto).ToArray()
                        ?? Array.Empty<ProgramTypeDto>(),
                    StaffCollection = response.StaffCollection?.Entities
                        .Select(EntityToDtoMapper.ToContactDto).ToArray()
                        ?? Array.Empty<ContactDto>()
                };

                return Ok(dto);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting cap application info 'vsd_GetCPUScheduleF_CAP'. Contract id = {scheduleFId}. Source = CPU");
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(SetCAPApplicationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SetCAPApplicationResponseDto>> SetCAPApplication(
            [FromBody] CAPApplicationPost model)
        {
            if (!ModelState.IsValid)
            {
                var messages = string.Join("\n", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                _logger.Error(new Exception(messages),
                    $"API call to 'SetCAPApplication' made with invalid model state. Error is:\n{messages}\nSource = CPU");
                return BadRequest(ModelState);
            }

            try
            {
                var request = CAPApplicationRequestMapper.ToRequest(model);
                var response = (Vsd_SetCpuOrgContractsResponse)
                    await _organizationService.ExecuteAsync(request);

                return Ok(new SetCAPApplicationResponseDto
                {
                    IsSuccess = response.IsSuccess,
                    Result = response.Result ?? string.Empty
                });
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while submitting cap application. Source = CPU");
                return BadRequest();
            }
        }
    }
}