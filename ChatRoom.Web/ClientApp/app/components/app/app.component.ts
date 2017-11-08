import { Component, OnInit } from '@angular/core';
import { AppState } from "../../services/api/appState";
import { UserService } from "../../services/user/userService";

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

    constructor(public appState: AppState, private userService: UserService) {
    }

    ngOnInit(): void {
        this.userService.init();
        this.appState.init();
    }
}
