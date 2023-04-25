import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormControl, AbstractControl } from '@angular/forms';
import { AddTeacherModel } from '../../models/teacher';
import { TeacherService } from '../teacher.service';

@Component({
    selector: 'app-add-teacher-dialog',
    templateUrl: './add-teacher-dialog.component.html',
    styleUrls: ['./add-teacher-dialog.component.scss']
})
export class AddTeacherDialogComponent implements OnInit {

    public fg: FormGroup = null!;

    constructor(
        public dialogRef: MatDialogRef<AddTeacherDialogComponent>, 
        private _teacherService: TeacherService) 
    {

    }

    public get firstName() : AbstractControl
    {
        return this.fg.controls.firstName;
    }

    public get lastName() : AbstractControl
    {
        return this.fg.controls.lastName;
    }

    public get middleName() : AbstractControl
    {
        return this.fg.controls.middleName;
    }

    public get description() : AbstractControl
    {
        return this.fg.controls.description;
    }

    ngOnInit(): void {
        this.fg = new FormGroup({
            firstName: new FormControl(''),
            lastName: new FormControl(''),
            middleName: new FormControl(''),
            description: new FormControl(''),
        });
    }

    onSubmit()
    {
        if(!this.fg.valid){
            return;
        }

        let teacher: AddTeacherModel = {
            firstName: this.firstName.value,
            lastName: this.lastName.value,
            middleName: this.middleName.value,
            description: this.description.value,
            note: null
        }

        this._teacherService.addTeacher(teacher).subscribe((_) => {
            console.log('after adding before close');
            this.dialogRef.close();
        });
    }

    onCancel()
    {
        this.dialogRef.close();
    }
}
