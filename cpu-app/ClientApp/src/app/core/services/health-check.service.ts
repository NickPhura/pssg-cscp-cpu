import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of } from 'rxjs';

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
  providedIn: 'root',
})
export class HealthCheckService {
  private _healthy = false;

  get isHealthy(): boolean {
    return this._healthy;
  }

  constructor(private http: HttpClient) {}

  /** Called once at app init via APP_INITIALIZER. */
  initialize(): Promise<void> {
    return this.http
      .get<HealthCheckResponse>('/hc')
      .pipe(
        map((response) => {
          this._healthy = response?.status?.toLowerCase() === 'healthy';
        }),
        catchError(() => {
          this._healthy = false;
          return of(undefined);
        })
      )
      .toPromise()
      .then(() => undefined);
  }
}
