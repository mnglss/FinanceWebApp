import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7134/api/auth'; // URL dell'API di autenticazione




  constructor(
    private httpClient: HttpClient,
    private router: Router
  ) { }

  login(email: string, password: string): Observable<any> {
    const credentials = {
      email: email,
      password: password
    };
    return this.httpClient.post<any>(`${this.apiUrl}/login`, { credentials });
  }

  saveUserData(res: any): void {
    // const userData = {
    //   name: res.name,
    //   email: res.email,
    //   password: res.password
    // }
    //this.userService.userRole.next(res.role);

    sessionStorage.setItem('userData', JSON.stringify(res));
  }

  readUserData(): any {
    const userData = sessionStorage.getItem('userData');
    return userData ? JSON.parse(userData) : null;
  }

  isLogged(): boolean {
    const userData = sessionStorage.getItem('userData');
    return userData !== null;
  }

  logout(): void {
    sessionStorage.clear();
    this.router.navigate(['/home']);
  }

}
