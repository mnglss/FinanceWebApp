import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { MovementService } from '../../../services/movement.service';
import { CurrencyPipe, DatePipe, NgFor } from '@angular/common';
import { MultiSelectModule } from 'primeng/multiselect';
import { FormsModule } from '@angular/forms';
import { FloatLabelModule } from 'primeng/floatlabel';

@Component({
  selector: 'app-movement-list',
  imports: [NgFor, DatePipe, CurrencyPipe, MultiSelectModule, FormsModule, FloatLabelModule],
  templateUrl: './movement-list.component.html',
  styleUrl: './movement-list.component.css'
})
export class MovementListComponent implements OnInit {
  yearsList: number[] = [2024, 2025]; // Crea un'istanza dell'array di anni
  selectedYears: number[] = [2025]; // Crea un'istanza dell'anno selezionato
  monthsList: number[] = [1,2,3,4,5,6,7,8,9,10,11,12]; // Crea un'istanza dell'array di mesi
  selectedMonths: number[] = [5]; // Crea un'istanza del mese selezionato
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
    this.updateData(this.selectedYears, this.selectedMonths);
  }

  onPeriodsChanged(): void {
    console.log('Selected years:', this.selectedYears);
    console.log('Selected months:', this.selectedMonths);
     if (this.selectedYears && this.selectedMonths)
      this.updateData(this.selectedYears, this.selectedMonths);
    else {
       alert('Please select at least one year and one month.');
       this.movements = []; // Resetta l'array dei movimenti
    }
  }

  updateData(years: number[], months: number[]): void {
    this.movementService.getMovements(years, months).subscribe({
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

}
