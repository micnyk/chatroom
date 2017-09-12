export class CreateUserCommand {
    constructor(
        public userName: string,
        public password: string,
        public isGuest: boolean)
    { }
}