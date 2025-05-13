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
    //const userId = userData ? userData.id : null; // Ottieni l'ID dell'utente dal localStorage
    const movementByUserIdDto = {
      userId: userData.idUser,
      year: [2025],
      month: [5],
    }
    return this.httpClient.post<any[]>(`${this.apiUrl}/ByUserId`, movementByUserIdDto); // Modifica l'URL in base alla tua APIe

  }
}
