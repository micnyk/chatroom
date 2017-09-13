import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { CookieModule } from 'ngx-cookie';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { SignInComponent } from "./components/signin/signin.component";
import { RegisterComponent } from "./components/register/register.component";
import { RoomsComponent } from "./components/rooms/rooms.component";
import { RoomComponent } from "./components/room/room.component";

import { AppState } from "./services/api/appState";

import { SignInGuard } from "./guards/signInGuard";
import { RoomGuard } from "./guards/roomGuard";
import { HomeGuard } from "./guards/homeGuard";

import { ApiService } from "./services/api/apiService";
import { UserService } from "./services/user/userService";
import { UrlService } from "./services/url/urlService";
import { RoomService } from "./services/room/roomService";
import { ChatService } from "./services/chat/chatService";


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        RoomsComponent,
        RoomComponent,
        HomeComponent,
        SignInComponent,
        RegisterComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent, canActivate: [HomeGuard] },
            { path: 'rooms', component: RoomsComponent, canActivate: [SignInGuard] },
            { path: 'room/:id', component: RoomComponent, canActivate: [SignInGuard, RoomGuard] },
            { path: '**', redirectTo: 'home' }
        ]),
        CookieModule.forRoot()
    ],
    providers: [SignInGuard, RoomGuard, HomeGuard, AppState, UrlService, ApiService, UserService, RoomService, ChatService]
})
export class AppModuleShared {
}
