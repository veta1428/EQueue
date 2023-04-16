import { ChangeDetectorRef, Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Teacher, TeacherList } from '../../models/teacher'
import { Router } from '@angular/router';
@Component({
    selector: 'view-teachers',
    templateUrl: './view-teachers.component.html',
    styleUrls: ['view-teachers.component.scss']
})

export class ViewTeachersComponent {
    public displayedColumns: String[] = ['id', 'firstName', 'middleName', 'lastName', 'description'];
    public dataSource: Teacher[] = [];
    public isLoading: boolean = true;

    constructor(private _http: HttpClient, private _cdr: ChangeDetectorRef, private _router: Router) {
        _http.get<TeacherList>('/api/teacher/teachers').subscribe((teachers: TeacherList) => { 
            this.dataSource = teachers.teachers;
            this.isLoading = false;
            _cdr.detectChanges();
        });
    }

    public getTeacher(row: Teacher)
    {
        this._router.navigate([`/view-teacher/${row.id}`]);
    }
}