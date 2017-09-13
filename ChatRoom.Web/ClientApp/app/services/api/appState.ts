import { Injectable, EventEmitter } from "@angular/core";
import { CookieService } from "ngx-cookie";
import { RoomDto, RoomsList } from "../room/responses";

@Injectable()
export class AppState {
    currentUserId: string;
    loaded: boolean;
    signedIn: boolean;

    rooms: RoomsList;

    setSignedInStateEvent: EventEmitter<any> = new EventEmitter();

    constructor(private cookieService: CookieService) {
        this.rooms = [];
    }

    init(): void {
        const identityCookie = this.cookieService.getObject("signedIn") as string;

        if (identityCookie != null && identityCookie)
            this.setSignedInStateEvent.emit(identityCookie);
            
        this.loaded = true;
    }

    connectedRooms(): RoomsList {
        return this.rooms.filter(x => x.connected);
    }
}