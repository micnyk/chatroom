import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from "rxjs/Observable";
import { AppState } from "../services/api/appState";
import { Injectable } from "@angular/core";

@Injectable()
export class HomeGuard implements CanActivate {

    constructor(private appState: AppState, private router: Router) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        if (this.appState.signedIn) {
            this.router.navigate(["rooms"]);
            return false;
        }

        return true;
    }
}