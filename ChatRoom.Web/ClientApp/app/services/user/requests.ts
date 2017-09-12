export class RegisterCommand {
    constructor(
        public userName: string,
        public password: string,
        public isGuest: boolean)
    { }
}

export class SignInCommand {
    constructor(
        public userName: string,
        public password: string)
    { }
}

export class SignOutCommand {
}