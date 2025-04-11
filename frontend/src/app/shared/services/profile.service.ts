import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { map, Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class ProfileService {
    private httpClient = inject(HttpClient);
    private apiUrl = environment.apiUrl;

    getUserProfile(userId: number): Observable<string> {
        return this.httpClient.get(`${this.apiUrl}/profile/image?userId=${userId}`, {
            withCredentials: true,
            responseType: 'blob'
        }).pipe(
            map(response => {
                if (response.size == 0) {
                    return "";
                }
                return URL.createObjectURL(response)
            })
        );
    }

    setUserProfile(formData: FormData) : Observable<any>{
        return this.httpClient.post(`${this.apiUrl}/profile/upload`, formData, {
          withCredentials: true
        });
    }
}