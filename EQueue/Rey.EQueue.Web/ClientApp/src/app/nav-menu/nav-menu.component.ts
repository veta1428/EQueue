import { Component, Input } from '@angular/core';
import { UserModel } from '../../auth/models/user';
import { GroupContextService } from 'src/core/group-context.service';
import { RoleService } from '../../auth/role.service';

@Component({
	selector: 'app-nav-menu',
	templateUrl: './nav-menu.component.html',
	styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
	/**
	 *
	 */
	constructor(private _groupContext: GroupContextService, private _roleService: RoleService) {

	}

	isExpanded = false;

	get groupId(): number | undefined {
		return this._groupContext.getGroupId();
	}

	get showGroupsTab(): boolean
	{
		return !this._roleService.hasOnlyOneGroup();
	}

	@Input() user: UserModel | null = null;

	@Input() showGroupMenu: boolean = false;

	collapse() {
		this.isExpanded = false;
	}

	toggle() {
		this.isExpanded = !this.isExpanded;
	}
}
