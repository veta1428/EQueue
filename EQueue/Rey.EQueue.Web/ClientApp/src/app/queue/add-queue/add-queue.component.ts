import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { SubjectInstance, SubjectInstanceList } from '../../models/subject-instance';
import { SubjectIntsanceService } from '../../subject-instance/subject-intsance.service';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { AddQueueModel } from '../../models/queue';
import { QueueService } from '../queue.service';
import {Location} from '@angular/common';
import moment from 'moment';

@Component({
    selector: 'app-add-queue',
    templateUrl: './add-queue.component.html',
    styleUrls: ['./add-queue.component.scss']
})
export class AddQueueComponent implements OnInit {

    public subjectInstances: SubjectInstance[] = [];
    public isLoading: boolean = true;
    public fg: FormGroup = null!;
    public selectedSIId: number = 0;

    constructor(
        private _siService: SubjectIntsanceService, 
        private _cdr: ChangeDetectorRef, 
        private _fb: FormBuilder,
        private _queueService: QueueService,
        private _location: Location) 
        {

        }

    public get subjectInstanceId() : AbstractControl
    {
        return this.fg.controls.subjectInstanceId;
    }

    public get description() : AbstractControl
    {
        return this.fg.controls.description;
    }

    public get startTime() : AbstractControl
    {
        return this.fg.controls.startTime;
    }

    public get duration() : AbstractControl
    {
        return this.fg.controls.duration;
    }

    ngOnInit(): void {
        this._siService.getSubjectInstances().subscribe((si: SubjectInstanceList) => {
            this.subjectInstances = si.subjectInstances;
            this.isLoading = false;
            this._cdr.detectChanges();
        })

        this.fg = this._fb.group({
            subjectInstanceId: '',
            startTime: '',
            duration: '',
            description: ''
        });
    }

    onSubmit()
    {
        if(!this.fg.valid)
        {
            return;
        }
        console.log(this.startTime.value);
        console.log(moment(this.startTime.value).format('yyyy-MM-DDThh:mm:ss'));
        let queue: AddQueueModel = {
            subjectInstanceId: this.subjectInstanceId.value,
            startTime: moment(this.startTime.value).utc().format('yyyy-MM-DDThh:mm:ss'),
            duration: this.duration.value,
            description: this.description.value,
        }

        this._queueService.addQueue(queue).subscribe((_)=>{this._location.back();});
    }

    onCancel()
    {
        this._location.back();
    }
}
