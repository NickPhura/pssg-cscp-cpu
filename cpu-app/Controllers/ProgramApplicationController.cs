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
    public class ProgramApplicationController : Controller
    {
        private readonly IOrganizationServiceAsync _organizationService;
        private readonly ILogger _logger;

        public ProgramApplicationController(IOrganizationServiceAsync organizationService)
        {
            this._organizationService = organizationService;
            _logger = Log.Logger;
        }

        [HttpGet("{businessBceid}/{userBceid}/{scheduleFId}")]
        [ProducesResponseType(typeof(ProgramApplicationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProgramApplicationDto>> GetProgramApplication(string businessBceid, string userBceid, string scheduleFId)
        {
            if (string.IsNullOrWhiteSpace(businessBceid))
                return BadRequest("businessBceid parameter is required.");

            if (string.IsNullOrWhiteSpace(userBceid))
                return BadRequest("userBceid parameter is required.");

            if (!Guid.TryParse(scheduleFId, out var scheduleFGuid))
                return BadRequest("scheduleFId must be a valid GUID.");

            try
            {
                var request = new Vsd_GetCpuScheduleFRequest
                {
                    Target = new EntityReference("vsd_contract", scheduleFGuid),
                    UserBcEId = userBceid,
                    BusinessBcEId = businessBceid
                };

                var response = (Vsd_GetCpuScheduleFResponse)await _organizationService.ExecuteAsync(request);

                var dto = new ProgramApplicationDto
                {
                    IsSuccess = response.IsSuccess,
                    Result = response.Result ?? string.Empty,
                    Businessbceid = response.BusinessBcEId,
                    Userbceid = response.UserBcEId,
                    BoardContact = EntityToDtoMapper.ToContactDto(response.BoardContact),
                    ExecutiveContact = EntityToDtoMapper.ToContactDto(response.ExecutiveContact),
                    Contract = EntityToDtoMapper.ToContractDto(response.Contract),
                    Organization = EntityToDtoMapper.ToOrganizationDto(response.Organization),
                    PortalRoles = EntityCollectionToPortalRoleDtoArray(response.PortalRoles),
                    ProgramCollection = EntityCollectionToProgramDtoArray(response.ProgramCollection),
                    ProgramContactCollection = EntityCollectionToProgramContactDtoArray(response.ProgramContactCollection),
                    ProgramSubcontractorCollection = EntityCollectionToProgramContactDtoArray(response.ProgramSubcontractorCollection),
                    ProgramTypeCollection = EntityCollectionToProgramTypeDtoArray(response.ProgramTypeCollection),
                    RegionDistrictCollection = EntityCollectionToRegionDistrictDtoArray(response.RegionDistrictCollection),
                    ScheduleCollection = EntityCollectionToScheduleDtoArray(response.ScheduleCollection),
                    ServiceAreaCollection = EntityCollectionToServiceAreaDtoArray(response.ServiceAreaCollection),
                    StaffCollection = EntityCollectionToContactDtoArray(response.StaffCollection)
                };

                return Ok(dto);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting program application info 'vsd_GetCPUScheduleF'. Contract id = {scheduleFId}. Source = CPU");
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetProgramApplication([FromBody] ProgramApplicationPost model)
        {
            if (!ModelState.IsValid)
            {
                string messages = string.Join("\n", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                _logger.Error(new Exception(messages), $"API call to 'SetProgramApplication' made with invalid model state. Error is:\n{messages}\nSource = CPU");
                return BadRequest(ModelState);
            }

            if (model.ContactCollection != null)
                model.ContactCollection = CleanEmptyContacts(model.ContactCollection);

            try
            {
                var request = ProgramApplicationRequestMapper.ToRequest(model);
                var response = (Vsd_SetCpuOrgContractsResponse)await _organizationService.ExecuteAsync(request);

                return Ok(new { isSuccess = response.IsSuccess, result = response.Result ?? string.Empty });
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while submitting program application. Source = CPU");
                return BadRequest();
            }
        }

        private DynamicsProgramApplicationContactPost[] CleanEmptyContacts(DynamicsProgramApplicationContactPost[] contacts)
        {
            foreach (DynamicsProgramApplicationContactPost contact in contacts)
            {
                var requiredFields = new[] { contact.firstname, contact.lastname, contact.jobtitle, contact.emailaddress1, contact.address1_line1 };
                if (requiredFields.Any(x => x == null))
                    contacts = contacts.Where(c => c != contact).ToArray();
            }
            return contacts;
        }

        private PortalRoleDto[] EntityCollectionToPortalRoleDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null) return new PortalRoleDto[0];
            return collection.Entities.Select(e => EntityToDtoMapper.ToPortalRoleDto(e)).ToArray();
        }

        private ProgramDto[] EntityCollectionToProgramDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null) return new ProgramDto[0];
            return collection.Entities.Select(e => EntityToDtoMapper.ToProgramDto(e)).ToArray();
        }

        private ProgramContactDto[] EntityCollectionToProgramContactDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null) return new ProgramContactDto[0];
            return collection.Entities.Select(e => EntityToDtoMapper.ToProgramContactDto(e)).ToArray();
        }

        private ProgramTypeDto[] EntityCollectionToProgramTypeDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null) return new ProgramTypeDto[0];
            return collection.Entities.Select(e => EntityToDtoMapper.ToProgramTypeDto(e)).ToArray();
        }

        private RegionDistrictDto[] EntityCollectionToRegionDistrictDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null) return new RegionDistrictDto[0];
            return collection.Entities.Select(e => EntityToDtoMapper.ToRegionDistrictDto(e)).ToArray();
        }

        private ScheduleDto[] EntityCollectionToScheduleDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null) return new ScheduleDto[0];
            return collection.Entities.Select(e => EntityToDtoMapper.ToScheduleDto(e)).ToArray();
        }

        private ServiceAreaDto[] EntityCollectionToServiceAreaDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null) return new ServiceAreaDto[0];
            return collection.Entities.Select(e => EntityToDtoMapper.ToServiceAreaDto(e)).ToArray();
        }

        private ContactDto[] EntityCollectionToContactDtoArray(EntityCollection collection)
        {
            if (collection == null || collection.Entities == null) return new ContactDto[0];
            return collection.Entities.Select(e => EntityToDtoMapper.ToContactDto(e)).ToArray();
        }
    }
}