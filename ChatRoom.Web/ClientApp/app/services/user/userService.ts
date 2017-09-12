import { Injectable } from "@angular/core";
import { CreateUserCommand } from "./requests";
import {ApiService} from "../api/apiService";
import {CreateUserResult} from "./responses";
import { Observable } from "rxjs/Observable";

@Injectable()
export class UserService {

    constructor(private apiService: ApiService) { }

    signIn(userName: string, password: string): Observable<CreateUserResult> {
        return this.apiService
            .post("user", new CreateUserCommand(userName, password, false))
            .subscribe((result: CreateUserResult) => { console.log(result) });
    }
}