import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { authInterceptor, provideAuth } from 'angular-auth-oidc-client';
import { authConfig } from './auth/auth.config';
import { provideHttpClient, withInterceptors } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideAuth(authConfig), provideHttpClient(withInterceptors([authInterceptor()])),]
};
