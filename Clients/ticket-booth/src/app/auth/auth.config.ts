import { LogLevel, PassedInitialConfig } from 'angular-auth-oidc-client';

export const authConfig: PassedInitialConfig = {
  config: {
              authority: 'http://localhost:5050/auth',
              redirectUrl: window.location.origin,
              postLogoutRedirectUri: window.location.origin,
              clientId: 'ticket-booth-spa',
              scope: 'openid profile offline_access ticket.issue ticket.validate', // 'openid profile offline_access ' + your scopes
              responseType: 'code',
              silentRenew: true,
              useRefreshToken: true,
              renewTimeBeforeTokenExpiresInSeconds: 30,
              secureRoutes: ['http://localhost:5050'],
              unauthorizedRoute : '/unauthorized',
              logLevel: LogLevel.Debug,
              forbiddenRoute: '/unauthorized'
          }
}
