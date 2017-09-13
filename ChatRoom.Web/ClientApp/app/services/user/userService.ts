import { Injectable } from "@angular/core";
import { RegisterCommand, SignInCommand, SignOutCommand } from "./requests";
import { ApiService } from "../api/apiService";
import { RegisterResult, SignInResult, SignOutResult } from "./responses";
import { Observable } from "rxjs/Observable";
import { AppState } from "../api/appState";
import { CookieService } from "ngx-cookie";
import { ChatService } from "../chat/chatService";

@Injectable()
export class UserService {

    constructor(private apiService: ApiService,
        private appState: AppState,
        private cookieService: CookieService,
        private chatService: ChatService) {
    }

    init() {
        this.appState.setSignedInStateEvent.subscribe((userId: string) => {
            this.setSignedInState(userId);
        });
    }

    register(isGuest: boolean, userName: string, password: string): Observable<RegisterResult> {
        return this.apiService
            .post("user/register", new RegisterCommand(userName, password, isGuest))
            .map((result: any) => {
                const registerResult = result as RegisterResult;

                if (registerResult.success)
                    this.setSignedInState(registerResult.user.id);

                return result as RegisterResult;
            });
    }

    signIn(userName: string, password: string): Observable<SignInResult> {
        return this.apiService
            .post("user/signIn", new SignInCommand(userName, password))
            .map((result: any) => {
                const signInResult = result as SignInResult;

                if (signInResult.success)
                    this.setSignedInState(signInResult.user.id);

                return signInResult;
            });
    }

    signOut(): Observable<SignOutResult> {
        this.cookieService.remove(".AspNetCore.Identity.Application");
        this.cookieService.remove("signedIn");

        this.appState.signedIn = false;
        this.appState.currentUserId = <any>null;
        this.appState.rooms = [];
        this.chatService.disconnectSignalR();

        return this.apiService
            .post("user/signOut", new SignOutCommand())
            .map((result: any) => {
                return result as SignOutResult;
            });
    }

    private setSignedInCookie(userId: string) {
        const expires = new Date(new Date().getTime() + (1000 * 60 * 60 * 24 * 13));
        this.cookieService.putObject("signedIn", userId, { expires: expires });
    }

    setSignedInState(userId: string) {
        this.setSignedInCookie(userId);
        this.appState.signedIn = true;
        this.appState.currentUserId = userId;
        this.chatService.connectSignalR();
    }
}