export enum RequestResult {
    Ok = 1,
    Error,
    ModelNotValid
}

export class RequestResponse {
    responseResult: RequestResult;
    data: any;
    messages: Array<string>;
}