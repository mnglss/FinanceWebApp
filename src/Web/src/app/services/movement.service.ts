import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { Movement } from '../models/movement';

@Injectable({
  providedIn: 'root'
})
export class MovementService {
  private apiUrl = 'https://localhost:7134/api/movement'; // URL dell'API per gestire gli utenti

  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) { }

  getMovements(years: number[], months: number[]): Observable<any[]> {
    const userData = this.authService.readUserData();
    const movementByUserIdDto = {
      userId: userData.idUser,
      year: years,
      month: months,
    }
    return this.httpClient.post<any[]>(`${this.apiUrl}/ByUserId`, movementByUserIdDto);
  }

  createMovement(newMovement: Movement): Observable<any> {

      const userData = this.authService.readUserData();
      newMovement.userId = userData.idUser;
      return this.httpClient.post(`${this.apiUrl}`, newMovement);
  }
}
