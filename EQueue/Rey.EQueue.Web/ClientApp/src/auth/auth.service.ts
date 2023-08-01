import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { UserModel } from './models/user';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(private _httpClient: HttpClient) { }
    private _user: UserModel | null = null;
    public get user(): UserModel | null {
        return this._user;
    }

    isAuthenticated(): Observable<boolean> {
        return this.getUser().pipe(
            map(u => {
                this._user = u;
                return u === null ? false : true;
            })
        );
    }

    getUser(): Observable<UserModel> {
        return this._httpClient.get<UserModel>("api/membership/current");
    }
}
