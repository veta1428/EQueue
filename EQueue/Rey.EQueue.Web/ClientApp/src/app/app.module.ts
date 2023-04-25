import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { ViewTeachersComponent } from './teacher/view-teachers/view-teachers.compoment';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTableModule } from '@angular/material/table'
import { MatInputModule } from '@angular/material/input'
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatCardModule } from '@angular/material/card'
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { ViewSubjectsComponent } from './subject/view-subjects/view-subjects.compoment';
import { ViewTeacherComponent } from './teacher/view-teacher/view-teacher.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ViewSubjectInstancesComponent } from './subject-instance/view-subject-instances/view-subject-instances.component';
import { ViewSubjectComponent } from './subject/view-subject/view-subject.component';
import { AuthGuardService } from 'src/auth/auth-guard.service';
import { ViewQueuesComponent } from './queue/view-queues/view-queues.component';
import { ViewQueueComponent } from './queue/view-queue/view-queue.component';
import { AddTeacherDialogComponent } from './teacher/add-teacher-dialog/add-teacher-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { AddSubjectInstanceComponent } from './subject-instance/add-subject-instance/add-subject-instance.component';
import { MatSelectModule } from '@angular/material/select';
import { AddSubjectDialogComponent } from './subject/add-subject-dialog/add-subject-dialog.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CounterComponent,
        FetchDataComponent,
        ViewTeachersComponent,
        ViewSubjectsComponent,
        ViewTeacherComponent,
        ViewSubjectInstancesComponent,
        ViewSubjectComponent,
        ViewQueuesComponent,
        ViewQueueComponent,
        AddTeacherDialogComponent,
        AddSubjectInstanceComponent,
        AddSubjectDialogComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        BrowserModule,
        BrowserAnimationsModule,
        MatTableModule,
        MatCardModule,
        MatFormFieldModule,
        MatInputModule,
        MatDividerModule,
        MatButtonModule,
        ReactiveFormsModule,
        MatProgressSpinnerModule,
        MatDialogModule,
        MatSelectModule,
        ApiAuthorizationModule,
        RouterModule.forRoot([
            { path: 'queues', component: ViewQueuesComponent, canActivate: [AuthGuardService]},
            { path: 'counter', component: CounterComponent, canActivate: [AuthGuardService] },
            { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthGuardService] },
            { path: 'view-teachers', component: ViewTeachersComponent, canActivate: [AuthGuardService] },
            { path: 'view-subjects', component: ViewSubjectsComponent, canActivate: [AuthGuardService] },
            { path: 'view-teacher/:id', component: ViewTeacherComponent, canActivate: [AuthGuardService]},
            { path: 'view-subject/:id', component: ViewSubjectComponent, canActivate: [AuthGuardService] },
            { path: 'view-queue/:id', component: ViewQueueComponent, canActivate: [AuthGuardService]},
            { path: 'add-subject-instance', component: AddSubjectInstanceComponent, canActivate: [AuthGuardService]},
            { path: '**', redirectTo: '/queues', pathMatch: 'full'}
        ])
    ],
    providers: [AuthGuardService],
    bootstrap: [AppComponent]
})
export class AppModule { }
