using Database.Model;
using Gov.Cscp.Victims.Public.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    public class StatusReportController : Controller
    {
        private readonly IOrganizationServiceAsync _organizationService;
        private readonly ILogger _logger;

        // Partial-save status code - excluded from export
        private const int PartialSaveStatusCode = 100000004;

        public StatusReportController(IOrganizationServiceAsync organizationService)
        {
            _organizationService = organizationService;
            _logger = Log.Logger;
        }

        [HttpGet("{businessBceid}/{userBceid}/{taskId}")]
        [ProducesResponseType(typeof(StatusReportQuestionsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StatusReportQuestionsDto>> GetQuestions(
            string businessBceid, string userBceid, string taskId)
        {
            if (string.IsNullOrWhiteSpace(businessBceid))
                return BadRequest("businessBceid parameter is required.");
            if (string.IsNullOrWhiteSpace(userBceid))
                return BadRequest("userBceid parameter is required.");
            if (!Guid.TryParse(taskId, out var taskGuid))
                return BadRequest("taskId must be a valid GUID.");

            try
            {
                // Step 1 – get questions for the task
                var questionsRequest = new Vsd_GetCpuMonthlyStatisticsQuestionsRequest
                {
                    Target = new EntityReference("task", taskGuid),
                    UserBcEId = userBceid,
                    BusinessBcEId = businessBceid
                };
                var questionsResponse =
                    (Vsd_GetCpuMonthlyStatisticsQuestionsResponse)
                    await _organizationService.ExecuteAsync(questionsRequest);

                var dto = MapQuestionsResponse(questionsResponse);

                // Step 2 – check for an existing data collection (for pre-populating saved answers)
                if (questionsResponse.IsSuccess
                    && questionsResponse.Contract != null
                    && questionsResponse.Program != null
                    && questionsResponse.ReportingPeriod != 0)
                {
                    var contractId = (questionsResponse.Contract.GetAttributeValue<Guid>("vsd_contractid") != Guid.Empty
                        ? questionsResponse.Contract.GetAttributeValue<Guid>("vsd_contractid")
                        : questionsResponse.Contract.Id);

                    var programId = (questionsResponse.Program.GetAttributeValue<Guid>("vsd_programid") != Guid.Empty
                        ? questionsResponse.Program.GetAttributeValue<Guid>("vsd_programid")
                        : questionsResponse.Program.Id);

                    var collectionQuery = new QueryExpression("vsd_datacollection");
                    collectionQuery.ColumnSet = new ColumnSet("vsd_datacollectionid");
                    collectionQuery.Criteria.AddCondition("vsd_contract", ConditionOperator.Equal, contractId);
                    collectionQuery.Criteria.AddCondition("vsd_program", ConditionOperator.Equal, programId);
                    collectionQuery.Criteria.AddCondition("vsd_reportingperiod", ConditionOperator.Equal,
                        questionsResponse.ReportingPeriod);

                    var collectionResult = await _organizationService.RetrieveMultipleAsync(collectionQuery);
                    var latestCollection = collectionResult?.Entities?.LastOrDefault();

                    // Step 3 – if a collection exists, load the saved answers
                    if (latestCollection != null)
                    {
                        dto.DataCollectionid = latestCollection.Id.ToString();
                        var answersRequest = new Vsd_GetCpuMonthlyStatisticsAnswersRequest
                        {
                            Target = new EntityReference("vsd_datacollection", latestCollection.Id),
                            UserBcEId = userBceid,
                            BusinessBcEId = businessBceid
                        };
                        var answersResponse =
                            (Vsd_GetCpuMonthlyStatisticsAnswersResponse)
                            await _organizationService.ExecuteAsync(answersRequest);

                        if (answersResponse.IsSuccess && answersResponse.AnswerCollection != null)
                        {
                            dto.AnswerCollection = answersResponse.AnswerCollection.Entities
                                .Select(EntityToDtoMapper.ToStatusReportAnswerItemDto)
                                .ToArray();
                        }
                    }
                }

                return Ok(dto);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting stats questions info 'vsd_GetCPUMonthlyStatisticsQuestions'. Task id = {taskId}. Source = CPU");
                return BadRequest();
            }
        }

        [HttpGet("monthly_stats/{businessBceid}/{userBceid}/{contractId}")]
        [ProducesResponseType(typeof(MonthlyStatisticsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MonthlyStatisticsDto>> GetMonthlyStats(
            string businessBceid, string userBceid, string contractId)
        {
            if (string.IsNullOrWhiteSpace(businessBceid))
                return BadRequest("businessBceid parameter is required.");
            if (string.IsNullOrWhiteSpace(userBceid))
                return BadRequest("userBceid parameter is required.");
            if (!Guid.TryParse(contractId, out var contractGuid))
                return BadRequest("contractId must be a valid GUID.");

            try
            {
                var request = new Vsd_GetCpuMonthlyStatisticsRequest
                {
                    Target = new EntityReference("vsd_contract", contractGuid),
                    UserBcEId = userBceid,
                    BusinessBcEId = businessBceid
                };
                var response =
                    (Vsd_GetCpuMonthlyStatisticsResponse)
                    await _organizationService.ExecuteAsync(request);

                var dto = new MonthlyStatisticsDto
                {
                    IsSuccess = response.IsSuccess,
                    Result = response.Result ?? string.Empty,
                    Businessbceid = response.BusinessBcEId,
                    Userbceid = response.UserBcEId,
                    DataCollection = response.DataCollection?.Entities
                        .Select(EntityToDtoMapper.ToDataCollectionItemDto).ToArray()
                        ?? Array.Empty<DataCollectionItemDto>(),
                    ContactCollection = response.ContactCollection?.Entities
                        .Select(EntityToDtoMapper.ToContactMinimalDto).ToArray()
                        ?? Array.Empty<ContactMinimalDto>(),
                    ProgramCollection = response.ProgramCollection?.Entities
                        .Select(EntityToDtoMapper.ToProgramMinimalDto).ToArray()
                        ?? Array.Empty<ProgramMinimalDto>()
                };

                return Ok(dto);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting monthly stats info 'vsd_GetCPUMonthlyStatistics'. Contract id = {contractId}. Source = CPU");
                return BadRequest();
            }
        }

        [HttpGet("data_collection/{businessBceid}/{userBceid}/{dataCollectionId}")]
        [ProducesResponseType(typeof(StatusReportAnswersDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StatusReportAnswersDto>> GetSubmittedReport(
            string businessBceid, string userBceid, string dataCollectionId)
        {
            if (string.IsNullOrWhiteSpace(businessBceid))
                return BadRequest("businessBceid parameter is required.");
            if (string.IsNullOrWhiteSpace(userBceid))
                return BadRequest("userBceid parameter is required.");
            if (!Guid.TryParse(dataCollectionId, out var collectionGuid))
                return BadRequest("dataCollectionId must be a valid GUID.");

            try
            {
                var request = new Vsd_GetCpuMonthlyStatisticsAnswersRequest
                {
                    Target = new EntityReference("vsd_datacollection", collectionGuid),
                    UserBcEId = userBceid,
                    BusinessBcEId = businessBceid
                };
                var response =
                    (Vsd_GetCpuMonthlyStatisticsAnswersResponse)
                    await _organizationService.ExecuteAsync(request);

                var dto = new StatusReportAnswersDto
                {
                    IsSuccess = response.IsSuccess,
                    Result = response.Result ?? string.Empty,
                    Businessbceid = response.BusinessBcEId,
                    Userbceid = response.UserBcEId,
                    ReportingPeriod = response.Data?.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("vsd_reportingperiod")?.Value,
                    AnswerCollection = response.AnswerCollection?.Entities
                        .Select(EntityToDtoMapper.ToStatusReportAnswerItemDto).ToArray()
                        ?? Array.Empty<StatusReportAnswerItemDto>(),
                    CategoryCollection = response.CategoryCollection?.Entities
                        .Select(EntityToDtoMapper.ToStatusReportCategoryDto).ToArray()
                        ?? Array.Empty<StatusReportCategoryDto>(),
                    Contact = EntityToDtoMapper.ToStatusReportContactDto(response.Contact),
                    Contract = EntityToDtoMapper.ToStatusReportContractDto(response.Contract),
                    Organization = EntityToDtoMapper.ToStatusReportOrganizationDto(response.Organization),
                    Program = EntityToDtoMapper.ToStatusReportProgramDto(response.Program)
                };

                return Ok(dto);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting submitted stats info 'vsd_GetCPUMonthlyStatisticsAnswers'. Data collection id = {dataCollectionId}. Source = CPU");
                return BadRequest();
            }
        }

        [HttpPost("{taskId}")]
        [ProducesResponseType(typeof(SetStatusReportAnswersResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SetStatusReportAnswersResponseDto>> AnswerQuestions(
            [FromBody] MonthlyStatisticsAnswers model, string taskId)
        {
            if (!ModelState.IsValid)
            {
                var messages = string.Join("\n", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
                _logger.Error(new Exception(messages),
                    $"API call to 'AnswerQuestions' made with invalid model state. Error is:\n{messages}\nSource = CPU");
                return BadRequest(ModelState);
            }

            if (!Guid.TryParse(taskId, out var taskGuid))
                return BadRequest("taskId must be a valid GUID.");

            try
            {
                var request = StatusReportRequestMapper.ToSetAnswersRequest(model, taskGuid);
                var response =
                    (Vsd_SetCpuMonthlyStatisticsAnswersResponse)
                    await _organizationService.ExecuteAsync(request);

                return Ok(new SetStatusReportAnswersResponseDto
                {
                    IsSuccess = response.IsSuccess,
                    Result = response.Result ?? string.Empty
                });
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while submitting stats answers. Source = CPU");
                return BadRequest();
            }
        }

        [HttpGet("export_monthly_report/{contractId}/{programId}/{contractNumber}/{programName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExportMonthlyReport(
            string contractId, string programId, string contractNumber, string programName)
        {
            if (!Guid.TryParse(contractId, out var contractGuid))
                return BadRequest("contractId must be a valid GUID.");
            if (!Guid.TryParse(programId, out var programGuid))
                return BadRequest("programId must be a valid GUID.");

            try
            {
                // Query submitted data collections for the contract/program
                var collectionQuery = new QueryExpression("vsd_datacollection");
                collectionQuery.ColumnSet = new ColumnSet("vsd_datacollectionid", "vsd_reportingperiod");
                collectionQuery.Criteria.AddCondition("vsd_contract", ConditionOperator.Equal, contractGuid);
                collectionQuery.Criteria.AddCondition("vsd_program", ConditionOperator.Equal, programGuid);
                collectionQuery.Criteria.AddCondition("statuscode", ConditionOperator.NotEqual, PartialSaveStatusCode);

                var collectionsResult = await _organizationService.RetrieveMultipleAsync(collectionQuery);

                if (collectionsResult == null || !collectionsResult.Entities.Any())
                    return NoContent();

                var reports = collectionsResult.Entities
                    .Select(e => new
                    {
                        collectionId = e.Id,
                        reportingPeriodId = e.GetAttributeValue<OptionSetValue>("vsd_reportingperiod")?.Value ?? 0
                    })
                    .OrderBy(r => r.reportingPeriodId >= (int)MonthEnum.April
                                  && r.reportingPeriodId <= (int)MonthEnum.December ? 0 : 1)
                    .ThenBy(r => r.reportingPeriodId)
                    .ToArray();

                // Query line items for all collected data collections
                var lineItemsQuery = new QueryExpression("vsd_datacollectionlineitem");
                lineItemsQuery.ColumnSet = new ColumnSet(
                    "vsd_datacollectionlineitemid", "vsd_name", "vsd_yesno",
                    "vsd_textanswer", "vsd_number", "vsd_questionid", "vsd_datacollectionid");

                var dcCondition = new ConditionExpression(
                    "vsd_datacollectionid", ConditionOperator.In);
                foreach (var r in reports)
                    dcCondition.Values.Add(r.collectionId);
                lineItemsQuery.Criteria.AddCondition(dcCondition);

                var lineItemsResult = await _organizationService.RetrieveMultipleAsync(lineItemsQuery);

                var reportCsvHeaders = "," + string.Join(",", reports.Select(r => (MonthEnum)r.reportingPeriodId));
                return BuildCsvFile(lineItemsResult, reportCsvHeaders, programName, contractNumber);
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while exporting monthly report. Contract id = {contractId}. Source = CPU");
                return BadRequest();
            }
        }

        // ── Private helpers ──────────────────────────────────────────────────────

        private static StatusReportQuestionsDto MapQuestionsResponse(
            Vsd_GetCpuMonthlyStatisticsQuestionsResponse r)
        {
            return new StatusReportQuestionsDto
            {
                IsSuccess = r.IsSuccess,
                Result = r.Result ?? string.Empty,
                Businessbceid = r.BusinessBcEId,
                Userbceid = r.UserBcEId,
                ReportingPeriod = r.ReportingPeriod,
                DataCollectionid = null,  // populated after querying vsd_datacollection
                Contract = EntityToDtoMapper.ToStatusReportContractDto(r.Contract),
                Program = EntityToDtoMapper.ToStatusReportProgramDto(r.Program),
                Organization = EntityToDtoMapper.ToStatusReportOrganizationDto(r.Organization),
                CategoryCollection = r.CategoryCollection?.Entities
                    .Select(EntityToDtoMapper.ToStatusReportCategoryDto).ToArray()
                    ?? Array.Empty<StatusReportCategoryDto>(),
                QuestionCollection = r.QuestionCollection?.Entities
                    .Select(EntityToDtoMapper.ToStatusReportQuestionItemDto).ToArray()
                    ?? Array.Empty<StatusReportQuestionItemDto>(),
                MultipleChoiceCollection = r.MultipleChoiceCollection?.Entities
                    .Select(EntityToDtoMapper.ToStatusReportMcQuestionDto).ToArray()
                    ?? Array.Empty<StatusReportMcQuestionDto>(),
                ProgramTypeCollection = r.ProgramTypeCollection?.Entities
                    .Select(EntityToDtoMapper.ToStatusReportProgramTypeDto).ToArray()
                    ?? Array.Empty<StatusReportProgramTypeDto>(),
                ChildQuestionCollection = r.ChildQuestionCollection?.Entities
                    .Select(EntityToDtoMapper.ToStatusReportChildQuestionDto).ToArray()
                    ?? Array.Empty<StatusReportChildQuestionDto>(),
                ChildMultipleChoiceCollection = r.ChildMultipleChoiceCollection?.Entities
                    .Select(EntityToDtoMapper.ToStatusReportChildMcQuestionDto).ToArray()
                    ?? Array.Empty<StatusReportChildMcQuestionDto>(),
                // AnswerCollection filled in separately if a data collection exists
                AnswerCollection = Array.Empty<StatusReportAnswerItemDto>()
            };
        }

        private static IActionResult BuildCsvFile(
            EntityCollection lineItems, string csvHeaders, string programName, string contractName)
        {
            var groupedByQuestion = lineItems.Entities
                .GroupBy(e => GetEntityReferenceId(e, "vsd_questionid"));

            var csv = new StringBuilder();
            csv.AppendLine("Contract Number: " + contractName);
            csv.AppendLine("Program Name: " + programName);
            csv.AppendLine(csvHeaders);

            foreach (var questionGroup in groupedByQuestion)
            {
                var firstAnswer = questionGroup.First();
                var row = new StringBuilder("\"");
                row.Append(firstAnswer.GetAttributeValue<string>("vsd_name"));
                row.Append("\",");

                foreach (var answerEntity in questionGroup)
                    row.Append(GetLineItemAnswer(answerEntity) + ",");

                csv.AppendLine(row.ToString());
            }

            byte[] fileBytes = Encoding.UTF8.GetBytes(csv.ToString());
            return new FileStreamResult(new MemoryStream(fileBytes), "text/csv")
            {
                FileDownloadName = ".csv"
            };
        }

        private static string GetLineItemAnswer(Entity entity)
        {
            var yesNo = entity.GetAttributeValue<OptionSetValue>("vsd_yesno");
            if (yesNo != null) return yesNo.Value.ToString();

            var text = entity.GetAttributeValue<string>("vsd_textanswer");
            if (!string.IsNullOrEmpty(text)) return text;

            var number = entity.GetAttributeValue<double?>("vsd_number");
            if (number.HasValue) return number.Value.ToString();

            return string.Empty;
        }

        private static string GetEntityReferenceId(Entity entity, string attributeName)
        {
            if (entity.Contains(attributeName) && entity[attributeName] is EntityReference er)
                return er.Id.ToString();
            return null;
        }
    }
}