import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddSubjectModel, SubjectList } from '../models/subject';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class SubjectService {

    constructor(private _httpClient: HttpClient) { }

    getSubjects() : Observable<SubjectList>
    {
        return this._httpClient.get<SubjectList>('/api/subject/subjects');
    }

    addSubject(subject: AddSubjectModel)
    {
        return this._httpClient.post('api/subject/add-subject', subject);
    }
}
