import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormControl, FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { CustomValidator } from './customvalidator';
import { RouterLink } from '@angular/router';
import { HotToastService } from '@ngxpert/hot-toast';
import { PasswordModule } from 'primeng/password';

@Component({
  selector: 'app-registration',
  imports: [ReactiveFormsModule, NgIf, FormsModule, RouterLink, PasswordModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {
  regExPassword = '^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\d\\s:])([^\\s]){8,16}$';


  registrationForm: FormGroup;
  constructor(
    public formBuilder: FormBuilder,
    private toastService: HotToastService
  ) {
    this.registrationForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(16), Validators.pattern(this.regExPassword)]],
      confirmPassword: ['', [Validators.required]],
      accetpTerms: [false, Validators.requiredTrue]
    });
    this.registrationForm.addValidators([CustomValidator.MatchValidator('password', 'confirmPassword')]);
  }

  onSubmit() {
    if (this.registrationForm.valid) {
      console.log('Form Submitted!', this.registrationForm.value);
      // Perform registration logic
      this.toastService.success('Utente registrato correttamente !');
    } else {
      this.toastService.error('Errori di Validazione !');
    }
  }
}
