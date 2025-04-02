import { Routes } from '@angular/router';
import { CanActivateAuthorizedPages } from './core/guards/auth.guard';
import { HomeComponent } from './features/home/home.component';
import { routes as authorizationRoutes} from './features/authorization/authorization.route'
import { CanActivateSignPages } from './core/guards/sign-in.guard';

export const routes: Routes = [
    {
        path: '',
        canActivate: [CanActivateAuthorizedPages],
        component: HomeComponent
    },
    {
        path: 'auth',
        canActivate: [CanActivateSignPages],
        children: authorizationRoutes
    }
];
