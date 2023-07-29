import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Group } from './groups.model';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { GroupService } from './group.service';

@Component({
    selector: 'app-groups',
    templateUrl: './groups.component.html',
    styleUrls: ['./groups.component.scss']
})
export class GroupsComponent implements OnInit {
    public displayedColumns: String[] = ['id', 'name'];
    public dataSource: Group[] = [];
    public isLoading: boolean = true;

    constructor(
        private _http: HttpClient,
        private _cdr: ChangeDetectorRef,
        private _route: ActivatedRoute,
        private _router: Router,
        private _groupService: GroupService) {
    }

    ngOnInit() {
        this._groupService.getGroups().subscribe((groups: Group[]) => {
            this.dataSource = groups;
            this.isLoading = false;
            this._cdr.detectChanges();
        });
    }

    getGroup(group: Group) {
        this._router.navigate([`/group/${group.id}/queues/active`]);
    }
}
