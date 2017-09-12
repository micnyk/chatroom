import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import {UserService} from "../../services/user/userService";
import { Observable } from 'rxjs';

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
})
export class RegisterComponent {
    registerForm: FormGroup;

    constructor(fb: FormBuilder, private userService: UserService) {
        this.registerForm = fb.group({
            "userName": [null, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(50)])],
            "password": [null],
            "isGuest": [null]
        });
    }

    //public signIn(): void {
    //    const model = this.signInForm.value;
    //    let x = this.userService.signIn(model.userName, model.password);
    //    x.subscribe(response => console.log(response));
    //}
}
