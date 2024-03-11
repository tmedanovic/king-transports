import { Component } from '@angular/core';
import { TicketsBrowseComponent } from '../tickets-browse/tickets-browse.component';
import { TicketEditComponent } from '../ticket-edit/ticket-edit.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [TicketsBrowseComponent, TicketEditComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
