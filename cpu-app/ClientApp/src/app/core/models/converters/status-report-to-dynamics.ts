import { TransmogrifierStatusReport } from "../transmogrifier-status-report.class";
import { iDynamicsPostStatusReport, iDynamicsAnswer } from "../dynamics-post";
import { iQuestionCollection } from "../question-collection.interface";
import { iQuestion } from "../status-report-question.interface";
import { months as monthDict } from "../../constants/month-codes";
import { boolOptionSet } from "../../constants/bool-optionset-values";

export function convertStatusReportToDynamics(
  trans: TransmogrifierStatusReport
): iDynamicsPostStatusReport {
  const types = {
    number: 100000000,
    boolean: 100000001,
    string: 100000002,
  };
  // build the answers into a flatter dynamics form
  const answers: iDynamicsAnswer[] = [];

  const typeHandlers: Record<
    string,
    (q: iQuestion, li: iDynamicsAnswer) => void
  > = {
    number: (q, li) => {
      li["vsd_number"] = q.number != null ? q.number : null;
    },
    boolean: (q, li) => {
      li["vsd_yesno"] =
        q.boolean != null
          ? q.boolean
            ? boolOptionSet.isTrue
            : boolOptionSet.isFalse
          : null;
    },
    string: (q, li) => {
      li["vsd_textanswer"] = q.string != null ? q.string : null;
    },
  };

  trans.statusReportQuestions.forEach((srq: iQuestionCollection) => {
    // for each question assemble shared elements
    srq.questions.forEach((q: iQuestion) => {
      const lineItem: iDynamicsAnswer = {
        vsd_name: q.label,
        vsd_questioncategory: srq.name,
        vsd_QuestionIdfortunecookiebind: q.uuid,
        vsd_CategoryIdfortunecookiebind: q.categoryID,
        vsd_questionorder: Math.floor(q.questionNumber),
        vsd_questiontype1: types[q.type],
      };
      // Dynamically handle type-specific value property
      const handler = typeHandlers[q.type];
      if (handler) {
        handler(q, lineItem);
      }

      // add the line item to the answers list
      answers.push(lineItem);
    });
  });

  return {
    BusinessBCeID: trans.organizationId,
    UserBCeID: trans.userId,
    DataCollectionid: trans.DataCollectionid,
    // ReportingPeriod: monthDict[trans.reportingPeriod] || 0,
    // get rid of any answers are missing a value. Otherwise dynamics 204's.
    AnswerCollection: answers, //.filter(v => v.vsd_yesno || v.vsd_textanswer || v.vsd_number)
  };
}
