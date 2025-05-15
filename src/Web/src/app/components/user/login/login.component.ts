import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { PasswordModule } from 'primeng/password';
import { InputTextModule } from 'primeng/inputtext';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { HotToastService } from '@ngxpert/hot-toast';

@Component({
  selector: 'app-login',
  imports: [FormsModule, PasswordModule, InputTextModule, NgIf, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  errorMessage: string = ''; // Messaggio di errore per il login
  //user: any;

  constructor(
    private router: Router,
    private authService: AuthService,
    private userService: UserService,
    private toastService: HotToastService
  ) { }


  onSubmit(cred: any) {
    if (cred.email !== '' || cred.password !== '') {
      this.authService.login(cred.email, cred.password).subscribe(
        {
          next: (res) => {
            sessionStorage.setItem('auth-key', res.value.result); // Salva il token nel localStorage
            this.toastService.success('Successfully toasted!');
            if (res) {

              this.userService.getUserData().subscribe({
                next: (data) => {
                  console.log('User data:', data.value); // Mostra i dati dell'utente nella console
                  this.authService.saveUserData(data.value); // Salva i dati dell'utente nel localStorage
                  this.router.navigate(['/dashboard']);
                },
                error: (error) => {
                  console.error('Error fetching user data:', error); // Mostra l'errore nella console
                  this.toastService.error(error);
                }
              });
            }
          },
          error: (err) => {
            console.log(err.error.message); // Mostra il messaggio di errore nella console
            this.toastService.error(err.message);
          }
        });
      }
  }
}
