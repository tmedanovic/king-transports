import { AsyncPipe } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgbDropdown, NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginResponse, OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AsyncPipe, NgbDropdownModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  
  public userData$;
  public isAuthenticated: boolean = false;

  constructor(public oidcSecurityService: OidcSecurityService) {
    this.userData$ = this.oidcSecurityService.userData$;
    this.userData$.subscribe(x => {
      console.log(x.userData);
    })
  }
  
  ngOnInit() {
    this.oidcSecurityService
      .checkAuth()
      .subscribe((loginResponse: LoginResponse) => {

        this.isAuthenticated = loginResponse.isAuthenticated;

        if(!this.isAuthenticated) {
          this.oidcSecurityService.authorize();
        }
      });
  }

  public logout() {
    this.oidcSecurityService.logoffAndRevokeTokens().subscribe(x=> console.log(x), y => console.log(y));
  }
}
