import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { SubjectInstanceDetails, TimetableModel } from '../../models/subject-instance';
import { SubjectIntsanceService } from '../subject-intsance.service';
import { ActivatedRoute } from '@angular/router';
import { RoleService } from '../../../auth/role.service';

@Component({
    selector: 'app-view-subject-instance',
    templateUrl: './view-subject-instance.component.html',
    styleUrls: ['./view-subject-instance.component.scss']
})
export class ViewSubjectInstanceComponent implements OnInit {

    public subjectInstance: SubjectInstanceDetails = null!;
    public subjectInstanceId: number = 0;
    public isLoading: boolean = true;
    public displayedColumns: String[] = ['id', 'firstName', 'middleName', 'lastName', 'description'];
    public showAddTimetableBlock: boolean = false;
    constructor(
        private _siService: SubjectIntsanceService, 
        private _activatedRoute: ActivatedRoute, 
        private _cdr: ChangeDetectorRef,
        private _roleService: RoleService) { }

    public getStringData(data: string | undefined | null): string {
        if (data == '' || data == null) {
            return 'N/A';
        }
        return data;
    }

    public isAdmin(){
        return this._roleService.isAdmin();
    }

    ngOnInit(): void {
        this._activatedRoute.params.subscribe((params) => {
            this.subjectInstanceId = params['id'];
            this._siService.getSubjectInstance(this.subjectInstanceId).subscribe((si => {
                this.subjectInstance = si;
                this.isLoading = false;
                this._cdr.detectChanges();
            }));
        })
    }

    onAddTimetable($event: TimetableModel)
    {
        this._siService.addTimetable(this.subjectInstanceId, $event).subscribe(()=>{this.ngOnInit()});
        this.showAddTimetableBlock = false;
    }

    onAddNewTimetableClicked()
    {
        this.showAddTimetableBlock = true;
        this._cdr.detectChanges();
    }

    onCancelAddNewTimetable()
    {
        this.showAddTimetableBlock = false;
        this._cdr.detectChanges();
    }
}   

