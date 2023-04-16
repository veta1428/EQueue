import { ChangeDetectorRef, Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Teacher, TeacherList } from '../../models/teacher'
import { Subject, SubjectList } from '../../models/subject';
import { Router } from '@angular/router';
@Component({
    selector: 'view-subjects',
    templateUrl: './view-subjects.component.html',
    styleUrls: ['view-subjects.component.scss']
})

export class ViewSubjectsComponent {
    public displayedColumns: String[] = ['id', 'name', 'description'];
    public dataSource: Subject[] = [];
    public isLoading: boolean = true;
    
    constructor(private _http: HttpClient, private _cdr: ChangeDetectorRef, private _router: Router) {
        //let teacher: Teacher = { firstName: 'TestName', middleName: null, lastName: 'TestLastName', description: null, note: null };
        _http.get<SubjectList>('/api/subject/subjects').subscribe((subjects: SubjectList) => { 
            this.dataSource = subjects.subjects;
            this.isLoading = false;
            _cdr.detectChanges();
        });
    }

    getSubject(subject: Subject)
    {
        this._router.navigate([`/view-subject/${subject.id}`]);
    }
}