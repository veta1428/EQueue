import { ChangeDetectorRef, Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { Class, TimetableModel } from '../../../models/subject-instance';
import { MatDialog } from '@angular/material/dialog';
import { AddClassDialogComponent } from '../add-class-dialog/add-class-dialog.component';
import moment from 'moment';

@Component({
    selector: 'app-add-timetable',
    templateUrl: './add-timetable.component.html',
    styleUrls: ['./add-timetable.component.scss']
})
export class AddTimetableComponent implements OnInit {

    public fg: FormGroup;

    public classes: Class[] = [];

    public get validityStart() : AbstractControl
    {
        return this.fg.controls.validityStart;
    }

    public get validityEnd(): AbstractControl
    {
        return this.fg.controls.validityEnd;
    }

    @Output() newTimetableEvent = new EventEmitter<TimetableModel>();
    @Output() onNewTimetableCancel = new EventEmitter<void>();

    constructor(private _fb: FormBuilder, private _dialog: MatDialog, private _cdr: ChangeDetectorRef) { 
        this.fg = this._fb.group({
            validity: '',
            validityStart: '',
            validityEnd: ''
        });
    }

    ngOnInit(): void {
    }

    onCancel() {
        this.onNewTimetableCancel.emit();
    }

    onAddClass()
    {
        this.openDialog();
    }

    openDialog(): void {
        const dialogRef = this._dialog.open(AddClassDialogComponent, {});
    
        dialogRef.afterClosed().subscribe(result => {

            if(result?.class != null)
            {
                this.classes = [...this.classes, result?.class];
                this._cdr.detectChanges();
            }
        });
    }

    onSubmit() {
        if (!this.fg.valid || this.classes.length < 1)
        {
            return;
        }

        let timetable: TimetableModel =
        {
            appliedPeriodStart: moment(this.validityStart.value).format('yyyy-MM-DDThh:mm:ss'),
            appliedPeriodEnd: moment(this.validityEnd.value).format('yyyy-MM-DDThh:mm:ss'),
            classes: this.classes
        }

        this.newTimetableEvent.emit(timetable);
    }
}
