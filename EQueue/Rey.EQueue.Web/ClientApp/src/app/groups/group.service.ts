import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Group } from './groups.model';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class GroupService {

    constructor(private _httpClient: HttpClient) { }

    public getGroups() : Observable<Group[]>
    {
        return this._httpClient.get<Group[]>("api/group");
    }
}
