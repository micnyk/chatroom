import { Injectable } from "@angular/core";
import { ApiService } from "../api/apiService";
import { AppState } from "../api/appState";
import { RoomDto, RoomsList } from "./responses";
import { Observable } from "rxjs/Observable";

@Injectable()
export class RoomService {

    constructor(private appState: AppState, private apiService: ApiService) { }

    getRoom(id: string): Observable<RoomDto> {
        return this.apiService
            .get(`rooms/${id}`)
            .map((result: any) => {
                return result as RoomDto;
            });
    }

    getAllRooms(): Observable<RoomsList> {
        return this.apiService
            .get("rooms")
            .map((result: any) => {
                let rooms = result as RoomsList;

                if (rooms == null)
                    rooms = new RoomsList();

                return rooms;
            });
    }
}