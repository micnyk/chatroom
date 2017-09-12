import { Http } from "@angular/http";
import { Injectable } from "@angular/core";
import { UrlService } from "../url/urlService";
import { RequestResponse, RequestResult } from "./responses";
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

@Injectable()
export class ApiService {

    constructor(private urlService: UrlService, private http: Http) { }

    post(action: string, data: any): any {
        return this.http
            .post(this.urlService.apiUrl(action), data)
            .map(response => {
                const requestResponse = response.json() as RequestResponse;

                if (requestResponse == null)
                    throw new Error("Invalid server response");

                if (requestResponse.responseResult === RequestResult.Error) {
                    this.printErrors(requestResponse.messages);
                } else if (requestResponse.responseResult === RequestResult.ModelNotValid) {
                    this.printValidationErrors(requestResponse.data);
                }
                return requestResponse.data;
            });
    }

    printErrors(messages: string[]): void {
        console.log(messages);

        let output = "";
        messages.forEach(message => output += `${message}\n`);
        alert(output);
    }

    printValidationErrors(data: Array<any>): void {
        console.log(data);

        let output = "";
        data.forEach(obj => Object.keys(obj).forEach(key => output += `${key}: ${obj[key]}\n`));
        alert(output);
    }
}