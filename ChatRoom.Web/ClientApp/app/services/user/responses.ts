import {UserDto} from "../../dtos/user";

export class CreateUserResult {
    success: boolean;
    user: UserDto;
    errors: Array<string>;
}