import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Teacher, TeacherList } from '../../models/teacher'
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { SubjectInstance, SubjectInstanceList } from '../../models/subject-instance';
import { Subject } from 'src/app/models/subject';

@Component({
    selector: 'view-subject',
    templateUrl: './view-subject.component.html',
    styleUrls: ['view-subject.component.scss']
})
export class ViewSubjectComponent implements OnInit, OnDestroy {

    public subject: Subject | null = null;
    public subjectId: number = 0;
    private subsciption: Subscription = new Subscription();
    public isLoading: boolean = true;
    public dataSource: SubjectInstance[] = [];
    constructor(private _http: HttpClient, private _cdr: ChangeDetectorRef, private _activateRoute: ActivatedRoute) 
    {
        this.subsciption.add(_activateRoute.params.subscribe(params => this.subjectId = params['id']));
    }

    ngOnInit()
    {
        this._http.get<Subject>(`/api/subject/${this.subjectId}`).subscribe((subject: Subject) => { 
            this._http.get<SubjectInstanceList>(`/api/subjectinstance/subject/${this.subjectId}`).subscribe((subjectInstances: SubjectInstanceList) => { 
                this.dataSource = subjectInstances.subjectInstances;
                this.subject = subject;
                console.log(this.subject);
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