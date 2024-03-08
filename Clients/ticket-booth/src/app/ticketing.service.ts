import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TicketingService {
 
  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = 'http://localhost:5050/ticketing/tickets';
  }

  getAllTickets() {
    return this.http.get(this.apiUrl);
  }
}
