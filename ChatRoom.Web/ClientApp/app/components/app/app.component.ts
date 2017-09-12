import { Component, OnInit } from '@angular/core';
import { AppState } from "../../services/api/appState";

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

    constructor(private appState: AppState) {
    }

    ngOnInit(): void {
        this.appState.init();
    }
}
