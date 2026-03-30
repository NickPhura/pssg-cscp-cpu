import { Injectable, inject } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError, retry } from "rxjs/operators";
import { BudgetProposalDto, BudgetProposalPost } from "../api/models";
import { DynamicsBudgetProposalService } from "../api/services/dynamics-budget-proposal/dynamics-budget-proposal.service";

@Injectable({
  providedIn: "root",
})
export class BudgetProposalService {
  private readonly dynamicsBudgetProposalService = inject(
    DynamicsBudgetProposalService,
  );

  getBudgetProposal(
    organizationId: string,
    userId: string,
    contractId: string,
  ): Observable<BudgetProposalDto> {
    return this.dynamicsBudgetProposalService
      .getApiDynamicsBudgetProposalBusinessBceidUserBceidContractId(
        organizationId,
        userId,
        contractId,
      )
      .pipe(retry(3), catchError(this.handleError));
  }

  setBudgetProposal(budgetProposal: BudgetProposalPost): Observable<void> {
    return this.dynamicsBudgetProposalService
      .postApiDynamicsBudgetProposal(budgetProposal)
      .pipe(retry(3), catchError(this.handleError));
  }

  protected handleError(err): Observable<never> {
    let errorMessage = "";
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = err.error.message;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}, body was: ${err.message}`;
    }
    return throwError(errorMessage);
  }
}
