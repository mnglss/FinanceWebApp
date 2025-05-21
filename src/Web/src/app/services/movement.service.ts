import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { Movement } from '../models/movement';
import { UtilsService } from './utils.service';

@Injectable({
  providedIn: 'root'
})
export class MovementService {
  //private apiUrl = 'https://localhost:7134/api/movement'; // URL dell'API per gestire gli utenti

  constructor(
    private httpClient: HttpClient,
    private authService: AuthService,
    private utilsService: UtilsService
  ) { }

  getMovements(years: number[], months: number[]): Observable<any[]> {
    const userData = this.authService.readUserData();
    const movementByUserIdDto = {
      userId: userData.idUser,
      years: years.join(", "),
      months: months.join(", "),
    }
    return this.httpClient.post<any[]>(`${this.utilsService.financeApiUrl}/movement/ByUserId`, movementByUserIdDto);
  }

  createMovement(newMovement: Movement): Observable<any> {

      const userData = this.authService.readUserData();
      newMovement.userId = userData.idUser;
      return this.httpClient.post(`${this.utilsService.financeApiUrl}/movement`, newMovement);
  }
}
