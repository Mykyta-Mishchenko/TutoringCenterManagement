import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { AuthService } from "../services/auth/auth.service";
import { TokenService } from "../services/auth/token.service";
import { Observable } from "rxjs";

@Injectable()
export class JwtInterceptor implements HttpInterceptor{
    private authService = inject(AuthService);
    private tokenService = inject(TokenService);

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const isLoggedIn = this.authService.isLoggedIn;
        if (isLoggedIn()) {
            req = req.clone({
                setHeaders: {
                    Authorization: `Bearer ${this.tokenService.ACCESS_TOKEN()}`
                }
            });   
        }

        return next.handle(req);
    }
}