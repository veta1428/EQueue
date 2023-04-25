import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { QueueModel, QueueModelList } from '../../models/queue';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { QueueService } from '../queue.service';

@Component({
    selector: 'app-view-queues',
    templateUrl: './view-queues.component.html',
    styleUrls: ['./view-queues.component.scss']
})
export class ViewQueuesComponent implements OnInit {

    public displayedColumns: String[] = ['id', 'subjectInstanceName', 'startTime', 'peopleIn', 'currentUserPosition', 'actions'];
    public dataSource: QueueModel[] = [];
    public isLoading: boolean = true;

    constructor(
        private _http: HttpClient, 
        private _cdr: ChangeDetectorRef, 
        private _router: Router, 
        private _queueService: QueueService) {
        _queueService.getQueues().subscribe((queues: QueueModelList) => {
            console.log(queues);
            this.dataSource = queues.queues;
            this.isLoading = false;
            _cdr.detectChanges();
        });
    }

    ngOnInit(): void {}

    public getQueue(row: QueueModel) {
        this._router.navigate([`/view-queue/${row.id}`]);
    }
}