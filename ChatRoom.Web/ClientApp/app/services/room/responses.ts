export class RoomDto {
    id: string;
    name: string;
    usersOnline: number;
    unreadMessages: number;
    connected: boolean;
}

export class RoomsList extends Array<RoomDto> {
}