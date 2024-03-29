import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from './../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RouteService {

  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = `${environment.apiUrl}/ticketing/routes`;
  }

  searchRoutes(term: string) {
    // TODO: implement search on backend
    return this.http.get(`${this.apiUrl}/search`);
  }
}
