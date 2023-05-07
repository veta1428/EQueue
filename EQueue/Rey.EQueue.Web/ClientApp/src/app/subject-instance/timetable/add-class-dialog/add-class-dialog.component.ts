import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Class } from '../../../models/subject-instance';
import moment from 'moment';

@Component({
    selector: 'app-add-class-dialog',
    templateUrl: './add-class-dialog.component.html',
    styleUrls: ['./add-class-dialog.component.scss']
})
export class AddClassDialogComponent implements OnInit {

    public fg: FormGroup;
    public class: Class = null!;

    public get dayOfWeek() : AbstractControl
    {
        return this.fg.controls.dayOfWeek;
    }

    public get duration() : AbstractControl
    {
        return this.fg.controls.duration;
    }

    public get startTime() : AbstractControl
    {
        return this.fg.controls.startTime;
    }

    constructor(public dialogRef: MatDialogRef<AddClassDialogComponent>, private _fb: FormBuilder) 
    { 
        this.fg = this._fb.group({
            dayOfWeek: '',
            duration: [80, null],
            startTime: ''
        });
    }

    ngOnInit(): void {

    }

    onSubmit()
    {
        if(!this.fg.valid){
            return;
        }

        this.class = 
        { 
            dayOfWeek: this.dayOfWeek.value,
            duration: this.duration.value,
            startTime: moment(this.startTime.value, 'HH:mm').utc().format('yyyy-MM-DDTHH:mm:ss'),
        }

        this.dialogRef.close({class:this.class});
    }

    onCancel()
    {
        this.dialogRef.close();
    }
}
