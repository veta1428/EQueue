import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpResponse, HttpRequest, HttpHandler, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { GroupContextService } from './group-context.service';

@Injectable()
export class HeaderInterceptor implements HttpInterceptor {
    /**
     *
     */
    constructor(private _activateRoute: ActivatedRoute, private _groupService: GroupContextService) {
    }
    intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const headers = new HttpHeaders({
            'Group-Id': `${this._groupService.getGroupId()}`
        });

        return next.handle(httpRequest.clone({ headers }));
    }
}