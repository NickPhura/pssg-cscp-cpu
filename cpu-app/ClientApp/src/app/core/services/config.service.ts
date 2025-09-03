import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { firstValueFrom, Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Configuration } from '../models/configuration.interface';

@Injectable({
  providedIn: 'root'
})
export class ConfigService{
  private configuration: Configuration | null = null;

  baseUrl = environment.apiRootUrl;
  apiPath = this.baseUrl.endsWith('/') ? `${this.baseUrl}api/Configuration` : `${this.baseUrl}/api/Configuration`;
  headers: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  constructor(
    private http: HttpClient,
  ) { }

  public async load(): Promise<Configuration> {
    if (this.configuration) {
      // Already loaded — avoids another API call
      return this.configuration;
    }

    try {
      console.log('Fetching configuration from:', this.apiPath);

      const config = await firstValueFrom(
      this.http
        .get<Configuration>(this.apiPath, { headers: this.headers })
        .pipe(catchError(this.handleError))
      );

      this.configuration = config;
      return config;
    } catch (error) {
      this.handleError(error);
      throw error;
    }
  }

  /**
   * Convenience getter for isProdCpu flag
   */
  public get isProdCpu(): boolean {
    return this.configuration?.isProdCpu ?? false;
  }

  public get featureHideReportSaveButton(): boolean {
   return this.configuration?.featureHideReportSaveButton ?? true;
  }

  protected handleError(err: any): Observable<never> {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = err.error.message;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}, body was: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
