import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';

export const routes: Routes = [
        { path: '', redirectTo: 'home', pathMatch: 'full' }, //default route
        { path: 'home', component: HomeComponent },
        { path: 'unauthorized', component: UnauthorizedComponent },
];
