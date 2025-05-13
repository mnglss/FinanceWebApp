import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class MovementService {
  private apiUrl = 'https://localhost:7134/api/movement'; // URL dell'API per gestire gli utenti

  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) { }

  getMovements(): Observable<any[]> {
    const userData = this.authService.readUserData();
    const userId = userData ? userData.id : null; // Ottieni l'ID dell'utente dal localStorage
    return this.httpClient.get<any>(`${this.apiUrl}/${userId}` ); // Modifica l'URL in base alla tua APIe

  }
}
