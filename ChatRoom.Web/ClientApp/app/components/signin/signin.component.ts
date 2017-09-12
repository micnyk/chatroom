import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import {UserService} from "../../services/user/userService";
import { Observable } from 'rxjs';

@Component({
    selector: 'signin',
    templateUrl: './signin.component.html',
    //styleUrls: ['./signin.component.css']
})
export class SignInComponent {
    signInForm: FormGroup;

    constructor(fb: FormBuilder, private userService: UserService) {
        this.signInForm = fb.group({
            "userName": [null, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(50)])],
            "password": [null]
        });
    }

    public signIn(): void {
        const model = this.signInForm.value;
        this.userService.signIn(model.userName, model.password).subscribe(response => console.log(response));

    }
}
