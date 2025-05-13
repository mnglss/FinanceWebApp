import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { MovementService } from '../../../services/movement.service';
import { CurrencyPipe, DatePipe, NgFor } from '@angular/common';
import { SelectModule } from 'primeng/select';

@Component({
  selector: 'app-movement-list',
  imports: [NgFor, DatePipe, CurrencyPipe, SelectModule],
  templateUrl: './movement-list.component.html',
  styleUrl: './movement-list.component.css'
})
export class MovementListComponent implements OnInit {
  years = [2025]; // Crea un'istanza dell'array di anni
  months: number[] = [1,2,3,4,5,6,7,8,9,10,11,12]; // Crea un'istanza dell'array di mesi
  movements: any[] = []; // Crea un'istanza dell'array di movimenti
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
        console.log('Movements data:', data); // Mostra i dati dei movimenti nella console
        const movementsDataString = JSON.stringify(data); // Converte i dati in una stringa JSON
        const movementsJson = JSON.parse(movementsDataString); // Converte la stringa JSON in un oggetto
        this.movements = movementsJson.value; // Assegna i dati dei movimenti all'array
      },
      error: (error) => {
        console.error('Error fetching movements data:', error); // Mostra l'errore nella console
      }
    });
  }

  onYearChanged(event: any): void {}
}
