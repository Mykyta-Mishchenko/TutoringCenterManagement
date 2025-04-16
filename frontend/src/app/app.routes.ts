import { Routes } from '@angular/router';
import { CanActivateAuthorizedPages } from './core/guards/auth.guard';
import { routes as authorizationRoutes} from './features/components/authorization/authorization.route'
import { CanActivateSignPages } from './core/guards/sign-in.guard';
import { TeachersListComponent } from './features/components/teachers/teachers-list/teachers-list.component';
import { ProfileComponent } from './features/components/lessons/user-info/profile/profile.component';
import { HomeComponent } from './features/components/lessons/user-info/home/home.component';
import { ReportsComponent } from './features/components/reports/reports/reports.component';

export const routes: Routes = [
    {
        path: '',
        canActivate: [CanActivateAuthorizedPages],
        component: TeachersListComponent
    },
    {
        path: 'auth',
        canActivate: [CanActivateSignPages],
        children: authorizationRoutes
    },
    {
        path: 'profile',
        canActivate: [CanActivateAuthorizedPages],
        component: HomeComponent
    },
    {
        path: 'reports',
        canActivate: [CanActivateAuthorizedPages],
        component: ReportsComponent
    }
];
