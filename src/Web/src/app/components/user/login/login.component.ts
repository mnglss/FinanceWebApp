import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { PasswordModule } from 'primeng/password';
import { InputTextModule } from 'primeng/inputtext';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../../../services/user.service';

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
    private userService: UserService
  ) { }


  onSubmit(cred: any) {
    if (cred.email !== '' || cred.password !== '') {
      this.authService.login(cred.email, cred.password).subscribe(
        {
          next: (res) => {
            //this.user = res; // Salva i dati dell'utente
            this.errorMessage = res.value.result;
            sessionStorage.setItem('auth-key', res.value.result); // Salva il token nel localStorage

            if (res) {

              // this.messageService.add({
              //   severity: 'success',
              //   summary: 'Login',
              //   detail: 'Login effettuato con successo!',
              //   life: 3000 // Durata del messaggio in millisecondi
              //  }); // Mostra il messaggio di successo

              this.userService.getUserData().subscribe({
                next: (data) => {
                  console.log('User data:', data.value); // Mostra i dati dell'utente nella console
                  this.authService.saveUserData(data.value); // Salva i dati dell'utente nel localStorage
                  this.router.navigate(['/dashboard']);
                },
                error: (error) => {
                  console.error('Error fetching user data:', error); // Mostra l'errore nella console
                }
              });
            }
          },
          error: (err) => {
            this.errorMessage = err.message; // Salva il messaggio di errore);
            // this.messageService.add({
            //   severity: 'error',
            //   summary: 'Login',
            //   detail: 'Login errato!',
            //   life: 3000
            //  }); // Durata del messaggio in millisecondi // Mostra il messaggio di errore
            console.log(err.error.message); // Mostra il messaggio di errore nella console
          }
        });
      }
  }
}
