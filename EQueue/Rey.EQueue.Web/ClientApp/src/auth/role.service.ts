import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from './auth.service';
import { GroupContextService } from '../core/group-context.service';

@Injectable({
    providedIn: 'root'
})
export class RoleService {

    constructor(private _authService: AuthService, private _groupContext: GroupContextService) {

    }

    isUser(): boolean {
        return this.getRoles().includes('user');
    }

    isAdmin() {
        return this.getRoles().includes('admin');
    }

    private getRoles(): string[] {
        let groupId = this._groupContext.getGroupId();

        if (groupId === undefined) {
            return [];
        }

        let user = this._authService.user;

        if (user === null) {
            return [];
        }
        return user.roles.filter(r => r.groupId === groupId).map(r => r.role.toLowerCase());
    }

    hasOnlyOneGroup()
    {
        let user = this._authService.user;

        if (user === null) {
            return false;
        }
        
        return new Set(user.roles.map(r => r.groupId)).size === 1;
    }
}
