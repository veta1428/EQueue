import { ChangeDetectorRef, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { SubjectInstance, SubjectInstanceList } from '../../models/subject-instance';

@Component({
    selector: 'view-subject-instances',
    templateUrl: './view-subject-instances.component.html',
    styleUrls: ['view-subject-instances.component.scss']
})
export class ViewSubjectInstancesComponent {

    public displayedColumns: String[] = ['id', 'instanceName', 'timetable', 'instanceDescription', 'actions'];

    @Input() dataSource: SubjectInstance[] = [];

    public get hasData() : boolean
    {
        return this.dataSource.length > 0;
    }
    
    constructor(private _http: HttpClient, private _cdr: ChangeDetectorRef, private _router: Router) {
        // _http.get<SubjectInstanceList>('/api/subjectinstance/teacher/').subscribe((teachers: TeacherList) => { 
        //     this.dataSource = teachers.teachers;
        //     this.isLoading = false;
        //     _cdr.detectChanges();
        // });
    }

    ngOnInit()
    {
    }
}