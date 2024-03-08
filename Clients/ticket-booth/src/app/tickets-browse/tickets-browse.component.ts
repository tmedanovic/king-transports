import { Component } from '@angular/core';
import { TicketingService } from '../ticketing.service';

@Component({
  selector: 'app-tickets-browse',
  standalone: true,
  imports: [],
  templateUrl: './tickets-browse.component.html',
  styleUrl: './tickets-browse.component.scss'
})
export class TicketsBrowseComponent {

  public tickets: any;

  constructor(private ticketingService: TicketingService)
  {
    this.ticketingService.getAllTickets().subscribe((clientesResponse: any) => {
      console.log(clientesResponse);
      this.tickets = clientesResponse;
    });
  }
}
