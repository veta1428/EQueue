import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AddQueueModel, DetailedQueueModel, QueueModelList, QueueSearchMode } from '../models/queue';

@Injectable({
    providedIn: 'root'
})
export class QueueService {

    constructor(private _httpClient: HttpClient) { }

    getQueues(mode: QueueSearchMode): Observable<QueueModelList> {
        return this._httpClient.get<QueueModelList>(`api/queue/queues/${mode}`);
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

    addQueue(queue: AddQueueModel)
    {
        return this._httpClient.post('api/queue/add', queue);
    }

    deactivateQueue(queueId: number)
    {
        return this._httpClient.post(`api/queue/deactivate/${queueId}`, {});
    }

    activateQueue(queueId: number)
    {
        return this._httpClient.post(`api/queue/activate/${queueId}`, {});
    }
}
