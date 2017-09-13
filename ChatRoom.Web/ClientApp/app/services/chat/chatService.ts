import { HubConnection, ConsoleLogger, HttpConnection, TransportType, LogLevel } from "@aspnet/signalr-client";
import { IHttpConnectionOptions } from "@aspnet/signalr-client/dist/src/IHttpConnectionOptions";
import { Injectable } from "@angular/core";
import { ChatUserDetails } from "./chatUserDetails";

@Injectable()
export class ChatService {

    private hubConnection: HubConnection;
    private httpConnection: HttpConnection;
    private consoleLogger: ConsoleLogger;

    connectSignalR(): void {
        //this.disconnectSignalR();

        this.consoleLogger = new ConsoleLogger(LogLevel.Information);
        this.httpConnection = new HttpConnection(`http://${document.location.host}/chat`, {
            transport: TransportType.WebSockets,
            logger: this.consoleLogger
        } as IHttpConnectionOptions);

        this.hubConnection = new HubConnection(this.httpConnection);

        this.hubConnection.onClosed = e => { console.log(e); };
            
        this.hubConnection.start().catch(err => console.log(err));

        this.hubConnection.on("RoomUsersOnline", (users: Array<ChatUserDetails>) => {
            debugger;
        });
    }

    disconnectSignalR() {
        //if (this.hubConnection != null)
        //    this.hubConnection.stop();

        //if (this.httpConnection != null)
        //    this.httpConnection.stop();
    }

    connectRoom(roomId: string): void {
        //this.hubConnection.invoke("ConnectRoom", roomId);
    }
}