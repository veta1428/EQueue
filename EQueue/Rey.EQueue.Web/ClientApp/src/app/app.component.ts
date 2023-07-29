
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { UserModel } from '../auth/models/user';
import { GroupContextService } from '../../src/core/group-context.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
    title = 'app';

    public user: UserModel | null = null;

    /**
     *
     */
    constructor(
        private _authService: AuthService, 
        private _cdr: ChangeDetectorRef, 
        private _groupContext: GroupContextService) { }

    ngOnInit() 
    {
        this._authService.getUser().subscribe(user =>{
            if(user != null)
            {
                this.user = user;
                this._cdr.markForCheck();
            }
        });
    }

    hasPracticeContext(){
        console.log(!Number.isNaN(this._groupContext.getGroupId()));
        return !Number.isNaN(this._groupContext.getGroupId());
    }
}
