import { ContactComponent } from './../../contact/contact.component';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { MovementService } from '../../../services/movement.service';
import { CurrencyPipe, DatePipe, NgFor } from '@angular/common';
import { MultiSelectModule } from 'primeng/multiselect';
import { FormsModule } from '@angular/forms';
import { FloatLabelModule } from 'primeng/floatlabel';
import { User } from '../../../models/user';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { Menubar } from 'primeng/menubar';
import { MenubarModule } from 'primeng/menubar';
import { DownloadService } from '../../../services/download.service';
import { HotToastService } from '@ngxpert/hot-toast';
import { DomSanitizer } from '@angular/platform-browser';

interface Month {
    name: string,
    code: string
}

@Component({
  selector: 'app-movement-list',
  imports: [NgFor, DatePipe, CurrencyPipe, MultiSelectModule, FormsModule, FloatLabelModule, Menubar, MenubarModule],
  templateUrl: './movement-list.component.html',
  styleUrl: './movement-list.component.css'
})
export class MovementListComponent implements OnInit {
  userData: User|any;
  canModify: boolean = false;
  canDelete: boolean = false;
  yearsList: number[] = [2024, 2025];
  selectedYears: number[] = [2025];
  monthsList: Month[] = [
    {name:'Gennaio', code:'1'},
    {name:'Febbraio', code:'2'},
    {name:'Marzo', code:'3'},
    {name:'Aprile', code:'4'},
    {name:'Maggio', code:'5'},
    {name:'Giugno', code:'6'},
    {name:'Luglio', code:'7'},
    {name:'Agosto', code:'8'},
    {name:'Settembre', code:'9'},
    {name:'Ottobre', code:'10'},
    {name:'Novembre', code:'11'},
    {name:'Dicembre', code:'12'},
  ];
  selectedMonths = ['5'];
  movements: any[] = [];

  download: MenuItem[] | undefined;

  constructor(
    private authService: AuthService,
    private movementService: MovementService,
    private router: Router,
    private downloadService: DownloadService,
    private toastService: HotToastService,
    private sanitizer: DomSanitizer
  ) {
  }

  ngOnInit(): void {
    if (!this.authService.isLogged())
      this.router.navigate(['/login']);
    this.download = [
      {
        label: 'New',
        icon: 'pi pi-plus-circle',
        command: () => { this.onCreate(); }
      },
      {
        label: 'Download',
        icon: 'pi pi-download',
        items: [
          {
            label: 'Excel',
            icon: 'pi pi-file-excel',
            command: () => {
              this.downloadService.getExcel(this.selectedYears, this.convertMonthList(this.selectedMonths)).subscribe({
                next: (response) => {
                  this.readBlob(response);
                  this.toastService.success('Download Excel successfully!');
                },
                error: (error) => {
                  this.toastService.error('Error downloading excel movements data!');
                  console.error('Error downloading excel movements data:', error); // Mostra l'errore nella console
                }
              });
            }
          },
          {
            label: 'Pdf',
            icon: 'pi pi-file-pdf',
            command: () => {
              this.downloadService.getPdf(this.selectedYears, this.convertMonthList(this.selectedMonths)).subscribe({
                next: (response) => {
                  this.readBlob(response);
                  this.toastService.success('Download Pdf successfully!')
                },
                error: (error) => {
                  this.toastService.error('Error downloading Pdf movements data!');
                  console.error('Error downloading pdf movements data:', error); // Mostra l'errore nella console
                }
              });
            }
          }
        ]
      }
    ];
    this.userData = this.authService.readUserData(); // Legge i dati dell'utente
    //console.log('User data.roles:', this.userData.roles); // Mostra i dati dell'utente nella console
    this.canModify = this.userData.roles.includes('User'); // Controlla se l'utente può modificare
    this.canDelete = this.userData.roles.includes('User'); // Controlla se l'utente può eliminare
    this.updateData(this.selectedYears, this.convertMonthList(this.selectedMonths));

  }

  readBlob(data: any){
    let fileName = data.headers.get('content-disposition')?.split(';')[1].split('=')[1];
    let blob = data.body as Blob;
    let a = document.createElement('a');
    a.download = fileName!;
    a.href = window.URL.createObjectURL(blob);
    a.click();
    a.href = '';
  }

  onPeriodsChanged(): void {
    console.log('Selected years:', this.selectedYears);
    console.log('Selected months:', this.selectedMonths);
     if (this.selectedYears && this.selectedMonths)
      this.updateData(this.selectedYears, this.convertMonthList(this.selectedMonths));
    else {
       alert('Please select at least one year and one month.');
       this.movements = []; // Resetta l'array dei movimenti
    }
  }

  updateData(years: number[], months: number[]): void {
    this.movementService.getMovements(years, months).subscribe({
      next: (data) => {
        //console.log('Movements data:', data); // Mostra i dati dei movimenti nella console
        const movementsDataString = JSON.stringify(data); // Converte i dati in una stringa JSON
        const movementsJson = JSON.parse(movementsDataString); // Converte la stringa JSON in un oggetto
        this.movements = movementsJson.value; // Assegna i dati dei movimenti all'array
      },
      error: (error) => {
        console.error('Error fetching movements data:', error); // Mostra l'errore nella console
      }
    });
  }

  onEdit(movement: any): void {}
  onDelete(movement: any): void {}
  onCreate(): void {
    this.router.navigate(['/createMovement']);
  }

  convertMonthList(monthsList: string[]): number[]{
    const result: number[] = [];
    for (const month of monthsList) {
        result.push(parseInt(month));
    }
    return result;
  }


  createFileType(e: any): string {
    let fileType: string = "";
    if (e == 'pdf' || e == 'csv') {
      fileType = `application/${e}`;
    }
    else if (e == 'jpeg' || e == 'jpg' || e == 'png') {
      fileType = `image/${e}`;
    }
    else if (e == 'txt') {
      fileType = 'text/plain';
    }

    else if (e == 'ppt' || e == 'pot' || e == 'pps' || e == 'ppa') {
      fileType = 'application/vnd.ms-powerpoint';
    }
    else if (e == 'pptx') {
      fileType = 'application/vnd.openxmlformats-officedocument.presentationml.presentation';
    }
    else if (e == 'doc' || e == 'dot') {
      fileType = 'application/msword';
    }
    else if (e == 'docx') {
      fileType = 'application/vnd.openxmlformats-officedocument.wordprocessingml.document';
    }
    else if (e == 'xls' || e == 'xlt' || e == 'xla') {
      fileType = 'application/vnd.ms-excel';
    }
    else if (e == 'xlsx') {
      fileType = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
    }

    return fileType;
  }
}
