import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketsBrowseComponent } from './tickets-browse.component';

describe('TicketsBrowseComponent', () => {
  let component: TicketsBrowseComponent;
  let fixture: ComponentFixture<TicketsBrowseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketsBrowseComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TicketsBrowseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
