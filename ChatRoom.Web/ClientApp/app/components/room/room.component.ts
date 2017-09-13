import { Component, OnInit, OnDestroy } from '@angular/core';
import { RoomDto } from "../../services/room/responses";
import { RoomService } from "../../services/room/roomService";
import { ActivatedRoute } from "@angular/router";
import { AppState } from "../../services/api/appState";
import { ChatService } from "../../services/chat/chatService";
import { Subscription } from "rxjs/Subscription";
import { ChatMessage } from "../../services/chat/chatMessage";

@Component({
    selector: 'room',
    templateUrl: './room.component.html',
    styleUrls: ['./room.component.css']
})
export class RoomComponent implements OnInit, OnDestroy {

    room: RoomDto;
    messages: Array<ChatMessage> = [];
    message: string;
    messageSubscription: Subscription;

    constructor(private route: ActivatedRoute, private appState: AppState, private roomService: RoomService,
        private chatService: ChatService) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.room = this.appState.connectedRooms.filter(r => r.id === params["id"])[0];
        });

        this.messageSubscription = this.chatService
            .messageObservable
            .subscribe(message => this.displayMessage(message));
    }

    ngOnDestroy() {
        this.messageSubscription.unsubscribe();
    }

    sendMessage() {
        this.chatService.sendMessage(this.room.id, this.message);
        this.message = <any>null;
    }

    private displayMessage(message: ChatMessage) {
        if (message != null)
            this.messages.push(message);
    }
}


