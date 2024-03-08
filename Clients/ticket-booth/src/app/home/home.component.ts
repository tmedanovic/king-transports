import { Component } from '@angular/core';
import { TicketsBrowseComponent } from '../tickets-browse/tickets-browse.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [TicketsBrowseComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
