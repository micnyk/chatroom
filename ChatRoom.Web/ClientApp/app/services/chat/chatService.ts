import { HubConnection, ConsoleLogger, HttpConnection, TransportType, LogLevel } from "@aspnet/signalr-client";
import { IHttpConnectionOptions } from "@aspnet/signalr-client/dist/src/IHttpConnectionOptions";
import { Injectable } from "@angular/core";
import { ChatUserDetails } from "./chatUserDetails";
import { ChatRoomDetails } from "./chatRoomDetails";
import { AppState } from "../api/appState";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { ChatMessage } from "./chatMessage";

@Injectable()
export class ChatService {

    constructor(public appState: AppState) { }

    private hubConnection: HubConnection;
    private httpConnection: HttpConnection;
    private consoleLogger: ConsoleLogger;

    private messagesSubject: BehaviorSubject<ChatMessage> = new BehaviorSubject(<any>null);
    messageObservable = this.messagesSubject.asObservable();

    private roomUsers: BehaviorSubject<ChatUserDetails> = new BehaviorSubject(<any>null);
    roomUsersObservable = this.roomUsers.asObservable();

    connectSignalR(): void {
        this.disconnectSignalR();

        this.consoleLogger = new ConsoleLogger(LogLevel.Information);
        this.httpConnection = new HttpConnection(`http://${document.location.host}/chat`, {
            transport: TransportType.WebSockets,
            logger: this.consoleLogger
        } as IHttpConnectionOptions);

        this.hubConnection = new HubConnection(this.httpConnection);

        this.hubConnection.onclose = e => { console.log(e); };

        this.hubConnection.start().catch(err => console.log(err));

        this.hubConnection.on("RoomUsers", (users: Array<ChatUserDetails>) => {
            users.forEach(u => this.roomUsers.next(u));
        });

        this.hubConnection.on("RoomsOnlineUsers", (roomsDetails: Array<ChatRoomDetails>) => {
            this.appState.rooms.forEach(room => {
                const roomDetails = roomsDetails.filter(r => r.roomId === room.id)[0];
                if (roomDetails != null)
                    room.usersOnline = roomDetails.usersOnline;
            });
        });

        this.hubConnection.on("Message", (roomId: string, message: string, userName: string) => {
            this.messagesSubject.next(new ChatMessage(roomId, userName, message));
        });
    }

    disconnectSignalR() {
        if (this.hubConnection != null)
            this.hubConnection.stop();

        if (this.httpConnection != null)
            this.httpConnection.stop();
    }

    connectToRoom(roomId: string): void {
        this.hubConnection.invoke("ConnectToRoom", roomId);
    }

    disconnectFromRoom(): void {
        this.hubConnection.invoke("DisconnectFromRoom");
    }

    sendMessage(roomId: string, message: string): void {
        this.hubConnection.invoke("SendMessage", roomId, message);
    }

    updateOnlineUsers(): void {
        this.hubConnection.invoke("GetRoomsOnlineUsers");
    }
}