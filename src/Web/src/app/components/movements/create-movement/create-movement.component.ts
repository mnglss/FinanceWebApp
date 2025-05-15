import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgForm, NgModel, ReactiveFormsModule, Validators } from '@angular/forms';
import { DatePickerModule } from 'primeng/datepicker';
import { Movement } from '../../../models/movement';
import { MovementService } from '../../../services/movement.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-movement',
  imports: [FormsModule, ReactiveFormsModule, NgIf, DatePickerModule],
  templateUrl: './create-movement.component.html',
  styleUrl: './create-movement.component.css'
})
export class CreateMovementComponent {

  newMovementForm: FormGroup;

  datePicker: Date = new Date();
  onlydata: Date = new Date();

  newMovement: Movement = {
    year: 0,
    month: 0,
    category: '',
    amount: 0,
    date: "",
    description: '',
    userId: 0,
    id: 0
  };

  constructor(
    public formBuilder: FormBuilder,
    private movementService: MovementService,
    private route: Router
  )
  {
    this.newMovementForm = this.formBuilder.group({
      year: ['', Validators.required],
      month: ['', Validators.required],
      category: ['', Validators.required],
      amount: ['', Validators.required],
      date: ['', Validators.required],
      description: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.newMovementForm.valid) {
      console.log('Form Submitted!', this.newMovementForm.value);
      console.log('Year:', this.newMovementForm.get('year')?.value );
      console.log('Month:', this.newMovementForm.get('month')?.value);
      console.log('Category:', this.newMovementForm.get('category')?.value);
      console.log('Amount:', this.newMovementForm.get('amount')?.value);
      console.log('Date:', this.newMovementForm.get('date')?.value);
      console.log('Description:', this.newMovementForm.get('description')?.value);

      this.datePicker = this.newMovementForm.get('date')?.value;
      this.onlydata = new Date(this.datePicker);
      // Perform creation logic here

      this.newMovement.year = this.onlydata.getFullYear();
      this.newMovement.month = this.onlydata.getMonth() + 1;
      this.newMovement.category = this.newMovementForm.get('category')?.value;
      this.newMovement.description = this.newMovementForm.get('description')?.value;
      this.newMovement.date = new Date(this.datePicker).toLocaleDateString('en-US', {});
      this.newMovement.amount = this.newMovementForm.get('amount')?.value;

      this.movementService.createMovement(this.newMovement).subscribe({
        next: (data) => {
          this.route.navigate(['/movement']);
        },
        error: (error) =>{
          alert('Errore Salvataggio');
        }
      });
    }
  }
}
