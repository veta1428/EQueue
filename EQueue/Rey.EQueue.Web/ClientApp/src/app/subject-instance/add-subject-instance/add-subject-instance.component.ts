import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Subject, SubjectList } from '../../models/subject';
import { SubjectService } from '../../subject/subject.service';
import { Teacher, TeacherList } from '../../models/teacher';
import { forkJoin } from 'rxjs';
import { TeacherService } from '../../teacher/teacher.service';
import {Location} from '@angular/common';
import { AddSubjectInstanceModel } from '../../models/subject-instance';
import { SubjectIntsanceService } from '../subject-intsance.service';


@Component({
    selector: 'app-add-subject-instance',
    templateUrl: './add-subject-instance.component.html',
    styleUrls: ['./add-subject-instance.component.scss']
})
export class AddSubjectInstanceComponent implements OnInit {

    public fg: FormGroup = null!;

    public subjects: Subject[] = [];
    public teachers: Teacher[] = [];
    public selectedSubjectId: number = 0;
    public selectedTeacherIds: number[] = [];
    public isLoading = true;


    public get name() : AbstractControl
    {
        return this.fg.controls.instanceName;
    }

    public get description() : AbstractControl
    {
        return this.fg.controls.description;
    }

    constructor(
        private _subjectService: SubjectService,
        private _teacherService: TeacherService,
        private _cdr: ChangeDetectorRef,
        private _fb: FormBuilder,
        private _location: Location,
        private _subjectInstanceService: SubjectIntsanceService
    ) {

    }

    ngOnInit(): void 
    {
        this.fg = this._fb.group({
            instanceName: '',
            description: '',
            subjectId: '',
            teacherIds: ''
        });

        forkJoin([this._subjectService.getSubjects(), this._teacherService.getTeachers()])
        .subscribe(([sl, tl]: [SubjectList, TeacherList]) => {
            this.subjects = sl.subjects;
            this.teachers = tl.teachers;
            this.isLoading = false;
            this._cdr.detectChanges();
        });
    }

    onSubmit()
    {
        console.log('in on submit');
        console.log(this.selectedTeacherIds);
        console.log(this.selectedSubjectId);
        if(!this.fg.valid)
        {
            return;
        }
        let si: AddSubjectInstanceModel = {
            name: this.name.value,
            description: this.description.value,
            teacherIds: this.selectedTeacherIds,
            subjectId: this.selectedSubjectId
        };

        this._subjectInstanceService.addSubjectInstance(si).subscribe((_) => {
            this._location.back();
        });
    }

    onCancel()
    {
        this._location.back();
    }
}
