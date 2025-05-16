import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class DownloadService {
  private apiUrl = 'https://localhost:7134/api/download';
  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) { }

  getExcel(years: Number[], months: Number[]): Observable<any[]>{
    const userData = this.authService.readUserData();
    const movementByUserIdDto = {
      userId: userData.idUser,
      year: years,
      month: months,
    }
    return this.httpClient.post<any[]>(`${this.apiUrl}/excel`, movementByUserIdDto);
  }

    getPdf(years: Number[], months: Number[]): Observable<any[]>{
    const userData = this.authService.readUserData();
    const movementByUserIdDto = {
      userId: userData.idUser,
      year: years,
      month: months,
    }
    return this.httpClient.post<any[]>(`${this.apiUrl}/pdf`, movementByUserIdDto, {
      headers: {
        "Accept": "application/pdf"
      },
      responseType: undefined
    });
  }
}
