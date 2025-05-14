import { Component, DoCheck } from '@angular/core';
import { RouterLink, Route } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { NgIf } from '@angular/common';
import { User } from '../../models/user';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, NgIf],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements DoCheck {
  user: any; // Crea un'istanza dell'oggetto User
  isLogged: boolean; // Variabile per controllare se l'utente è loggato

  constructor(private authService: AuthService) {
    this.isLogged = this.authService.isLogged();
  }

  ngDoCheck(): void {
    this.isLogged = this.authService.isLogged();
    const usarData = this.authService.readUserData(); // Legge i dati dell'utente dal localStorage
    //console.log('User data:', usarData); // Mostra i dati dell'utente nella console
    if (usarData) {
      const userDataString = JSON.stringify(usarData);
     const userJson = JSON.parse(userDataString); // Converte la stringa JSON in un oggetto
      this.user = {
        name: userJson.firstName,
        email: userJson.email
      };
      //console.log('User:', this.user); // Mostra i dati dell'utente nella console
    }
  }

  logOut():void {
    this.authService.logout(); // Chiama il metodo di logout del servizio di autenticazione
    this.isLogged = false; // Imposta isLogged a false
  }

}
