import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LocalStorageService } from '../services/localstorage.service';

@Injectable({
  providedIn: 'root'
})
export class authKeyInterceptor implements HttpInterceptor {

  constructor(
    private storageService: LocalStorageService)
  {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const token = sessionStorage.getItem('auth-key'); // Recupera il token dal localStorage
    //console.log('Authenticaton Key: ', token); // Stampa il token nella console
    if (token)
      req = req.clone({
        url: req.url,
        setHeaders: {
          Authorization: `Bearer ${token}` // Aggiungi il token all'intestazione della richiesta
        }
      });

    return next.handle(req); // Passa la richiesta al prossimo gestore
  }
}
