using Database.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;

namespace Gov.Cscp.Victims.Public.Models
{
    public static class StatusReportRequestMapper
    {
        /// <summary>
        /// Maps MonthlyStatisticsAnswers to Vsd_SetCpuMonthlyStatisticsAnswersRequest.
        /// The DynamicsDataCollectionLineItemPost.vsd_QuestionIdfortunecookiebind property
        /// holds the raw GUID set by the FE (e.g. the question uuid).
        /// </summary>
        public static Vsd_SetCpuMonthlyStatisticsAnswersRequest ToSetAnswersRequest(
            MonthlyStatisticsAnswers model, Guid taskId)
        {
            var request = new Vsd_SetCpuMonthlyStatisticsAnswersRequest
            {
                Target = new EntityReference("task", taskId),
                UserBcEId = model.UserBCeID,
                BusinessBcEId = model.BusinessBCeID,
                StatusCode = model.StatusCode ?? 0
            };

            if (model.AnswerCollection != null)
            {
                var answerCollection = new EntityCollection();
                foreach (var answer in model.AnswerCollection)
                    answerCollection.Entities.Add(MapAnswerToEntity(answer));
                request.AnswerCollection = answerCollection;
            }

            return request;
        }

        private static Entity MapAnswerToEntity(DynamicsDataCollectionLineItemPost answer)
        {
            var entity = new Entity("vsd_datacollectionlineitem");

            if (!string.IsNullOrEmpty(answer.vsd_name))
                entity["vsd_name"] = answer.vsd_name;

            if (!string.IsNullOrEmpty(answer.vsd_questioncategory))
                entity["vsd_questioncategory"] = answer.vsd_questioncategory;

            if (answer.vsd_questionorder != 0)
                entity["vsd_questionorder"] = answer.vsd_questionorder;

            if (answer.vsd_questiontype1 != 0)
                entity["vsd_questiontype1"] = new OptionSetValue(answer.vsd_questiontype1);

            if (answer.vsd_number.HasValue)
                entity["vsd_number"] = answer.vsd_number.Value;

            if (!string.IsNullOrEmpty(answer.vsd_textanswer))
                entity["vsd_textanswer"] = answer.vsd_textanswer;

            if (answer.vsd_yesno.HasValue)
                entity["vsd_yesno"] = new OptionSetValue(answer.vsd_yesno.Value);

            // vsd_QuestionIdfortunecookiebind getter returns "/vsd_cpustatisticsmasterdatas(GUID)"
            // the backing field was set with the raw GUID from the FE  
            var questionBindPath = answer.vsd_QuestionIdfortunecookiebind;
            var questionGuid = ParseBindGuid(questionBindPath);
            if (questionGuid.HasValue)
                entity["vsd_questionid"] = new EntityReference("vsd_cpustatisticsmasterdata", questionGuid.Value);

            // vsd_CategoryIdfortunecookiebind getter returns "/vsd_monthlystatisticscategories(GUID)"
            var categoryBindPath = answer.vsd_CategoryIdfortunecookiebind;
            var categoryGuid = ParseBindGuid(categoryBindPath);
            if (categoryGuid.HasValue)
                entity["vsd_categoryid"] = new EntityReference("vsd_monthlystatisticscategory", categoryGuid.Value);

            return entity;
        }

        /// <summary>Parses a GUID from an OData bind path like "/vsd_entities(GUID)".</summary>
        private static Guid? ParseBindGuid(string bindPath)
        {
            if (string.IsNullOrEmpty(bindPath)) return null;
            var guidStr = bindPath.TrimEnd(')').Split('(').LastOrDefault();
            return Guid.TryParse(guidStr, out var g) ? g : (Guid?)null;
        }
    }
}
