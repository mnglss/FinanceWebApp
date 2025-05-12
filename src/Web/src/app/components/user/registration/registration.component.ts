import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormControl, FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { CustomValidator } from './customvalidator';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-registration',
  imports: [ReactiveFormsModule, NgIf, FormsModule, RouterLink],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {
  regExPassword = '^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\d\\s:])([^\\s]){8,16}$';


  registrationForm: FormGroup;
  constructor(public formBuilder: FormBuilder) {
    this.registrationForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.pattern(this.regExPassword)]],
      confirmPassword: ['', [Validators.required]],
      accetpTerms: [false, Validators.requiredTrue]
    });
    this.registrationForm.addValidators([CustomValidator.MatchValidator('password', 'confirmPassword')]);
  }

  onSubmit() {
    if (this.registrationForm.valid) {
      console.log('Form Submitted!', this.registrationForm.value);
      // Perform registration logic here
    } else {
      console.log('Form is invalid');
    }
  }
}
