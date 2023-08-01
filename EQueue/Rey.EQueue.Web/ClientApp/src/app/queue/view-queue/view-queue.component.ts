import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { DetailedQueueModel, QueueModel, RecordModel } from '../../models/queue';
import { Subscription } from 'rxjs/internal/Subscription';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { QueueService } from '../queue.service';
import {Moment} from 'moment';
import moment from 'moment';
import { RoleService } from '../../../auth/role.service';

@Component({
    selector: 'app-view-queue',
    templateUrl: './view-queue.component.html',
    styleUrls: ['./view-queue.component.scss']
})
export class ViewQueueComponent implements OnInit, OnDestroy {

    public queue: DetailedQueueModel | null = null;
    public queueId: number = 0;
    private subsciption: Subscription = new Subscription();
    public isLoading: boolean = true;
    public dataSource: RecordModel[] = [];
    public displayedColumns: String[] = ['position','studentFirstName', 'studentLastName', 'created', 'actions'];
    
    public get hasData() : boolean
    {
        return this.dataSource.length > 0;
    }
    constructor(
        private _http: HttpClient, 
        private _cdr: ChangeDetectorRef, 
        private _activateRoute: ActivatedRoute,
        private _queueService: QueueService,
        private _roleService: RoleService) 
    {
        this.subsciption.add(_activateRoute.params.subscribe(params => this.queueId = params['id']));
    }

    public isAdmin(){
        return this._roleService.isAdmin();
    }

    public userInQueue() : boolean
    {
        if(this.dataSource.filter(r => r.isCurrentUser).length != 0)
        {
            return true;
        }

        return false;
    }

    public getDateFormatted(date: string)
    {
        return moment.utc(date).local().format('DD.MM.yyyy HH:mm:ss');
    }

    ngOnInit()
    {
        this._queueService.getQueueDetailes(this.queueId).subscribe((q: DetailedQueueModel)=>{
            this.dataSource = q.records;
            this.queue = q;
            this.isLoading = false;
            this._cdr.detectChanges();
            this._cdr.markForCheck();
        });
    }

    ngOnDestroy()
    {
        this.subsciption.unsubscribe();
    }

    addUser() : void
    {
        this._queueService.addUser(this.queueId).subscribe(_=>{
            this.ngOnInit();
        });
    }

    removeUser() : void
    {
        this._queueService.removeUser(this.queueId).subscribe(_=>{
            this.ngOnInit();
        });
    }

    onSendRequestClicked(element: RecordModel)
    {
        console.log('on send request clicked');
        this._queueService.sendChangeRequest(this.queueId, element.recordId).subscribe((_)=>{this.ngOnInit();});
    }
}
