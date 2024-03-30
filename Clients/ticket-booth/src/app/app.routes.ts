import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { AuthGuard } from './auth/auth.guard';

export const routes: Routes = [
        { path: '', redirectTo: 'home', pathMatch: 'full' }, //default route
        { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
        { path: 'login', component: UnauthorizedComponent },
];
