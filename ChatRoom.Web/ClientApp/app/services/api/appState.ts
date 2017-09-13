import { Injectable, EventEmitter } from "@angular/core";
import { CookieService } from "ngx-cookie";
import { RoomDto, RoomsList } from "../room/responses";

@Injectable()
export class AppState {
    rooms: RoomsList;
    currentUserId: string;
    loaded: boolean;
    signedIn: boolean;
    connectedRooms: Array<RoomDto>;

    setSignedInStateEvent: EventEmitter<any> = new EventEmitter();

    constructor(private cookieService: CookieService) {
        this.rooms = [];
        this.connectedRooms = [];
    }

    init(): void {
        const identityCookie = this.cookieService.getObject("signedIn") as string;

        if (identityCookie != null && identityCookie)
            this.setSignedInStateEvent.emit(identityCookie);
            
        this.loaded = true;
    }
}