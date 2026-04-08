using Database.Model;
using Gov.Cscp.Victims.Public.Models;
using Gov.Cscp.Victims.Public.Services;
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
    public class BudgetProposalController : Controller
    {
        private readonly IOrganizationServiceAsync _organizationService;
        private readonly IDynamicsResultService _dynamicsResultService;
        private readonly ILogger _logger;

        public BudgetProposalController(IOrganizationServiceAsync organizationService, IDynamicsResultService dynamicsResultService)
        {
            _organizationService = organizationService;
            _dynamicsResultService = dynamicsResultService;
            _logger = Log.Logger;
        }

        [HttpGet("{businessBceid}/{userBceid}/{contractId}")]
        [ProducesResponseType(typeof(BudgetProposalDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BudgetProposalDto>> GetBudgetProposal(string businessBceid, string userBceid, string contractId)
        {
            if (string.IsNullOrWhiteSpace(businessBceid))
            {
                return BadRequest("businessBceid parameter is required.");
            }

            if (string.IsNullOrWhiteSpace(userBceid))
            {
                return BadRequest("userBceid parameter is required.");
            }

            if (string.IsNullOrWhiteSpace(contractId))
            {
                return BadRequest("contractId parameter is required.");
            }

            try
            {
                var request = new Vsd_GetCpuBudgetProposalRequest
                {
                    UserBcEId = userBceid,
                    BusinessBcEId = businessBceid,
                    Target = new EntityReference("vsd_contract", Guid.Parse(contractId))
                };

                var response = (Vsd_GetCpuBudgetProposalResponse)await _organizationService.ExecuteAsync(request);

                // Map the Dataverse response to the DTO
                var dto = new BudgetProposalDto
                {
                    IsSuccess = response.IsSuccess,
                    Result = response.Result ?? string.Empty,
                    Businessbceid = response.BusinessBcEId,
                    Userbceid = response.UserBcEId,
                    Contract = EntityToDtoMapper.ToContractBudgetDto(response.Contract),
                    Organization = EntityToDtoMapper.ToOrganizationDto(response.Organization),
                    PortalRoles = EntityCollectionToPortalRoleDtoArray(response.PortalRoles),
                    ProgramCollection = EntityCollectionToProgramBudgetDtoArray(response.ProgramCollection),
                    AdministrationCostCollection = EntityCollectionToProgramExpenseDtoArray(response.AdministrationCostCollection),
                    ProgramDeliveryCostCollection = EntityCollectionToProgramExpenseDtoArray(response.ProgramDeliveryCostCollection),
                    SalaryAndBenefitCollection = EntityCollectionToProgramExpenseDtoArray(response.SalaryAndBenefitCollection),
                    ProgramRevenueSourceCollection = EntityCollectionToProgramRevenueSourceDtoArray(response.ProgramRevenueSourceCollection),
                    EligibleExpenseItemCollection = EntityCollectionToEligibleExpenseItemDtoArray(response.EligibleExpenseItemCollection),
                    ProgramTypeCollection = EntityCollectionToProgramTypeDtoArray(response.ProgramTypeCollection)
                };

                return Ok(dto);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting budget proposal info 'vsd_GetCPUBudgetProposal'. Contract id = {contractId}. Source = CPU");
                return BadRequest();
            }
        }

        private PortalRoleDto[] EntityCollectionToPortalRoleDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null)
                return new PortalRoleDto[0];

            return collection.Entities.Select(e => EntityToDtoMapper.ToPortalRoleDto(e)).ToArray();
        }

        private ProgramBudgetDto[] EntityCollectionToProgramBudgetDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null)
                return new ProgramBudgetDto[0];

            return collection.Entities.Select(e => EntityToDtoMapper.ToProgramBudgetDto(e)).ToArray();
        }

        private ProgramExpenseDto[] EntityCollectionToProgramExpenseDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null)
                return new ProgramExpenseDto[0];

            return collection.Entities.Select(e => EntityToDtoMapper.ToProgramExpenseDto(e)).ToArray();
        }

        private ProgramRevenueSourceDto[] EntityCollectionToProgramRevenueSourceDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null)
                return new ProgramRevenueSourceDto[0];

            return collection.Entities.Select(e => EntityToDtoMapper.ToProgramRevenueSourceDto(e)).ToArray();
        }

        private EligibleExpenseItemDto[] EntityCollectionToEligibleExpenseItemDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null)
                return new EligibleExpenseItemDto[0];

            return collection.Entities.Select(e => EntityToDtoMapper.ToEligibleExpenseItemDto(e)).ToArray();
        }

        private ProgramTypeDto[] EntityCollectionToProgramTypeDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null)
                return new ProgramTypeDto[0];

            return collection.Entities.Select(e => EntityToDtoMapper.ToProgramTypeDto(e)).ToArray();
        }

        [HttpPost]
        public async Task<IActionResult> SetBudgetProposal([FromBody] BudgetProposalPost model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string messages = string.Join("\n", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                    _logger.Error(new Exception(messages), $"API call to 'SetBudgetProposal' made with invalid model state. Error is:\n{messages}\nSource = CPU");
                    return BadRequest(ModelState);
                }

                string endpointUrl = "vsd_SetCPUOrgContracts";
                string modelString = System.Text.Json.JsonSerializer.Serialize(model);
                modelString = Helpers.Helpers.updateFortunecookieBindNull(modelString);
                modelString = Helpers.Helpers.removeNullsForBudgetProposal(modelString);

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, modelString);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while submitting budget proposal. Source = CPU");
                return BadRequest();
            }
            finally { }
        }
    }
}