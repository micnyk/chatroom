import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from "@angular/forms";
import { UserService } from "../../services/user/userService";
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
            "password": [null]
        });
    }

    public register(): void {
        const model = this.registerForm.value;

        const password = (model.password as string);
        const isGuest = password == null || password.length > 0;

        this.userService
            .register(isGuest, model.userName, isGuest ? null : model.password)
            .subscribe(response => { });
    }
}
