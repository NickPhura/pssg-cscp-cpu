using Database.Model;
using Gov.Cscp.Victims.Public.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.PowerPlatform.Dataverse.Client;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    public class OrgController : Controller
    {
        private readonly IOrganizationServiceAsync _organizationService;
        private readonly ILogger _logger;

        public OrgController(IOrganizationServiceAsync organizationService)
        {
            _organizationService = organizationService;
            _logger = Log.Logger;
        }

        /// <summary>
        /// Updates organisation-level information (name, address, executive/board contacts, etc.).
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(SetOrgContractsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SetOrgContractsResponseDto>> UpdateOrg([FromBody] OrganizationPost model)
        {
            if (!ModelState.IsValid)
            {
                var messages = string.Join("\n", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                _logger.Error(new Exception(messages), $"API call to 'UpdateOrg' made with invalid model state. Error is:\n{messages}\nSource = CPU");
                return BadRequest(ModelState);
            }

            try
            {
                var request = OrgRequestMapper.ToRequest(model);
                var response = (Vsd_SetCpuOrgContractsResponse)await _organizationService.ExecuteAsync(request);

                return Ok(new SetOrgContractsResponseDto
                {
                    IsSuccess = response.IsSuccess,
                    Result = response.Result ?? string.Empty
                });
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while updating organisation. Source = CPU");
                return BadRequest();
            }
        }

        /// <summary>
        /// Creates or updates staff contacts associated with the organisation.
        /// </summary>
        [HttpPost("SetStaff", Name = "SetStaff")]
        [ProducesResponseType(typeof(SetOrgContractsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SetOrgContractsResponseDto>> SetStaff([FromBody] OrganizationPost model)
        {
            if (!ModelState.IsValid)
            {
                var messages = string.Join("\n", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                _logger.Error(new Exception(messages), $"API call to 'SetStaff' made with invalid model state. Error is:\n{messages}\nSource = CPU");
                return BadRequest(ModelState);
            }

            try
            {
                var request = OrgRequestMapper.ToRequest(model);
                var response = (Vsd_SetCpuOrgContractsResponse)await _organizationService.ExecuteAsync(request);

                return Ok(new SetOrgContractsResponseDto
                {
                    IsSuccess = response.IsSuccess,
                    Result = response.Result ?? string.Empty
                });
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while setting staff. Source = CPU");
                return BadRequest();
            }
        }
    }
}