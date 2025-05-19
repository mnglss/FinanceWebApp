import { HttpClient, HttpParams } from '@angular/common/http';
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

  getExcel(years: Number[], months: Number[]){
    const userData = this.authService.readUserData();
    const movementByUserIdDto = {
      userId: userData.idUser,
      year: years,
      month: months,
    }
     const options ={
      params:
        new HttpParams()
          .set('userId', userData.idUser)
          .set('year', years.join(", "))
          .set('month', months.join(", "))

    };
    // return this.httpClient.post<Blob>(`${this.apiUrl}/excel`, movementByUserIdDto);
    return this.httpClient.get(`${this.apiUrl}/excel`, {
      observe: 'response',
      responseType: 'blob',
      params: options.params
    });
  }

  getPdf(years: Number[], months: Number[]){
    const userData = this.authService.readUserData();
    const movementByUserIdDto = {
      userId: userData.idUser,
      year: years,
      month: months,
    };
    const options ={
      params:
        new HttpParams()
          .set('userId', userData.idUser)
          .set('year', years.join(", "))
          .set('month', years.join(", "))
    };
    //return this.httpClient.post(`${this.apiUrl}/pdf`, movementByUserIdDto);
    return this.httpClient.get(`${this.apiUrl}/pdf`, {
      observe: 'response',
      responseType: 'blob',
      params: options.params
    });
  }
}
