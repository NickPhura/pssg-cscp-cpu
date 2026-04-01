using Database.Model;
using Microsoft.Xrm.Sdk;
using System;

namespace Gov.Cscp.Victims.Public.Models
{
    public static class ProgramSurplusRequestMapper
    {
        public static Vsd_SetCpuOrgContractsRequest ToSetSurplusRequest(ProgramSurplusPost model)
        {
            var request = new Vsd_SetCpuOrgContractsRequest
            {
                BusinessBcEId = model.BusinessBCeID,
                UserBcEId = model.UserBCeID
            };

            if (model.SurplusPlanCollection != null)
            {
                var ec = new EntityCollection();
                foreach (var plan in model.SurplusPlanCollection)
                    ec.Entities.Add(MapSurplusPlan(plan));
                request.SurplusPlanCollection = ec;
            }

            if (model.SurplusPlanLineItemCollection != null)
            {
                var ec = new EntityCollection();
                foreach (var item in model.SurplusPlanLineItemCollection)
                    ec.Entities.Add(MapSurplusLineItem(item));
                request.SurplusPlanLineItemCollection = ec;
            }

            return request;
        }

        private static Vsd_SurplusPlanReport MapSurplusPlan(DynamicsProgramSurplus plan)
        {
            var entity = new Vsd_SurplusPlanReport();

            if (Guid.TryParse(plan.vsd_surplusplanreportid, out var planId))
                entity.Id = planId;

            entity.Vsd_SurplusRemittance = plan.vsd_surplusremittance;

            if (plan.vsd_datesubmitted.HasValue)
                entity.Vsd_DateSubmitted = plan.vsd_datesubmitted.Value;

            return entity;
        }

        private static Vsd_SurplusLineItem MapSurplusLineItem(DynamicsProgramSurplusLineItemPost item)
        {
            var entity = new Vsd_SurplusLineItem();

            if (Guid.TryParse(item.vsd_surpluslineitemid, out var lineItemId))
                entity.Id = lineItemId;

            if (!string.IsNullOrEmpty(item.vsd_justificationdetails))
                entity.Vsd_JustificationDetails = item.vsd_justificationdetails;

            if (item.vsd_actualexpenditures.HasValue)
                entity.Vsd_ActualExpenditures = new Money(item.vsd_actualexpenditures.Value);

            if (item.vsd_actualexpenditures2.HasValue)
                entity.Vsd_ActualExpenditures2 = new Money(item.vsd_actualexpenditures2.Value);

            if (item.vsd_actualexpenditures3.HasValue)
                entity.Vsd_ActualExpenditures3 = new Money(item.vsd_actualexpenditures3.Value);

            if (item.vsd_actualexpenditures4.HasValue)
                entity.Vsd_ActualExpenditures4 = new Money(item.vsd_actualexpenditures4.Value);

            if (item.vsd_allocatedamount.HasValue)
                entity.Vsd_AllocatedAmount = new Money(item.vsd_allocatedamount.Value);

            if (item.vsd_proposedexpenditures.HasValue)
                entity.Vsd_ProposedExpenditures = new Money(item.vsd_proposedexpenditures.Value);

            if (item.vsd_datesubmitted.HasValue)
                entity.Vsd_DateSubmitted = item.vsd_datesubmitted.Value;

            return entity;
        }
    }
}
