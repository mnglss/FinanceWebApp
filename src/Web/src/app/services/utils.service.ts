import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UtilsService {
  //public fibanceApiUrl = 'https://localhost:7134/api';
  public financeApiUrl = 'http://www.amweb.somee.com/api';
  constructor() { }
}
