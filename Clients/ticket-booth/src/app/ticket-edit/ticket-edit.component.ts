import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouteService } from '../route.service';
import { CommonModule } from '@angular/common';
import { TicketingService } from '../ticketing.service';

@Component({
  selector: 'app-ticket-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './ticket-edit.component.html',
  styleUrl: './ticket-edit.component.scss'
})
export class TicketEditComponent implements OnInit {

  ticketForm: FormGroup = this.formBuilder.group({
    routeId: ['', [Validators.required]]
  });

  isSubmitted = false;
  routes: Array<any> = [];

  constructor(
    private formBuilder: FormBuilder, 
    private ticketingService: TicketingService,
    private routeService: RouteService) {
  }

  ngOnInit(): void {
    this.searchRoutes();
  }

  public searchRoutes() {
    this.routeService.searchRoutes('').subscribe((routes: any) => {
      console.log(routes);

      this.routes = routes;
    });
  }

  onSubmit(): void {
    let ticket = {
      routeId: this.ticketForm.value.routeId,
    };
    console.log(ticket);

    this.isSubmitted = true;
    if (this.ticketForm.valid) {
      this.ticketingService.createTicket(ticket);
    } else {
      console.error("Validation failed")
    }
  }
}
