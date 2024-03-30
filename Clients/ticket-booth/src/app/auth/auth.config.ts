import { LogLevel, PassedInitialConfig } from 'angular-auth-oidc-client';
import { environment } from './../../environments/environment';

export const authConfig: PassedInitialConfig = {
  config: {
              authority: `${environment.apiUrl}/auth`,
              redirectUrl: window.location.origin,
              postLogoutRedirectUri: window.location.origin,
              clientId: 'ticket-booth-spa',
              scope: 'openid profile offline_access ticket.issue ticket.validate', // 'openid profile offline_access ' + your scopes
              responseType: 'code',
              silentRenew: true,
              useRefreshToken: true,
              renewTimeBeforeTokenExpiresInSeconds: 30,
              secureRoutes: [environment.apiUrl],
              unauthorizedRoute : '/login',
              logLevel: LogLevel.Error
          }
}
