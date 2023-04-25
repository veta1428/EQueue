import { Injectable } from '@angular/core';
import { AddTeacherModel, TeacherList } from '../models/teacher';
import { HttpClient } from '@angular/common/http';
import { ObserversModule } from '@angular/cdk/observers';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class TeacherService {

    constructor(private _httpClient: HttpClient) { }

    addTeacher(teacher: AddTeacherModel) {
        return this._httpClient.post('api/teacher/add-teacher', teacher);
    }

    getTeachers(): Observable<TeacherList>
    {
        return this._httpClient.get<TeacherList>('/api/teacher/teachers');
    }
}
