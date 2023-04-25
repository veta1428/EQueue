import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Teacher, TeacherList } from '../../models/teacher'
import { Subject, SubjectList } from '../../models/subject';
import { Router } from '@angular/router';
import { SubjectService } from '../subject.service';
import { MatDialog } from '@angular/material/dialog';
import { AddSubjectDialogComponent } from '../add-subject-dialog/add-subject-dialog.component';
@Component({
    selector: 'view-subjects',
    templateUrl: './view-subjects.component.html',
    styleUrls: ['view-subjects.component.scss']
})

export class ViewSubjectsComponent implements OnInit
{
    public displayedColumns: String[] = ['id', 'name', 'description'];
    public dataSource: Subject[] = [];
    public isLoading: boolean = true;
    
    constructor(
        private _http: HttpClient, 
        private _cdr: ChangeDetectorRef, 
        private _router: Router,
        private _subjectService: SubjectService,
        private _dialog: MatDialog,) 
    {
    }

    ngOnInit()
    {
        this._subjectService.getSubjects().subscribe((subjects: SubjectList) => { 
            this.dataSource = subjects.subjects;
            this.isLoading = false;
            this._cdr.detectChanges();
        });
    }

    getSubject(subject: Subject)
    {
        this._router.navigate([`/view-subject/${subject.id}`]);
    }

    addSubjectClicked()
    {
        this.openDialog();
    }

    openDialog(): void {
        const dialogRef = this._dialog.open(AddSubjectDialogComponent, {
          data: {}
        });
    
        dialogRef.afterClosed().subscribe(result => {
            console.log('after closed');
            this.ngOnInit();
        });
    }
}