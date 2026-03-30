using Database.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;

namespace Gov.Cscp.Victims.Public.Models
{
    public static class ExpenseReportRequestMapper
    {
        public static Vsd_SetCpuOrgContractsRequest ToRequest(ExpenseReportPost model)
        {
            var request = new Vsd_SetCpuOrgContractsRequest
            {
                BusinessBcEId = model.BusinessBCeID,
                UserBcEId = model.UserBCeID
            };

            if (model.ScheduleGCollection != null)
                request.ScheduleGcOlLection = new EntityCollection(
                    model.ScheduleGCollection.Select(MapScheduleG).ToList());

            if (model.ScheduleGLineItemCollection != null)
                request.ScheduleGLineItemCollection = new EntityCollection(
                    model.ScheduleGLineItemCollection.Select(MapScheduleGLineItem).ToList());

            return request;
        }

        private static Entity MapScheduleG(DynamicsScheduleGCollectionPost sg)
        {
            var entity = new Entity("vsd_scheduleg");

            if (Guid.TryParse(sg.vsd_schedulegid, out var id))
                entity.Id = id;

            entity["vsd_actualhoursthisquarter"] = sg.vsd_actualhoursthisquarter;
            entity["vsd_contractedservicehrsthisquarter"] = sg.vsd_contractedservicehrsthisquarter;
            entity["vsd_programadministrationcurrentquarter"] = sg.vsd_programadministrationcurrentquarter;
            entity["vsd_quarterlyvarianceprogramadministration"] = sg.vsd_quarterlyvarianceprogramadministration;
            entity["vsd_yeartodateprogramadministration"] = sg.vsd_yeartodateprogramadministration;
            entity["vsd_yeartodatevarianceprogramadministration"] = sg.vsd_yeartodatevarianceprogramadministration;
            entity["vsd_programdeliverycurrentquarter"] = sg.vsd_programdeliverycurrentquarter;
            entity["vsd_quarterlyvarianceprogramdelivery"] = sg.vsd_quarterlyvarianceprogramdelivery;
            entity["vsd_yeartodateprogramdelivery"] = sg.vsd_yeartodateprogramdelivery;
            entity["vsd_yeartodatevarianceprogramdelivery"] = sg.vsd_yeartodatevarianceprogramdelivery;
            entity["vsd_salariesbenefitscurrentquarter"] = sg.vsd_salariesbenefitscurrentquarter;
            entity["vsd_quarterlyvariancesalariesbenefits"] = sg.vsd_quarterlyvariancesalariesbenefits;
            entity["vsd_yeartodatesalariesandbenefits"] = sg.vsd_yeartodatesalariesandbenefits;
            entity["vsd_yeartodatevariancesalariesbenefits"] = sg.vsd_yeartodatevariancesalariesbenefits;
            entity["vsd_reportreviewed"] = sg.vsd_reportreviewed;

            if (sg.vsd_programadministrationexplanation != null)
                entity["vsd_programadministrationexplanation"] = sg.vsd_programadministrationexplanation;
            if (sg.vsd_programdeliveryexplanations != null)
                entity["vsd_programdeliveryexplanations"] = sg.vsd_programdeliveryexplanations;
            if (sg.vsd_salariesandbenefitsexplanation != null)
                entity["vsd_salariesandbenefitsexplanation"] = sg.vsd_salariesandbenefitsexplanation;

            return entity;
        }

        private static Entity MapScheduleGLineItem(DynamicsScheduleGLineItemCollectionPost item)
        {
            var entity = new Entity("vsd_scheduleglineitem");

            if (Guid.TryParse(item.vsd_scheduleglineitemid, out var id))
                entity.Id = id;

            entity["vsd_actualexpensescurrentquarter"] = item.vsd_actualexpensescurrentquarter;
            entity["vsd_quarterlyvariance"] = item.vsd_quarterlyvariance;
            entity["vsd_actualexpendituresyeartodate"] = item.vsd_actualexpendituresyeartodate;
            entity["vsd_yeartodatevariance"] = item.vsd_yeartodatevariance;

            if (item.vsd_explanationforvariance != null)
                entity["vsd_explanationforvariance"] = item.vsd_explanationforvariance;

            return entity;
        }
    }
}
