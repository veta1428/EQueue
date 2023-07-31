import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, map, of } from 'rxjs';
import { AuthService } from './auth.service';
import { UserModel } from './models/user';

@Injectable({
    providedIn: 'root'
})
export class GroupListGuardService implements CanActivate {

    constructor(private _authService: AuthService, private _router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        return this._authService.getUser().pipe(
            map((user: UserModel | null) => {
                console.log(user);
                    if (user === null || user === undefined) {
                        window.location.href = `/Account/Login`;
                        return false;
                    }

                    let groups = new Set(user!.roles.map(r => r.groupId));
                    
                    if (groups.size === 1)
                        window.location.href = `/group/${groups.values().next().value}/queues/active`; 

                    return true;
            })
        );
    }
}
