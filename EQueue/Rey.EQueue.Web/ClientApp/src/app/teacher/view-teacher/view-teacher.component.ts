import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Teacher, TeacherList } from '../../models/teacher'
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { SubjectInstance, SubjectInstanceList } from '../../models/subject-instance';

@Component({
    selector: 'view-teacher',
    templateUrl: './view-teacher.component.html',
    styleUrls: ['view-teacher.component.scss']
})
export class ViewTeacherComponent implements OnInit, OnDestroy {

    public teacher: Teacher | null = null;
    public teacherId: number = 0;
    private subsciption: Subscription = new Subscription();
    public isLoading: boolean = true;
    public dataSource: SubjectInstance[] = [];
    constructor(private _http: HttpClient, private _cdr: ChangeDetectorRef, private _activateRoute: ActivatedRoute) 
    {
        this.subsciption.add(_activateRoute.params.subscribe(params => this.teacherId = params['id']));
    }

    ngOnInit()
    {
        this._http.get<Teacher>(`/api/teacher/teacher/${this.teacherId}`).subscribe((teacher: Teacher) => { 
            this._http.get<SubjectInstanceList>(`/api/subjectinstance/teacher/${this.teacherId}`).subscribe((subjectInstances: SubjectInstanceList) => { 
                this.dataSource = subjectInstances.subjectInstances;
                this.teacher = teacher;
                console.log(this.dataSource);
                this.isLoading = false;
                this._cdr.detectChanges();
                this._cdr.markForCheck();
            });
        });

    
    }

    ngOnDestroy()
    {
        this.subsciption.unsubscribe();
    }
}