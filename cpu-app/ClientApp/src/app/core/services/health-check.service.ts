import { HttpClient } from "@angular/common/http";
import { Injectable, signal } from "@angular/core";
import { catchError, map, of } from "rxjs";

export interface HealthCheckEntry {
  name: string;
  status: string;
  description: string | null;
}

export interface HealthCheckResponse {
  status: string;
  checks: HealthCheckEntry[];
}

@Injectable({
  providedIn: "root",
})
export class HealthCheckService {
  private readonly _healthy = signal(false);

  /** Read-only signal – consumed by guards and components. */
  readonly isHealthy = this._healthy.asReadonly();

  constructor(private http: HttpClient) {}

  /** Called once at app init via APP_INITIALIZER. */
  initialize(): Promise<void> {
    return this.http
      .get<HealthCheckResponse>("/coastcontracts/hc")
      .pipe(
        map((response) => {
          // All individual checks must be Healthy – mirrors the API logic where
          // the overall HTTP status is driven by the API self-check only, but
          // the FE treats any degraded/unhealthy check as an outage.
          const healthy =
            Array.isArray(response?.checks) &&
            response.checks.every((c) => c.status === "Healthy");
          this._healthy.set(healthy);
        }),
        catchError(() => {
          this._healthy.set(false);
          return of(undefined);
        }),
      )
      .toPromise()
      .then(() => undefined);
  }
}
