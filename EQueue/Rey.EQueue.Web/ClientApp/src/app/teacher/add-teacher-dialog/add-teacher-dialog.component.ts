import { Component, EventEmitter, OnInit, Inject } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormControl, AbstractControl } from '@angular/forms';
import { AddTeacherModel, Teacher } from '../../models/teacher';
import { TeacherService } from '../teacher.service';

@Component({
    selector: 'app-add-teacher-dialog',
    templateUrl: './add-teacher-dialog.component.html',
    styleUrls: ['./add-teacher-dialog.component.scss']
})
export class AddTeacherDialogComponent implements OnInit {

    public fg: FormGroup = null!;

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: {
            update: boolean, 
            id?: number, 
            firstName?: string, 
            middleName?: string, 
            lastName?: string,
            description?: string,
            note?: string,
        },
        public dialogRef: MatDialogRef<AddTeacherDialogComponent>, 
        private _teacherService: TeacherService) 
    {

    }
    
    public get isAdd() : boolean
    {
        return this.data.update == false;
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

        if (!this.isAdd)
        {
            this.fg.patchValue({
                firstName: this.data.firstName,
                lastName: this.data.lastName,
                middleName: this.data.middleName,
                description: this.data.description,
            });
        }
    }

    onSubmit()
    {
        if(!this.fg.valid){
            return;
        }

        if(this.isAdd)
        {
            let teacher: AddTeacherModel = {
                firstName: this.firstName.value,
                lastName: this.lastName.value,
                middleName: this.middleName.value,
                description: this.description.value,
                note: null
            }
    
            this._teacherService.addTeacher(teacher).subscribe((_) => {
                this.dialogRef.close();
            });
        }
        else
        {
            let teacher: Teacher = {
                id: this.data.id!,        
                firstName: this.firstName.value,
                lastName: this.lastName.value,
                middleName: this.middleName.value,
                description: this.description.value,
                note: ''
            }

            this._teacherService.updateTeacher(teacher).subscribe((_) => {
                this.dialogRef.close();
            });
        }
    }

    onCancel()
    {
        this.dialogRef.close();
    }
}
