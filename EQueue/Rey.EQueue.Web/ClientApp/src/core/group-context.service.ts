import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class GroupContextService {

    getGroupId(): number | undefined {
        return Number(window.location.pathname.split("/")[2]);
    }
    
}
