import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7134/api/user'; // URL dell'API per gestire gli utenti
  constructor(private httpClient: HttpClient) { }

  getUserData(): Observable<any> {
    return this.httpClient.get<any>(`${this.apiUrl}/UserData` ); // Modifica l'URL in base alla tua API
  }
}
