import { Component } from '@angular/core';
import { TicketingService } from '../ticketing.service';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { Pagination } from '../util/pagination';
import { NgbPagination } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-tickets-browse',
  standalone: true,
  imports: [ CurrencyPipe, DatePipe, NgbPagination, CommonModule ],
  templateUrl: './tickets-browse.component.html',
  styleUrl: './tickets-browse.component.scss'
})
export class TicketsBrowseComponent {

  public tickets: any;
  public currentPage: number = 1;
  public pagination?: Pagination = {
    currentPage: 1,
    totalItems: 0,
    itemsPerPage: 10,
    totalPages: 1
  };

  constructor(private ticketingService: TicketingService) {
    this.loadTicketsPage(1);
  }

  public loadTicketsPage(page: number) {
    this.ticketingService.getAllTickets(page).subscribe((ticketsPagedResponse: any) => {
      console.log(ticketsPagedResponse);

      this.tickets = ticketsPagedResponse.items;
      this.pagination = ticketsPagedResponse.pagination;
      this.currentPage = ticketsPagedResponse.pagination.currentPage;
    });
  }

  public refundTicket(ticket: any) {
    this.ticketingService.refundTicket(ticket.ticketId).subscribe((r) => {
      ticket.refunded = true;
    });
  }

  private updateTicketRow(updatedTicket: any) {
    let indexToUpdate = this.tickets.findIndex((item: { ticketId: any; }) => item.ticketId === updatedTicket.ticketId);
    this.tickets[indexToUpdate] = updatedTicket;
  }
}
