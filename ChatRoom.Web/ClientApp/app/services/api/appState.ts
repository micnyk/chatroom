import { Injectable, EventEmitter } from "@angular/core";
import { CookieService } from "ngx-cookie";
import { RoomDto } from "../room/responses";
import { UserService } from "../user/userService";

@Injectable()
export class AppState {

    currentUserId: string;
    loaded: boolean;
    signedIn: boolean;
    connectedRooms: Array<RoomDto>;

    setSignedInStateEvent: EventEmitter<any> = new EventEmitter();

    constructor(private cookieService: CookieService) {
        this.connectedRooms = [];
    }

    init(): void {
        const identityCookie = this.cookieService.getObject("signedIn") as string;

        if (identityCookie != null && identityCookie)
            this.setSignedInStateEvent.emit(identityCookie);
            
        this.loaded = true;
    }
}