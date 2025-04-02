import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { TokenResponseModel } from "./models/token.model";
import { Router } from "@angular/router";
import { environment } from "../../../../environments/environment";


@Injectable({
    providedIn: 'root'
})
export class TokenService{
    private httpClient = inject(HttpClient);
    private route = inject(Router);
  
    private apiUrl = environment.apiUrl;
  
    private accessToken = signal<string | null>(null);

    ACCESS_TOKEN = this.accessToken.asReadonly();
    checkTokenValidity(): Promise<string | null> {
    return new Promise((resolve) => {
        
        this.httpClient.post<TokenResponseModel>(`${this.apiUrl}/auth/refresh`, {}, { withCredentials: true })
            .subscribe({
                next: (response) => {
                    this.saveToken(response.accessToken);
                    resolve(response.accessToken);
                },
                error: () => {
                    this.accessToken.update(() => null);
                    
                    resolve(null);
                }
            });
        });
    }

    public saveToken(accessToken: string): void {
        this.accessToken.update(() => accessToken);
    }

    public resetAccessToken() {
        this.accessToken.update(() => null);
    }
}