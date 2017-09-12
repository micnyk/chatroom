import { Inject, Injectable } from "@angular/core";

@Injectable()
export class UrlService {
    constructor(@Inject('BASE_URL') private baseUrl: string) { }
    
    apiUrl(action: string): string {
        return this.baseUrl + "api/" + action;
    }
}