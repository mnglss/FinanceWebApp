import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7134/api/auth'; // URL dell'API di autenticazione




  constructor(
    private httpClient: HttpClient,
    private userService: UserService
  ) { }

  login(email: string, password: string): Observable<any> {
    const credentials = {
      email: email,
      password: password
    };
    return this.httpClient.post<any>(`${this.apiUrl}/login`, { credentials });
  }
saveUserData(res: any): void {
    const userData = {
      name: res.name,
      email: res.email,
      password: res.password
    }
    //this.userService.userRole.next(res.role);

    localStorage.setItem('userData', JSON.stringify(userData));
  }

  // isLogged(): boolean {
  //   const userData = localStorage.getItem('userData');
  //   //const u = JSON.parse(userData); // per leggere i dati
  //   return userData !== null;
  // }

  // logout(): void {
  //   localStorage.removeItem('userData');
  //   this.userService.userRole.next('');
  // }

}
