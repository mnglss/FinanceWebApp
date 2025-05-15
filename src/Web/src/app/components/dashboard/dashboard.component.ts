import { DashboardService } from './../../services/dashboard.service';
import { AuthService } from '../../services/auth.service';
import { CurrencyPipe, isPlatformBrowser, NgFor } from '@angular/common';
import { ChangeDetectorRef, Component, effect, inject, OnInit, PLATFORM_ID } from '@angular/core';
//import { AppConfigService } from '@/service/appconfigservice';
import { ChartModule } from 'primeng/chart';
import { MultiSelectModule } from 'primeng/multiselect';
import { FormsModule } from '@angular/forms';
import { FloatLabelModule } from 'primeng/floatlabel';

interface Month {
    name: string,
    code: string
}

interface DashboardData {
  totalIncome: number;
  totalOutcome: number;
  balance: number;
  balancePercentage: number;
  totalForCategory: [
    {
      category: string;
      amount: number;
      categoryPercentage: number;
    }
  ];
  pieDataSource: number[];
  pieDataLabels: string[];
}

@Component({
  selector: 'app-dashboard',
  imports: [ChartModule, MultiSelectModule, FormsModule, FloatLabelModule, CurrencyPipe, NgFor ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  yearsList: number[] = [2024, 2025]; // Crea un'istanza dell'array di anni
  selectedYears: number[] = [2025]; // Crea un'istanza dell'anno selezionato
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
  DashboardPieDataSource: DashboardData | any; // Crea un'istanza dell'array dei movimenti
  constructor(
    private authService: AuthService,
    private DashboardService: DashboardService,
    private cd: ChangeDetectorRef
  ) {
      this.DashboardPieDataSource = {
        totalIncome: 0,
        totalOutcome: 0,
        balance: 0,
        balancePercentage: 0,
        totalForCategory: [
          {
            category: '',
            amount: 0,
            categoryPercentage: 0
          }
        ],
        pieDataSource: [],
        pieDataLabels: []
      }

    }

  basicData: any;

  basicOptions: any;

  platformId = inject(PLATFORM_ID);



  ngOnInit(): void {
    if (!this.authService.isLogged()) {
      // Se l'utente non è loggato, reindirizza alla pagina di login
      window.location.href = '/login';
    }
    this.getData(this.selectedYears, this.convertMonthList(this.selectedMonths));
    //this.getData(this.selectedYears, this.selectedMonths);

  }

    onPeriodsChanged(): void {
    console.log('Selected years:', this.selectedYears);
    console.log('Selected months:', this.selectedMonths);
     if (this.selectedYears && this.selectedMonths) {
      this.getData(this.selectedYears, this.convertMonthList(this.selectedMonths));
      }
    else {
       alert('Please select at least one year and one month.');
       this.DashboardPieDataSource = undefined;
    }
  }

  getData(years: number[], months: number[]): void {
    this.DashboardService.getDashboardData(years, months).subscribe({
      next: (data) => {
        console.log('Dashboard Data:', data);
        const movementsDataString = JSON.stringify(data);
        const movementsJson = JSON.parse(movementsDataString);
        this.DashboardPieDataSource = movementsJson.value;
        this.initChart();
      },
      error: (error) => {
        console.error('Error fetching movements data:', error);
      }
    });
  }

  initChart() {
        if (isPlatformBrowser(this.platformId)) {
            const documentStyle = getComputedStyle(document.documentElement);
            const textColor = documentStyle.getPropertyValue('--p-text-color');
            const textColorSecondary = documentStyle.getPropertyValue('--p-text-muted-color');
            const surfaceBorder = documentStyle.getPropertyValue('--p-content-border-color');

            this.basicData = {
                labels: this.DashboardPieDataSource!.pieDataLabels,
                datasets: [
                    {
                        label: 'Sales',
                        data: this.DashboardPieDataSource!.pieDataSource,
                        backgroundColor: [
                            'rgba(249, 115, 22, 0.2)',
                            'rgba(6, 182, 212, 0.2)',
                            // 'rgb(107, 114, 128, 0.2)',
                            // 'rgba(139, 92, 246, 0.2)',
                        ],
                        //borderColor: ['rgb(249, 115, 22)', 'rgb(6, 182, 212)', 'rgb(107, 114, 128)', 'rgb(139, 92, 246)'],
                        borderWidth: 1,
                    },
                ],
            };

            this.basicOptions = {
                responsive: true,
                plugins: {
                    legend: {
                        labels: {
                            color: textColor,
                        },
                    },
                },
                // scales: {
                //     x: {
                //         ticks: {
                //             color: textColorSecondary,
                //         },
                //         grid: {
                //             color: surfaceBorder,
                //         },
                //     },
                //     y: {
                //         beginAtZero: true,
                //         ticks: {
                //             color: textColorSecondary,
                //         },
                //         grid: {
                //             color: surfaceBorder,
                //         },
                //     },
                // },
            };
            this.cd.markForCheck()
        }
    }

    convertMonthList(monthsList: string[]): number[]{
      const result: number[] = [];
      for (const month of monthsList) {
          result.push(parseInt(month));
      }
      return result;
    }
}
