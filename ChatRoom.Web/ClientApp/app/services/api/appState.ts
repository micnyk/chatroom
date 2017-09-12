import { Injectable } from "@angular/core";
import { CookieService } from "ngx-cookie";
import { UserDto } from "../../dtos/user";

@Injectable()
export class AppState {

    currentUser: UserDto;
    loaded: boolean;
    signedIn: boolean;

    constructor(private cookieService: CookieService) { }

    init(): void {
        const identityCookie = this.cookieService.getObject("signedIn") as boolean;

        if (identityCookie != null && identityCookie)
            this.signedIn = true;

        this.loaded = true;
    }
}