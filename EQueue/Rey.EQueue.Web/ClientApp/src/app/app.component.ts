
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { UserModel } from '../auth/models/user';

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
    constructor(private _authService: AuthService, private _cdr: ChangeDetectorRef) {

    }

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
}
