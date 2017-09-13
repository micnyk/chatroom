import { Component, OnInit } from '@angular/core';
import { RoomsList, RoomDto } from "../../services/room/responses";
import { RoomService } from "../../services/room/roomService";
import { AppState } from "../../services/api/appState";
import { ChatService } from "../../services/chat/chatService";

@Component({
    selector: 'rooms',
    templateUrl: './rooms.component.html'
})
export class RoomsComponent implements OnInit {

    rooms: RoomsList;

    constructor(private appState: AppState, private roomService: RoomService, private chatService: ChatService) { }

    ngOnInit() {
        this.roomService
            .getAllRooms()
            .subscribe(rooms => this.rooms = rooms);
    }

    canConnect(room: RoomDto): boolean {
        return this.appState
            .connectedRooms
            .filter(r => r.id === room.id)
            .length === 0;
    }

    connect(room: RoomDto): void {
        this.appState.connectedRooms.push(room);
        this.chatService.connectRoom(room.id);
    }

    canDisconnect(room: RoomDto): boolean {
        return this.appState
            .connectedRooms
            .filter(r => r.id === room.id)
            .length > 0;
    }

    disconnect(room: RoomDto): void {
        this.appState.connectedRooms = this.appState.connectedRooms.filter(r => r !== room);
    }
}


