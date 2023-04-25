import { Injectable } from '@angular/core';
import { AddSubjectInstanceModel } from '../models/subject-instance';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SubjectIntsanceService {

  constructor(private _httpClient: HttpClient) { }

  addSubjectInstance(subjectInstance: AddSubjectInstanceModel)
  {
    return this._httpClient.post('api/subjectinstance/add', subjectInstance);
  }
}
