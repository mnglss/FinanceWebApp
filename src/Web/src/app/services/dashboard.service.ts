import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { UtilsService } from './utils.service';



@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  //private apiUrl = 'https://localhost:7134/api/dashboard'; // URL del tuo endpoint API
  constructor(
    private httpClient: HttpClient,
    private authService: AuthService,
    private utilsService: UtilsService
  ) { }

  getDashboardData(years: number[], months: number[]): Observable<any> {
    const userData = this.authService.readUserData();
    //const userId = userData ? userData.id : null; // Ottieni l'ID dell'utente dal localStorage
    const requestBody = {
      userId: userData.idUser,
      years: years.join(", "),
      months: months.join(", "),
    }
    return this.httpClient.post<any>(`${this.utilsService.financeApiUrl}/dashboard`, requestBody); // Modifica l'URL in base alla tua APIe

  }
}
