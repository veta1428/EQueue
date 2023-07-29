import { ChangeDetectorRef, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { SubjectInstance, SubjectInstanceList } from '../../models/subject-instance';
import moment from 'moment';

@Component({
    selector: 'view-subject-instances',
    templateUrl: './view-subject-instances.component.html',
    styleUrls: ['view-subject-instances.component.scss']
})
export class ViewSubjectInstancesComponent {

    public displayedColumns: String[] = ['id', 'instanceName', 'timetable', 'instanceDescription'];

    @Input() dataSource: SubjectInstance[] = [];

    public get hasData() : boolean
    {
        return this.dataSource.length > 0;
    }

    public getStringData(data: string | undefined | null): string {
        if (data == '' || data == null) {
            return 'N/A';
        }
        return data;
    }

    public getDateFormatted(date: string)
    {
        return moment.utc(date).local().format('H:mm');
    }
    
    constructor(private _http: HttpClient, private _cdr: ChangeDetectorRef, private _router: Router, private _route: ActivatedRoute) {
        // _http.get<SubjectInstanceList>('/api/subjectinstance/teacher/').subscribe((teachers: TeacherList) => { 
        //     this.dataSource = teachers.teachers;
        //     this.isLoading = false;
        //     _cdr.detectChanges();
        // });
    }

    ngOnInit()
    {
    }

    getSubjectInstance(row: SubjectInstance)
    {
        this._router.navigate(['subject-instance', row.id], { relativeTo: this._route.parent });
    }
}