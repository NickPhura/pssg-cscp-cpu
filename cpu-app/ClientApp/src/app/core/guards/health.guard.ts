import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from "@angular/router";
import { HealthCheckService } from "../services/health-check.service";

@Injectable({
  providedIn: "root",
})
export class HealthGuard {
  constructor(
    private healthCheckService: HealthCheckService,
    private router: Router,
  ) {}

  canActivate(
    _route: ActivatedRouteSnapshot,
    _state: RouterStateSnapshot,
  ): boolean | UrlTree {
    if (this.healthCheckService.isHealthy) {
      return true;
    }
    return this.router.createUrlTree(["/outage"]);
  }
}
