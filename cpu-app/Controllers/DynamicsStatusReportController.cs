using Gov.Cscp.Victims.Public.Models;
using Gov.Cscp.Victims.Public.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Serilog;
using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Manager.Contract;
using System.Web;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DynamicsStatusReportController : Controller
    {
        private readonly IDynamicsResultService _dynamicsResultService;
        private readonly ILogger _logger;

        public DynamicsStatusReportController(IDynamicsResultService dynamicsResultService)
        {
            this._dynamicsResultService = dynamicsResultService;
            _logger = Log.Logger;
        }

        [HttpGet("{businessBceid}/{userBceid}/{taskId}")]
        public async Task<IActionResult> GetQuestions(string businessBceid, string userBceid, string taskId)
        {
            try
            {
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                string endpointUrl = "tasks(" + taskId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUMonthlyStatisticsQuestions";

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);
                var contractValue = result.result["Contract"]["vsd_contractid"];
                var programValue = result.result["Program"]["vsd_programid"];
                var reportingPeriodValue = result.result["ReportingPeriod"];

                string endpointUrl1 = "vsd_datacollections?$select=vsd_datacollectionid&$filter=(_vsd_contract_value eq " + contractValue + ") and (_vsd_program_value eq " + programValue + ") and (vsd_reportingperiod eq " + reportingPeriodValue +")";

                HttpClientResult result2 = await _dynamicsResultService.Get(endpointUrl1);
                

                if(result2.result["value"].HasValues)
                {
                    var dataCollectionId = result2.result["value"]?.Last()["vsd_datacollectionid"];
                    string requestJson2 = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                    string endpointUrl2 = "vsd_datacollections(" + dataCollectionId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUMonthlyStatisticsAnswers";

                    HttpClientResult result3 = await _dynamicsResultService.Post(endpointUrl2, requestJson2);

                    result.result.Add("AnswerCollection", result3.result["AnswerCollection"]);
                }


                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting stats questions info 'vsd_GetCPUMonthlyStatisticsQuestions'. Task id = {taskId}. Source = CPU");
                return BadRequest();
            }
            finally { }
        }
        [HttpGet("monthly_stats/{businessBceid}/{userBceid}/{contractId}")]
        public async Task<IActionResult> GetMonthlyStats(string businessBceid, string userBceid, string contractId)
        {
            try
            {
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                string endpointUrl = "vsd_contracts(" + contractId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUMonthlyStatistics";

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting monthly stats info 'vsd_GetCPUMonthlyStatistics'. Contract id = {contractId}. Source = CPU");
                return BadRequest();
            }
            finally { }
        }
        [HttpGet("data_collection/{businessBceid}/{userBceid}/{dataCollectionId}")]
        public async Task<IActionResult> GetSubmittedReport(string businessBceid, string userBceid, string dataCollectionId)
        {
            try
            {
                string requestJson = "{\"UserBCeID\":\"" + userBceid + "\",\"BusinessBCeID\":\"" + businessBceid + "\"}";
                string endpointUrl = "vsd_datacollections(" + dataCollectionId + ")/Microsoft.Dynamics.CRM.vsd_GetCPUMonthlyStatisticsAnswers";

                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, requestJson);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting submitted stats info 'vsd_GetCPUMonthlyStatisticsAnswers'. Data collection id = {dataCollectionId}. Source = CPU");
                return BadRequest();
            }
            finally { }
        }

        [HttpPost("{taskId}")]
        public async Task<IActionResult> AnswerQuestions([FromBody] MonthlyStatisticsAnswers model, string taskId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string messages = string.Join("\n", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                    _logger.Error(new Exception(messages), $"API call to 'AnswerQuestions' made with invalid model state. Error is:\n{messages}\nSource = CPU");
                    return BadRequest(ModelState);
                }
                string endpointUrl = "tasks(" + taskId + ")/Microsoft.Dynamics.CRM.vsd_SetCPUMonthlyStatisticsAnswers";
                string modelString = System.Text.Json.JsonSerializer.Serialize(model);
                modelString = Helpers.Helpers.updateFortunecookieBindNull(modelString);
                HttpClientResult result = await _dynamicsResultService.Post(endpointUrl, modelString);

                return StatusCode((int)result.statusCode, result.result.ToString());
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unexpected error while submitting stats answers. Source = CPU");
                return BadRequest();
            }
            finally { }
        }

        [HttpGet("export_monthly_report/{contractId}/{programId}/{contractNumber}/{programName}")]
        public async Task<IActionResult> ExportMonthlyReport(string contractId, string programId, string contractNumber, string programName)
        {
            try
            {
                string endpointUrl = "vsd_datacollections?$select=vsd_datacollectionid,vsd_reportingperiod&$filter=(_vsd_contract_value eq " + contractId + ") and (_vsd_program_value eq " + programId + " and statuscode ne 100000004)";

                HttpClientResult result = await _dynamicsResultService.Get(endpointUrl);

                if (result.result["value"].Count() == 0)
                {
                    return NoContent();
                }

                var reports = result.result["value"]
                .OrderBy(item => item["vsd_reportingperiod"].ToObject<int>() >= (int)MonthEnum.April && item["vsd_reportingperiod"].ToObject<int>() <= (int)MonthEnum.December ? 0 : 1)
                .ThenBy(item => item["vsd_reportingperiod"].ToObject<int>())
                .Select(item => new { collectionId = item["vsd_datacollectionid"].ToString(), reportingPeriodId = item["vsd_reportingperiod"].ToObject<int>() })
                .ToArray();


                string endpointUrl2 = "vsd_datacollectionlineitems?$select=vsd_yesno, vsd_textanswer, vsd_number, vsd_name, _vsd_questionid_value&$filter=(_vsd_datacollectionid_value eq " + string.Join(" or _vsd_datacollectionid_value eq ", reports.Select(d => d.collectionId)) + ")";
                HttpClientResult result2 = await _dynamicsResultService.Get(endpointUrl2);

                var reportCVSHeaders = "," + string.Join(",", reports.Select(d => (MonthEnum)d.reportingPeriodId));


                return ConvertJsonToCsvAsync(result2.result, reportCVSHeaders, programName, contractNumber);

            }
            catch (Exception e)
            {
                _logger.Error(e, $"Unexpected error while getting monthly stats info 'vsd_GetCPUMonthlyStatistics'. Contract id = {contractId}. Source = CPU");
                return BadRequest();
            }
            finally { }
        }

        private IActionResult ConvertJsonToCsvAsync(JObject answerCollection, string reportCVSHeaders, string programName, string contractName)
        {
            var exportDataList = JsonConvert.DeserializeObject<List<DynamicsDataCollectionLineItemPost>>(answerCollection["value"].ToString());
            var groupedQuestions = exportDataList.GroupBy(d => d._vsd_questionid_value);

            StringBuilder csvData = new StringBuilder();
            csvData.AppendLine("Contract Number: " + contractName);
            csvData.AppendLine("Program Name: " + programName);
            csvData.AppendLine(reportCVSHeaders);

            var cvsRow = "\"";
            foreach (var question in groupedQuestions)
            {
                cvsRow = cvsRow + question.First().vsd_name + "\",";
                foreach (var answer in question)
                {

                    cvsRow = cvsRow + answer.GetAnswer() + ",";
                }
                csvData.AppendLine(cvsRow);
                cvsRow = "\"";
            }

            byte[] fileBytes = Encoding.UTF8.GetBytes(csvData.ToString());
            var stream = new MemoryStream(fileBytes);

            return File(stream, "text/csv", ".csv");
        }
    }
}