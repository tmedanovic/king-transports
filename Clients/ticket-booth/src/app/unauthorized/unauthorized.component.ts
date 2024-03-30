import { Component } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-unauthorized',
  standalone: true,
  imports: [],
  templateUrl: './unauthorized.component.html',
  styleUrl: './unauthorized.component.scss'
})
export class UnauthorizedComponent {

    constructor(public oidcSecurityService: OidcSecurityService) {}

    login() {
      this.oidcSecurityService.authorize(undefined, {
        redirectUrl : window.location.origin
      });
    }
}
