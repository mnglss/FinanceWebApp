import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {

  constructor(private authService: AuthService) {


  }
  ngOnInit(): void {
    if (!this.authService.isLogged()) {
      // Se l'utente non è loggato, reindirizza alla pagina di login
      window.location.href = '/login';
    }
  }
}
