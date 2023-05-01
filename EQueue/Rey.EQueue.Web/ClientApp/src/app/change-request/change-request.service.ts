import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ChangeRequest, ChangeRequestList, SearchChangeRequestMode } from '../models/change-request';

@Injectable({
    providedIn: 'root'
})
export class ChangeRequestService {

    constructor(private _httpClient: HttpClient) { }

    getChangeRequestInfo(mode: SearchChangeRequestMode) : Observable<ChangeRequestList>
    {
        return this._httpClient.get<ChangeRequestList>(`api/queue/change-requests/${mode}`);
    }

    approve(chrId: number)
    {
        return this._httpClient.post(`api/queue/change-request/approve/${chrId}`, {});
    }

    decline(chrId: number)
    {
        return this._httpClient.post(`api/queue/change-request/decline/${chrId}`, {});
    }

    void(chrId: number)
    {
        return this._httpClient.post(`api/queue/change-request/void/${chrId}`, {});
    }
}
