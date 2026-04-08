using Database.Model;
using Gov.Cscp.Victims.Public.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ProgramSurplusController : Controller
    {
        private readonly IOrganizationServiceAsync _organizationService;
        private readonly ILogger _logger;

        public ProgramSurplusController(IOrganizationServiceAsync organizationService)
        {
            _organizationService = organizationService;
            _logger = Log.Logger;
        }

        [HttpGet("{businessBceid}/{userBceid}/{surplusId}")]
        [ProducesResponseType(typeof(ProgramSurplusDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProgramSurplusDto>> GetProgramSurplus(
            string businessBceid, string userBceid, string surplusId)
        {
            if (string.IsNullOrWhiteSpace(businessBceid))
                return BadRequest("businessBceid parameter is required.");
            if (string.IsNullOrWhiteSpace(userBceid))
                return BadRequest("userBceid parameter is required.");
            if (!Guid.TryParse(surplusId, out var surplusGuid))
                return BadRequest("surplusId must be a valid GUID.");

            try
            {
                var request = new Vsd_GetCpuSurplusPlanRequest
                {
                    Target = new EntityReference("vsd_surplusplanreport", surplusGuid),
                    UserBcEId = userBceid,
                    BusinessBcEId = businessBceid
                };

                var response = (Vsd_GetCpuSurplusPlanResponse)await _organizationService.ExecuteAsync(request);

                var dto = new ProgramSurplusDto
                {
                    IsSuccess = response.IsSuccess,
                    Result = response.Result ?? string.Empty,
                    Businessbceid = response.BusinessBcEId,
                    Userbceid = response.UserBcEId,
                    Contract = EntityToDtoMapper.ToContractDto(response.Contract),
                    Program = EntityToDtoMapper.ToProgramDto(response.Program),
                    Organization = EntityToDtoMapper.ToOrganizationDto(response.Organization),
                    SurplusPlan = EntityToDtoMapper.ToSurplusPlanDto(response.SurplusPlan),
                    SurplusPlanLineItems = response.SurplusPlanLineItems?.Entities
                        .Select(EntityToDtoMapper.ToSurplusLineItemDto).ToArray()
                        ?? Array.Empty<SurplusLineItemDto>(),
                    EligibleExpenseItemCollection = response.EligibleExpenseItemCollection?.Entities
                        .Select(EntityToDtoMapper.ToEligibleExpenseItemDto).ToArray()
                        ?? Array.Empty<EligibleExpenseItemDto>()
                };

                return Ok(dto);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting program surplus info 'vsd_GetCPUSurplusPlan'. Surplus id = {surplusId}. Source = CPU");
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(SetSurplusResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SetSurplusResponseDto>> SetProgramSurplus([FromBody] ProgramSurplusPost model)
        {
            if (!ModelState.IsValid)
            {
                var messages = string.Join("\n", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                _logger.Error(new Exception(messages), $"API call to 'SetProgramSurplus' made with invalid model state. Error is:\n{messages}\nSource = CPU");
                return BadRequest(ModelState);
            }

            try
            {
                var request = ProgramSurplusRequestMapper.ToSetSurplusRequest(model);
                var response = (Vsd_SetCpuOrgContractsResponse)await _organizationService.ExecuteAsync(request);

                return Ok(new SetSurplusResponseDto
                {
                    IsSuccess = response.IsSuccess,
                    Result = response.Result ?? string.Empty
                });
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while submitting program surplus. Source = CPU");
                return BadRequest();
            }
        }
    }
}