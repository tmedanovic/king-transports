import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TicketingService {
 
  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = 'http://localhost:5050/ticketing/tickets';
  }

  getAllTickets(page: number = 1) {
    return this.http.get(this.apiUrl, {observe: 'response', params: { page }}).pipe(map(response =>({
      items: response.body,
      pagination: JSON.parse(<string>response.headers.get('Pagination'))
    })));
  }

  refundTicket(ticketId: string) {
    return this.http.post(`${this.apiUrl}/${ticketId}/refund`, {});
  }
}
