export class RoomDto {
    id: string;
    name: string;
    usersOnline: number;
    unreadMessages: number;
}

export class RoomsList extends Array<RoomDto> {
}