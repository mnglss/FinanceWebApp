import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { MovementService } from '../../../services/movement.service';

@Component({
  selector: 'app-movement-list',
  imports: [],
  templateUrl: './movement-list.component.html',
  styleUrl: './movement-list.component.css'
})
export class MovementListComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private movementService: MovementService
  ) {
  }

  ngOnInit(): void {
    if (!this.authService.isLogged()) {
      // Se l'utente non è loggato, reindirizza alla pagina di login
      window.location.href = '/login';
    }
    this.movementService.getMovements().subscribe({
      next: (data) => {
        console.log('Movements data:', data.value); // Mostra i dati dei movimenti nella console
      },
      error: (error) => {
        console.error('Error fetching movements data:', error); // Mostra l'errore nella console
      }
    });
  }
}
