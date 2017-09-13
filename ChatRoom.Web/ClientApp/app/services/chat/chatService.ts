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

    constructor(private appState: AppState) { }

    private hubConnection: HubConnection;
    private httpConnection: HttpConnection;
    private consoleLogger: ConsoleLogger;

    private messagesSubject: BehaviorSubject<ChatMessage> = new BehaviorSubject(<any>null);
    messageObservable = this.messagesSubject.asObservable();

    connectSignalR(): void {
        this.disconnectSignalR();

        this.consoleLogger = new ConsoleLogger(LogLevel.Information);
        this.httpConnection = new HttpConnection(`http://${document.location.host}/chat`, {
            transport: TransportType.WebSockets,
            logger: this.consoleLogger
        } as IHttpConnectionOptions);

        this.hubConnection = new HubConnection(this.httpConnection);

        this.hubConnection.onClosed = e => { console.log(e); };

        this.hubConnection.start().catch(err => console.log(err));

        this.hubConnection.on("RoomUsers", (users: Array<ChatUserDetails>) => {
            console.log(users);
        });

        this.hubConnection.on("RoomsOnlineUsers", (roomsDetails: Array<ChatRoomDetails>) => {
            this.appState.rooms.forEach(room => {
                const roomDetails = roomsDetails.filter(r => r.RoomId === room.id)[0];
                if (roomDetails != null)
                    room.usersOnline = roomDetails.UsersOnline;
            });
        });

        this.hubConnection.on("Message", (message: string, userName: string) => {
            this.messagesSubject.next(new ChatMessage(userName, message));
        });
    }

    disconnectSignalR() {
        if (this.hubConnection != null)
            this.hubConnection.stop();

        if (this.httpConnection != null)
            this.httpConnection.stop();
    }

    connectRoom(roomId: string): void {
        this.hubConnection.invoke("ConnectRoom", roomId);
    }

    sendMessage(roomId: string, message: string): void {
        this.hubConnection.invoke("Send", roomId, message);
    }
}