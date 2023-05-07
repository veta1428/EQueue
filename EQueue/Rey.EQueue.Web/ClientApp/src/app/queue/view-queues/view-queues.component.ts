import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { QueueModel, QueueModelList, QueueSearchMode } from '../../models/queue';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { QueueService } from '../queue.service';
import { elementAt } from 'rxjs';
import moment from 'moment';

@Component({
    selector: 'app-view-queues',
    templateUrl: './view-queues.component.html',
    styleUrls: ['./view-queues.component.scss']
})
export class ViewQueuesComponent implements OnInit {

    public displayedColumns: String[] = ['id', 'subjectInstanceName', 'startTime', 'peopleIn', 'currentUserPosition', 'actions'];
    public dataSource: QueueModel[] = [];
    public isLoading: boolean = true;
    public mode: QueueSearchMode = QueueSearchMode.Active;

    constructor(
        private _http: HttpClient, 
        private _cdr: ChangeDetectorRef, 
        private _router: Router, 
        private _queueService: QueueService,
        private _activateRoute: ActivatedRoute) {

    }

    public getDateFormatted(date: string)
    {
        return moment.utc(date).local().format('DD.MM.yyyy HH:mm');
    }
    
    public get showDeactivate() : boolean
    {
        return this.mode == QueueSearchMode.Active;
    }

    ngOnInit(): void {
        this._activateRoute.params.subscribe(params => {
            this.mode = params['mode'];
            this._queueService.getQueues(this.mode).subscribe((queues: QueueModelList) => {
                this.dataSource = queues.queues;
                this.isLoading = false;
                this._cdr.detectChanges();
            });
        });
    }

    public getQueue(row: QueueModel) {
        this._router.navigate([`/view-queue/${row.id}`]);
    }

    addQueue()
    {
        this._router.navigate(['add-queue']);
    }

    deactivateQueue(row: QueueModel, $event: Event)
    {
        $event.stopPropagation();
        this._queueService.deactivateQueue(row.id).subscribe(()=> this.ngOnInit());
    }

    activateQueue(row: QueueModel, $event: Event)
    {
        $event.stopPropagation();
        this._queueService.activateQueue(row.id).subscribe(()=> this.ngOnInit());
    }
}