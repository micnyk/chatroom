import { Component } from '@angular/core';
import { AppState } from "../../services/api/appState";
import {UserService} from "../../services/user/userService";

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    constructor(public appState: AppState, private userSerivce: UserService) { }

    signOut(): void {
        this.userSerivce
            .signOut()
            .subscribe(() => {});
    }
}
