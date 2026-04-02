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
    public class ExpenseReportController : Controller
    {
        private readonly IOrganizationServiceAsync _organizationService;
        private readonly ILogger _logger;

        public ExpenseReportController(IOrganizationServiceAsync organizationService)
        {
            this._organizationService = organizationService;
            _logger = Log.Logger;
        }

        [HttpGet("{businessBceid}/{userBceid}/{expenseReportId}")]
        [ProducesResponseType(typeof(ExpenseReportDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExpenseReportDto>> GetExpenseReport(string businessBceid, string userBceid, string expenseReportId)
        {
            if (string.IsNullOrWhiteSpace(businessBceid))
                return BadRequest("businessBceid parameter is required.");

            if (string.IsNullOrWhiteSpace(userBceid))
                return BadRequest("userBceid parameter is required.");

            if (!Guid.TryParse(expenseReportId, out var expenseReportGuid))
                return BadRequest("expenseReportId must be a valid GUID.");

            try
            {
                var request = new Vsd_GetCpuScheduleGRequest
                {
                    Target = new EntityReference("vsd_scheduleg", expenseReportGuid),
                    UserBcEId = userBceid,
                    BusinessBcEId = businessBceid
                };

                var response = (Vsd_GetCpuScheduleGResponse)await _organizationService.ExecuteAsync(request);

                var dto = new ExpenseReportDto
                {
                    IsSuccess = response.IsSuccess,
                    Result = response.Result ?? string.Empty,
                    Businessbceid = response.BusinessBcEId,
                    Userbceid = response.UserBcEId,
                    Contract = EntityToDtoMapper.ToContractDto(response.Contract),
                    Organization = EntityToDtoMapper.ToOrganizationDto(response.Organization),
                    PortalRoles = response.PortalRoles?.Entities?.Select(e => EntityToDtoMapper.ToPortalRoleDto(e)).ToArray() ?? Array.Empty<PortalRoleDto>(),
                    Program = EntityToDtoMapper.ToProgramDto(response.Program),
                    ScheduleG = EntityToDtoMapper.ToScheduleGDto(response.ScheduleG),
                    ScheduleGLineItems = response.ScheduleGLineItems?.Entities?.Select(e => EntityToDtoMapper.ToScheduleGLineItemDto(e)).ToArray() ?? Array.Empty<ScheduleGLineItemDto>()
                };

                return Ok(dto);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting expense report info 'vsd_GetCPUScheduleG'. Schedule G id = {expenseReportId}. Source = CPU");
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetExpenseReport([FromBody] ExpenseReportPost model)
        {
            if (!ModelState.IsValid)
            {
                string messages = string.Join("\n", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                _logger.Error(new Exception(messages), $"API call to 'SetExpenseReport' made with invalid model state. Error is:\n{messages}\nSource = CPU");
                return BadRequest(ModelState);
            }

            try
            {
                var request = ExpenseReportRequestMapper.ToRequest(model);
                var response = (Vsd_SetCpuOrgContractsResponse)await _organizationService.ExecuteAsync(request);

                return Ok(new { isSuccess = response.IsSuccess, result = response.Result ?? string.Empty });
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while submitting expense report. Source = CPU");
                return BadRequest();
            }
        }
    }
}