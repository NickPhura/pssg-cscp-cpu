import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ProgramApplicationDto, ProgramApplicationPost } from "../api/models";
import { ProgramApplicationService as OrvalProgramApplicationService } from "../api/services/program-application/program-application.service";

@Injectable({
  providedIn: "root",
})
export class ProgramApplicationService {
  constructor(private orvalService: OrvalProgramApplicationService) {}

  getProgramApplication(
    organizationId: string,
    userId: string,
    scheduleFId: string,
  ): Observable<ProgramApplicationDto> {
    return this.orvalService.getApiProgramApplicationBusinessBceidUserBceidScheduleFId(
      organizationId,
      userId,
      scheduleFId,
    );
  }

  setProgramApplication(body: ProgramApplicationPost): Observable<void> {
    return this.orvalService.postApiProgramApplication(body);
  }
}
