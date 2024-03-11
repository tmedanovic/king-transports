import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RouteService {

  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = 'http://localhost:5050/ticketing/routes';
  }

  searchRoutes(term: string) {
    // TODO: implement search on backend
    return this.http.get(`${this.apiUrl}/search`);
  }
}
