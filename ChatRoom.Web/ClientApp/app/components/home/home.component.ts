import { Component } from '@angular/core';
import { AppState } from "../../services/api/appState";

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    constructor(public appState: AppState) { }
}
