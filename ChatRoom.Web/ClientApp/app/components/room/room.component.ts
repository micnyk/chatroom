import { Component, OnInit, OnDestroy } from '@angular/core';
import { RoomDto } from "../../services/room/responses";
import { RoomService } from "../../services/room/roomService";
import { ActivatedRoute } from "@angular/router";
import { AppState } from "../../services/api/appState";
import { ChatService } from "../../services/chat/chatService";
import { Subscription } from "rxjs/Subscription";
import { ChatMessage } from "../../services/chat/chatMessage";
import { ChatUserDetails } from "../../services/chat/chatUserDetails";
import { EmojiService } from "../../services/emoji/emojiService";

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
    roomUsersSubscription: Subscription;
    roomUsers: Array<ChatUserDetails> = [];

    constructor(private route: ActivatedRoute, public appState: AppState, private roomService: RoomService,
        private chatService: ChatService, private emojiService: EmojiService) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.room = this.appState.rooms.filter(r => r.id === params["id"])[0];
        });

        this.messageSubscription = this.chatService
            .messageObservable
            .subscribe(message => this.displayMessage(message));

        this.roomUsersSubscription = this.chatService
            .roomUsersObservable
            .subscribe(user => {
                if (user != null && this.roomUsers.filter(u => u.UserId === user.UserId).length === 0)
                    this.roomUsers.push(user);
            });
    }

    ngOnDestroy() {
        this.messageSubscription.unsubscribe();
        this.messages = [];
        this.message = <any>null;
        this.room = <any>null;
        this.roomUsersSubscription.unsubscribe();
        this.roomUsers = [];
        this.chatService.disconnectFromRoom();
    }

    sendMessage() {
        this.chatService.sendMessage(this.room.id, this.message);
        this.message = <any>null;
    }

    private displayMessage(message: ChatMessage) {
        if (message != null && message.roomId === this.room.id) {
            message = this.emojiService.parseMessage(message);
            this.messages.push(message);
        }
    }
}


