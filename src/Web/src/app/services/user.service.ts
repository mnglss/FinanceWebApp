import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UtilsService } from './utils.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  //private apiUrl = 'https://localhost:7134/api/user'; // URL dell'API per gestire gli utenti
  constructor(
    private httpClient: HttpClient,
    private utilsService: UtilsService
  ) { }

  getUserData(): Observable<any> {
    return this.httpClient.get<any>(`${this.utilsService.financeApiUrl}/user/UserData` ); // Modifica l'URL in base alla tua API
  }
}
