import { Injectable } from "@angular/core";
import { RegisterCommand, SignInCommand, SignOutCommand } from "./requests";
import { ApiService } from "../api/apiService";
import { RegisterResult, SignInResult, SignOutResult } from "./responses";
import { Observable } from "rxjs/Observable";
import { AppState } from "../api/appState";
import { CookieService } from "ngx-cookie";
import { UserDto } from "../../dtos/user";

@Injectable()
export class UserService {

    constructor(private apiService: ApiService, private appState: AppState, private cookieService: CookieService) { }

    register(isGuest: boolean, userName: string, password: string): Observable<RegisterResult> {
        return this.apiService
            .post("user/register", new RegisterCommand(userName, password, isGuest))
            .map((result: any) => {
                const registerResult = result as RegisterResult;

                if (registerResult.success)
                    this.setSignedInState(registerResult.user);

                return result as RegisterResult;
            });
    }

    signIn(userName: string, password: string): Observable<SignInResult> {
        return this.apiService
            .post("user/signIn", new SignInCommand(userName, password))
            .map((result: any) => {
                const signInResult = result as SignInResult;

                if (signInResult.success)
                    this.setSignedInState(signInResult.user);

                return signInResult;
            });
    }

    signOut(): Observable<SignOutResult> {
        this.cookieService.remove(".AspNetCore.Identity.Application");
        this.cookieService.remove("signedIn");

        this.appState.signedIn = false;
        this.appState.currentUser = <any>null;

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

    private setSignedInState(user: UserDto) {
        this.setSignedInCookie(user.id);
        this.appState.signedIn = true;
        this.appState.currentUser = user;
    }
}