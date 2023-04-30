import { Injectable } from '@angular/core';
import { AddSubjectInstanceModel, SubjectInstanceList } from '../models/subject-instance';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class SubjectIntsanceService {

	constructor(private _httpClient: HttpClient) { }

	addSubjectInstance(subjectInstance: AddSubjectInstanceModel) {
		return this._httpClient.post('api/subjectinstance/add', subjectInstance);
	}

	getSubjectInstances(): Observable<SubjectInstanceList> {
		return this._httpClient.get<SubjectInstanceList>('api/subjectinstance/all');
	}

	
}
