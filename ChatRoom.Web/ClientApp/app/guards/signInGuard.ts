﻿import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from "rxjs/Observable";
import { AppState } from "../services/api/appState";
import { Injectable } from "@angular/core";

@Injectable()
export class SignInGuard implements CanActivate {

    constructor(private appState: AppState) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        return this.appState.signedIn;
    }
}