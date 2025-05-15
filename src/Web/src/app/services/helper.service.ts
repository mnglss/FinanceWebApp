import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HelperService {
  private apiUrl = 'https://localhost:7134/api/helper';
  constructor(private httpClient: HttpClient) { }

  getCategories(): Observable<string[]> {
    return this.httpClient.get<string[]>(`${this.apiUrl}/categories`);
  }
}
