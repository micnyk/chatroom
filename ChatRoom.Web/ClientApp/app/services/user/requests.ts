export class RegisterCommand {
    constructor(
        public userName: string,
        public password: string,
        public isGuest: boolean)
    { }
}