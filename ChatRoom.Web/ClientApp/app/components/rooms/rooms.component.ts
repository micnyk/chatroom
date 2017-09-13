import { Component, OnInit } from '@angular/core';
import { RoomDto } from "../../services/room/responses";
import { RoomService } from "../../services/room/roomService";
import { AppState } from "../../services/api/appState";
import { ChatService } from "../../services/chat/chatService";
import { Router } from "@angular/router";

@Component({
    selector: 'rooms',
    templateUrl: './rooms.component.html'
})
export class RoomsComponent implements OnInit {

    constructor(private appState: AppState, private roomService: RoomService, private chatService: ChatService, private router: Router) { }

    ngOnInit() {
        this.roomService
            .getAllRooms()
            .subscribe(rooms => {
                this.appState.rooms = rooms;
                this.chatService.updateOnlineUsers();
            });
    }

    canConnect(room: RoomDto): boolean {
        return this.appState
            .connectedRooms()
            .filter(r => r.id === room.id)
            .length === 0;
    }

    connect(room: RoomDto): void {
        this.appState.rooms.filter(x => x.id === room.id)[0].connected = true;
        this.chatService.connectToRoom(room.id);
        this.router.navigate(["room", room.id]);
    }

    canDisconnect(room: RoomDto): boolean {
        return this.appState
            .connectedRooms()
            .filter(r => r.id === room.id)
            .length > 0;
    }

    disconnect(room: RoomDto): void {
        this.appState.rooms.filter(x => x.id === room.id)[0].connected = false;
        this.chatService.disconnectFromRoom();
    }
}


