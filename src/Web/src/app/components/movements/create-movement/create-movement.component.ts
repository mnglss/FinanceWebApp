import { DatePipe, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgForm, NgModel, ReactiveFormsModule, Validators } from '@angular/forms';
import { DatePickerModule } from 'primeng/datepicker';

@Component({
  selector: 'app-create-movement',
  imports: [FormsModule, ReactiveFormsModule, NgIf, DatePickerModule, DatePipe],
  templateUrl: './create-movement.component.html',
  styleUrl: './create-movement.component.css'
})
export class CreateMovementComponent {

  newMovementForm: FormGroup;
  onlydata: any;
  constructor(
    public formBuilder: FormBuilder)
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
      console.log('Year:', this.newMovementForm.get('date')?.value );
      console.log('Month:', this.newMovementForm.get('month')?.value);
      console.log('Category:', this.newMovementForm.get('category')?.value);
      console.log('Amount:', this.newMovementForm.get('amount')?.value);
      console.log('Date:', this.newMovementForm.get('date')?.value);
      console.log('Description:', this.newMovementForm.get('description')?.value);


      // Perform creation logic here
    } else {
      console.log('Form is invalid');
    }
  }
}
