import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { UserService } from "../../services/user/userService";
import { Observable } from 'rxjs';
import { SignInResult } from "../../services/user/responses";

@Component({
    selector: 'signin',
    templateUrl: './signin.component.html',
})
export class SignInComponent {
    signInForm: FormGroup;
    signInResult: SignInResult;

    constructor(fb: FormBuilder, private userService: UserService) {
        this.signInForm = fb.group({
            "userName": [null, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(50)])],
            "password": [null]
        });
    }

    public signIn(): void {
        const model = this.signInForm.value;

        this.userService
            .signIn(model.userName, model.password)
            .subscribe(result => { this.signInResult = result });
    }
}
