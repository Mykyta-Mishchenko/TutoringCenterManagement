import { Routes } from '@angular/router';
import { CanActivateAuthorizedPages } from './core/guards/auth.guard';
import { HomeComponent } from './features/components/home/home.component';
import { routes as authorizationRoutes} from './features/components/authorization/authorization.route'
import { CanActivateSignPages } from './core/guards/sign-in.guard';
import { ProfileComponent } from './features/components/profile/profile.component';
import { TeachersListComponent } from './features/components/teachers/teachers-list/teachers-list.component';

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
        component: ProfileComponent
    }
];
