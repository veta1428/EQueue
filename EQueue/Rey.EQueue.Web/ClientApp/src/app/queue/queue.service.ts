import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DetailedQueueModel, QueueModelList } from '../models/queue';

@Injectable({
    providedIn: 'root'
})
export class QueueService {

    constructor(private _httpClient: HttpClient) { }

    getQueues(): Observable<QueueModelList> {
        return this._httpClient.get<QueueModelList>("api/queue/queues");
    }

    getQueueDetailes(queueId: number): Observable<DetailedQueueModel> {
        return this._httpClient.get<DetailedQueueModel>(`api/queue/${queueId}`);
    }

    addUser(queueId: number): Observable<void> {
        return this._httpClient.get<void>(`api/queue/add-user/${queueId}`);
    }

    removeUser(queueId: number): Observable<void> {
        return this._httpClient.get<void>(`api/queue/remove-user/${queueId}`);
    }
}
