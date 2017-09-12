import {UserDto} from "../../dtos/user";

export class RegisterResult {
    success: boolean;
    user: UserDto;
    errors: Array<string>;
}