import { Component, OnInit } from '@angular/core';
import { ChangeRequestService } from '../change-request.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ChangeRequest, ChangeRequestList, SearchChangeRequestMode } from '../../models/change-request';

@Component({
    selector: 'app-change-request',
    templateUrl: './change-request.component.html',
    styleUrls: ['./change-request.component.scss']
})
export class ChangeRequestComponent implements OnInit {

    public mode: SearchChangeRequestMode = SearchChangeRequestMode.Incoming;
    public dataSource: ChangeRequest[] = [];
    public displayedColumns: String[] = ['id', 'subjectInstanceName', 'startTime', 'peopleIn', 'currentUserPosition', 'changeUserPosition', 'userFirstName', 'userLastName', 'status','actions'];

    constructor(
        private _shcService: ChangeRequestService,
        private _activateRoute: ActivatedRoute,
        private _router: Router) { }

    ngOnInit(): void {
        this._activateRoute.params.subscribe(params => {
            this.mode = params['mode'];
            this._shcService.getChangeRequestInfo(this.mode).subscribe((chr: ChangeRequestList) =>{
                this.dataSource = chr.changeRequests;
            });
        });
    }

    public get hasData()
    {
        return this.dataSource.length > 0;
    }

    public showApproveDeclineActions(row: ChangeRequest) : boolean
    {
        if(row.status == 'Pending' && this.mode == SearchChangeRequestMode.Incoming)
        {
            return true;
        }

        return false;
    }

    public showVoidAction(row: ChangeRequest)
    {
        if(row.status == 'Pending' && this.mode == SearchChangeRequestMode.Outcoming)
        {
            return true;
        }

        return false;
    }

    public approveChangeRequest(element: ChangeRequest,$event: Event)
    {
        $event.stopPropagation();
        this._shcService.approve(element.id).subscribe((_)=>{this.ngOnInit();});
    }

    public declineChangeRequest(element: ChangeRequest,$event: Event)
    {
        $event.stopPropagation();
        this._shcService.decline(element.id).subscribe((_)=>{this.ngOnInit();});
    }

    public voidChangeRequest(element: ChangeRequest,$event: Event)
    {
        $event.stopPropagation();
        this._shcService.void(element.id).subscribe((_)=>{this.ngOnInit();});
    }

    public visitQueue(element: ChangeRequest, $event: Event)
    {
        $event.stopPropagation();
        this._router.navigate([`view-queue/${element.queueId}`]);
    }
}
