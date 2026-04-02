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
                    model.ScheduleGCollection.Select(MapScheduleG).Cast<Entity>().ToList());

            if (model.ScheduleGLineItemCollection != null)
                request.ScheduleGLineItemCollection = new EntityCollection(
                    model.ScheduleGLineItemCollection.Select(MapScheduleGLineItem).Cast<Entity>().ToList());

            return request;
        }

        private static Vsd_ScheduleG MapScheduleG(DynamicsScheduleGCollectionPost sg)
        {
            var entity = new Vsd_ScheduleG();

            if (Guid.TryParse(sg.vsd_schedulegid, out var id))
                entity.Id = id;

            entity.Vsd_ActualHoursThisQuarter = sg.vsd_actualhoursthisquarter;
            entity["vsd_contractedservicehrsthisquarter"] = sg.vsd_contractedservicehrsthisquarter; // read-only computed property
            entity.Vsd_ProgramAdministrationCurrentQuarter = new Money(sg.vsd_programadministrationcurrentquarter);
            entity["vsd_quarterlyvarianceprogramadministration"] = sg.vsd_quarterlyvarianceprogramadministration; // read-only computed property
            entity.Vsd_YearToDateProgramAdministration = new Money(sg.vsd_yeartodateprogramadministration);
            entity["vsd_yeartodatevarianceprogramadministration"] = sg.vsd_yeartodatevarianceprogramadministration; // read-only computed property
            entity.Vsd_ProgramDeliveryCurrentQuarter = new Money(sg.vsd_programdeliverycurrentquarter);
            entity["vsd_quarterlyvarianceprogramdelivery"] = sg.vsd_quarterlyvarianceprogramdelivery; // read-only computed property
            entity.Vsd_YearToDateProgramDelivery = new Money(sg.vsd_yeartodateprogramdelivery);
            entity["vsd_yeartodatevarianceprogramdelivery"] = sg.vsd_yeartodatevarianceprogramdelivery; // read-only computed property
            entity.Vsd_SalariesBenefitsCurrentQuarter = new Money(sg.vsd_salariesbenefitscurrentquarter);
            entity["vsd_quarterlyvariancesalariesbenefits"] = sg.vsd_quarterlyvariancesalariesbenefits; // read-only computed property
            entity.Vsd_YearToDatesAlAriesAndBenefits = new Money(sg.vsd_yeartodatesalariesandbenefits);
            entity["vsd_yeartodatevariancesalariesbenefits"] = sg.vsd_yeartodatevariancesalariesbenefits; // read-only computed property
            entity.Vsd_ReportReviewed = sg.vsd_reportreviewed;

            if (sg.vsd_programadministrationexplanation != null)
                entity.Vsd_ProgramAdministrationExplanation = sg.vsd_programadministrationexplanation;
            if (sg.vsd_programdeliveryexplanations != null)
                entity.Vsd_ProgramDeliveryExplanations = sg.vsd_programdeliveryexplanations;
            if (sg.vsd_salariesandbenefitsexplanation != null)
                entity.Vsd_SalariesAndBenefitsExplanation = sg.vsd_salariesandbenefitsexplanation;

            return entity;
        }

        private static Vsd_ScheduleGLineItem MapScheduleGLineItem(DynamicsScheduleGLineItemCollectionPost item)
        {
            var entity = new Vsd_ScheduleGLineItem();

            if (Guid.TryParse(item.vsd_scheduleglineitemid, out var id))
                entity.Id = id;

            entity.Vsd_ActualExpensesCurrentQuarter = new Money(item.vsd_actualexpensescurrentquarter);
            entity["vsd_quarterlyvariance"] = item.vsd_quarterlyvariance; // read-only computed property
            entity.Vsd_ActualExpendituresYearToDate = new Money(item.vsd_actualexpendituresyeartodate);
            entity["vsd_yeartodatevariance"] = item.vsd_yeartodatevariance; // read-only computed property

            if (item.vsd_explanationforvariance != null)
                entity.Vsd_ExplanationForVariance = item.vsd_explanationforvariance;

            return entity;
        }
    }
}
