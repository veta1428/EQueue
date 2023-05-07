import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { SubjectService } from '../subject.service';
import { AddSubjectModel, Subject } from '../../models/subject';

@Component({
    selector: 'app-add-subject-dialog',
    templateUrl: './add-subject-dialog.component.html',
    styleUrls: ['./add-subject-dialog.component.scss']
})
export class AddSubjectDialogComponent implements OnInit {

    public fg: FormGroup = null!;

    constructor(@Inject(MAT_DIALOG_DATA) public data: {update: boolean, id?: number, name?: string, description?: string},
        public dialogRef: MatDialogRef<AddSubjectDialogComponent>,
        private _subjectService: SubjectService,) {
    }

    public get name(): AbstractControl {
        return this.fg.controls.name;
    }

    public get description(): AbstractControl {
        return this.fg.controls.description;
    }

    public get isAdd() : boolean
    {
        return this.data.update == false;
    }

    ngOnInit(): void {
        this.fg = new FormGroup({
            name: new FormControl(''),
            description: new FormControl(''),
        });

        if (!this.isAdd)
        {
            this.fg.patchValue({
                name: this.data.name,
                description: this.data.description,
            });
        }
    }

    onSubmit() {
        if (!this.fg.valid) {
            return;
        }

        if(this.isAdd)
        {
            let subject: AddSubjectModel = {
                name: this.name.value,
                description: this.description.value,
            }

            this._subjectService.addSubject(subject).subscribe((_) => {
                this.dialogRef.close();
            });
        }
        else
        {

            let subject: Subject = {
                id: this.data.id!,
                name: this.name.value,
                description: this.description.value,
            }

            this._subjectService.updateSubject(subject).subscribe((_) => {
                this.dialogRef.close();
            });
        }
    }

    onCancel() {
        this.dialogRef.close();
    }

}
