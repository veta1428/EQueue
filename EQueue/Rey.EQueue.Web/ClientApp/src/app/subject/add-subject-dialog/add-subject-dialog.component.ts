import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { SubjectService } from '../subject.service';
import { AddSubjectModel } from '../../models/subject';

@Component({
    selector: 'app-add-subject-dialog',
    templateUrl: './add-subject-dialog.component.html',
    styleUrls: ['./add-subject-dialog.component.scss']
})
export class AddSubjectDialogComponent implements OnInit {

    public fg: FormGroup = null!;

    constructor(
        public dialogRef: MatDialogRef<AddSubjectDialogComponent>,
        private _subjectService: SubjectService) {

    }

    public get name(): AbstractControl {
        return this.fg.controls.name;
    }

    public get description(): AbstractControl {
        return this.fg.controls.description;
    }

    ngOnInit(): void {
        this.fg = new FormGroup({
            name: new FormControl(''),
            description: new FormControl(''),
        });
    }

    onSubmit() {
        if (!this.fg.valid) {
            return;
        }

        let subject: AddSubjectModel = {
            name: this.name.value,
            description: this.description.value,
        }

        this._subjectService.addSubject(subject).subscribe((_) => {
            console.log('after adding before close');
            this.dialogRef.close();
        });
    }

    onCancel() {
        this.dialogRef.close();
    }

}
