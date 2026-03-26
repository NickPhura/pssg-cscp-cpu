using Database.Model;
using Gov.Cscp.Victims.Public.Models;
using Gov.Cscp.Victims.Public.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Serilog;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/cpuorgcontracts")] // Keep route for backward compatibility
    // [Authorize]
    public class CpuOrgContractsController : Controller
    {
        private readonly IOrganizationServiceAsync _organizationService;
        private readonly ILogger _logger;

        public CpuOrgContractsController(IOrganizationServiceAsync organizationService)
        {
            this._organizationService = organizationService;
            _logger = Log.Logger;
        }

        [HttpGet("{businessBceid}/{userBceid}")]
        [ProducesResponseType(typeof(CpuOrgContractsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CpuOrgContractsDto>> GetBlob(string businessBceid, string userBceid)
        {
            try
            {
                var request = new Vsd_GetCpuOrgContractsRequest
                {
                    UserBcEId = userBceid,
                    BusinessBcEId = businessBceid
                };

                var response = (Vsd_GetCpuOrgContractsResponse)await _organizationService.ExecuteAsync(request);

                // Map the Dataverse response to the DTO
                var dto = new CpuOrgContractsDto
                {
                    IsSuccess = response.IsSuccess,
                    Result = response.Result ?? string.Empty,
                    Businessbceid = response.BusinessBcEId,
                    Userbceid = response.UserBcEId,
                    BoardContact = EntityToDtoMapper.ToContactDto(response.BoardContact),
                    ExecutiveContact = EntityToDtoMapper.ToContactDto(response.ExecutiveContact),
                    MinistryUser = EntityToDtoMapper.ToSystemUserDto(response.MinistryUser),
                    Organization = EntityToDtoMapper.ToOrganizationDto(response.Organization),
                    Contracts = EntityCollectionToContractDtoArray(response.Contracts),
                    Invoices = EntityCollectionToInvoiceDtoArray(response.Invoices),
                    Programs = EntityCollectionToProgramDtoArray(response.Programs),
                    PortalRoles = EntityCollectionToPortalRoleDtoArray(response.PortalRoles),
                    Staff = EntityCollectionToContactDtoArray(response.Staff),
                    Tasks = EntityCollectionToTaskDtoArray(response.Tasks),
                    Messages = new MessageDto[0] // Not in response, default to empty array
                };

                return Ok(dto);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while getting contract info 'vsd_GetCPUOrgContracts'. Source = CPU");
                return BadRequest();
            }
        }

        private object EntityToDictionary(Entity entity)
        {
            if (entity == null) return null;

            var dict = new System.Collections.Generic.Dictionary<string, object>();

            foreach (var attr in entity.Attributes)
            {
                dict[attr.Key] = attr.Value;
            }

            if (entity.FormattedValues != null)
            {
                foreach (var formatted in entity.FormattedValues)
                {
                    dict[formatted.Key + "name"] = formatted.Value;
                }
            }

            return dict;
        }

        private object[] EntityCollectionToArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null)
                return new object[0];

            return collection.Entities.Select(e => EntityToDictionary(e)).ToArray();
        }

        private PortalRoleDto[] EntityCollectionToPortalRoleDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null)
                return new PortalRoleDto[0];

            return collection.Entities.Select(e => EntityToDtoMapper.ToPortalRoleDto(e)).ToArray();
        }

        private TaskDto[] EntityCollectionToTaskDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null)
                return new TaskDto[0];

            return collection.Entities.Select(e => EntityToDtoMapper.ToTaskDto(e)).ToArray();
        }

        private ProgramDto[] EntityCollectionToProgramDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null)
                return new ProgramDto[0];

            return collection.Entities.Select(e => EntityToDtoMapper.ToProgramDto(e)).ToArray();
        }

        private ContractDto[] EntityCollectionToContractDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null)
                return new ContractDto[0];

            return collection.Entities.Select(e => EntityToDtoMapper.ToContractDto(e)).ToArray();
        }

        private InvoiceDto[] EntityCollectionToInvoiceDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null)
                return new InvoiceDto[0];

            return collection.Entities.Select(e => EntityToDtoMapper.ToInvoiceDto(e)).ToArray();
        }

        private ContactDto[] EntityCollectionToContactDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null)
                return new ContactDto[0];

            return collection.Entities.Select(e => EntityToDtoMapper.ToContactDto(e)).ToArray();
        }
    }
}