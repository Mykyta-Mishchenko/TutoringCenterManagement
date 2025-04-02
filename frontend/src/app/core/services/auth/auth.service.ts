import { HttpClient } from "@angular/common/http";
import { computed, inject, Injectable, signal } from "@angular/core";
import { User } from "./models/user.models";
import { SignUpModel } from "./models/sign-up.model";
import { finalize, map, Observable } from "rxjs";
import { SignInModel } from "./models/sign-in.model";
import { TokenResponseModel } from "./models/token.model";
import { jwtDecode } from 'jwt-decode';
import { TokenService } from "./token.service";
import { Router } from "@angular/router";
import { environment } from "../../../../environments/environment";


@Injectable({
    providedIn: 'root'
})
export class AuthService{
  private httpClient = inject(HttpClient);
  private tokenService = inject(TokenService);
  private router = inject(Router);
  private apiUrl = environment.apiUrl;

  private user = signal<User | null>(null);

  User = this.user.asReadonly();

  isLoggedIn = computed(() => this.tokenService.ACCESS_TOKEN() !== null);

  updateUserByToken(accessToken: string | null) {
    this.user.update(user => this.getUserFromToken(accessToken));
  }

    signUp(credentials: SignUpModel): Observable<string>{
        return this.httpClient.post<string>(`${this.apiUrl}/auth/signUp`, credentials);
    }
    
    signIn(credentials: SignInModel): Observable<TokenResponseModel>{
        return this.httpClient.post<TokenResponseModel>(`${this.apiUrl}/auth/signIn`, credentials, { withCredentials: true })
         .pipe(map((response: TokenResponseModel) =>
         {
           this.updateUserByToken(response.accessToken);
           this.tokenService.saveToken(response.accessToken);
           return response;
         }));
    }

    refreshToken(): Observable<TokenResponseModel>{
      return this.httpClient.post<TokenResponseModel>(`${this.apiUrl}/auth/refresh`,  {} ,{ withCredentials: true })
        .pipe(map((response: TokenResponseModel) => {
          this.updateUserByToken(response.accessToken);
          this.tokenService.saveToken(response.accessToken)

          return response;
        })
      );
    }
    
      logout(): Observable<string> {
          return this.httpClient.post<string>(`${this.apiUrl}/auth/logout`, {},{ withCredentials: true }).pipe(
          finalize(() => {
            this.user.set(null);
            this.tokenService.resetAccessToken();
            this.router.navigateByUrl("/auth/signIn");
          })
        );
      }
    
    getUserFromToken(token :string | null): User | null {
        if (!token) return null;
        
        try {
          const payload:any = jwtDecode(token);
          
          console.log(payload);

            return {
                userId: payload.exp?.toString()!,
                username: payload.unique_name + " " + payload.family_name,
                email: payload.email
            }
        } catch (error) {
            console.error("Invalid token", error);
            return null;
        }
      }
}