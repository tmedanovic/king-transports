import { AsyncPipe } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoginResponse, OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AsyncPipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  
  title = 'ticket-booth';
  userData$;

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
        const { isAuthenticated, userData, accessToken, idToken, configId } =
          loginResponse;

          if(!loginResponse.isAuthenticated)
          this.oidcSecurityService.authorize();
      });
  }
}
