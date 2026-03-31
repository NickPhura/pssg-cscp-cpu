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

        private static Entity MapSurplusPlan(DynamicsProgramSurplus plan)
        {
            var entity = new Entity("vsd_surplusplanreport");

            if (Guid.TryParse(plan.vsd_surplusplanreportid, out var planId))
                entity.Id = planId;

            entity["vsd_surplusremittance"] = plan.vsd_surplusremittance;

            if (plan.vsd_datesubmitted.HasValue)
                entity["vsd_datesubmitted"] = plan.vsd_datesubmitted.Value;

            return entity;
        }

        private static Entity MapSurplusLineItem(DynamicsProgramSurplusLineItemPost item)
        {
            var entity = new Entity("vsd_surpluslineitem");

            if (Guid.TryParse(item.vsd_surpluslineitemid, out var lineItemId))
                entity.Id = lineItemId;

            if (!string.IsNullOrEmpty(item.vsd_justificationdetails))
                entity["vsd_justificationdetails"] = item.vsd_justificationdetails;

            if (item.vsd_actualexpenditures.HasValue)
                entity["vsd_actualexpenditures"] = new Money(item.vsd_actualexpenditures.Value);

            if (item.vsd_actualexpenditures2.HasValue)
                entity["vsd_actualexpenditures2"] = new Money(item.vsd_actualexpenditures2.Value);

            if (item.vsd_actualexpenditures3.HasValue)
                entity["vsd_actualexpenditures3"] = new Money(item.vsd_actualexpenditures3.Value);

            if (item.vsd_actualexpenditures4.HasValue)
                entity["vsd_actualexpenditures4"] = new Money(item.vsd_actualexpenditures4.Value);

            if (item.vsd_allocatedamount.HasValue)
                entity["vsd_allocatedamount"] = new Money(item.vsd_allocatedamount.Value);

            if (item.vsd_proposedexpenditures.HasValue)
                entity["vsd_proposedexpenditures"] = new Money(item.vsd_proposedexpenditures.Value);

            if (item.vsd_datesubmitted.HasValue)
                entity["vsd_datesubmitted"] = item.vsd_datesubmitted.Value;

            return entity;
        }
    }
}
