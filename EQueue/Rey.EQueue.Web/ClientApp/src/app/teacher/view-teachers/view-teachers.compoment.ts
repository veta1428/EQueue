import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Teacher, TeacherList } from '../../models/teacher'
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { AddTeacherDialogComponent } from '../add-teacher-dialog/add-teacher-dialog.component';
import { TeacherService } from '../teacher.service';
@Component({
    selector: 'view-teachers',
    templateUrl: './view-teachers.component.html',
    styleUrls: ['view-teachers.component.scss']
})

export class ViewTeachersComponent implements OnInit {
    public displayedColumns: String[] = ['id', 'firstName', 'middleName', 'lastName', 'description'];
    public dataSource: Teacher[] = [];
    public isLoading: boolean = true;

    constructor(
        private _http: HttpClient, 
        private _cdr: ChangeDetectorRef, 
        private _router: Router, 
        private _dialog: MatDialog,
        private _teacherService: TeacherService) {

    }

    ngOnInit()
    {
        this._teacherService.getTeachers().subscribe((teachers: TeacherList) => { 
            this.dataSource = teachers.teachers;
            this.isLoading = false;
            this._cdr.detectChanges();
        });
    }

    public getDescription(desc: string) : string
    {
        if (desc == '' || desc == null)
        {
            return 'N/A';
        }
        return desc;
    }

    public getTeacher(row: Teacher)
    {
        this._router.navigate([`/view-teacher/${row.id}`]);
    }

    openDialog(): void {
        const dialogRef = this._dialog.open(AddTeacherDialogComponent, {
          data: {update: false}
        });
    
        dialogRef.afterClosed().subscribe(result => {
            console.log('after closed');
            this.ngOnInit();
        });
    }

    onAddTeacherClicked()
    {
        this.openDialog();
    }

}