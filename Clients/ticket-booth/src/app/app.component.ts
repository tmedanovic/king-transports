import { AsyncPipe } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
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

  constructor(public oidcSecurityService: OidcSecurityService, private router: Router) {
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
      });
  }

  public logout() {
    this.oidcSecurityService.logoffAndRevokeTokens().subscribe({complete: () => {
      this.router.navigate(['/unauthorized']);
    }});
  }
}
