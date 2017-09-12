import { Injectable } from "@angular/core";
import { RegisterCommand } from "./requests";
import { ApiService } from "../api/apiService";
import { RegisterResult } from "./responses";
import { Observable } from "rxjs/Observable";

@Injectable()
export class UserService {

    constructor(private apiService: ApiService) { }

    signIn(userName: string, password: string): Observable<RegisterResult> {
        return this.apiService
            .post("user/register", new RegisterCommand(userName, password, false))
            .map((result: any) => result as RegisterResult);
    }
}