import { Component, OnInit } from '@angular/core';
import { RoomDto } from "../../services/room/responses";
import { RoomService } from "../../services/room/roomService";
import { ActivatedRoute } from "@angular/router";
import { AppState } from "../../services/api/appState";

@Component({
    selector: 'room',
    templateUrl: './room.component.html'
})
export class RoomComponent implements OnInit {

    room: RoomDto;

    constructor(private route: ActivatedRoute, private appState: AppState, private roomService: RoomService) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.room = this.appState.connectedRooms.filter(r => r.id === params["id"])[0];
        });
    }
}


