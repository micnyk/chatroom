import { Injectable } from "@angular/core";
import { CookieService } from "ngx-cookie";
import { UserDto } from "../../dtos/user";
import {RoomDto} from "../room/responses";

@Injectable()
export class AppState {

    currentUser: UserDto;
    loaded: boolean;
    signedIn: boolean;
    connectedRooms: Array<RoomDto>;

    constructor(private cookieService: CookieService) {
        this.connectedRooms = [];
    }

    init(): void {
        const identityCookie = this.cookieService.getObject("signedIn") as boolean;

        if (identityCookie != null && identityCookie)
            this.signedIn = true;

        this.loaded = true;
    }
}