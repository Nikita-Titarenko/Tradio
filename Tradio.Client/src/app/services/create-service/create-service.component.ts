import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ServiceService } from '../../core/services/service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-service',
  templateUrl: './create-service.component.html',
  styleUrl: '../../../form.css',
  imports: [CommonModule, ReactiveFormsModule],
})
export class CreateServiceComponent {
  formGroup: FormGroup;
  errorMessage: string = '';

  constructor(
    private serviceService: ServiceService,
    private router: Router,
    formBuilder: FormBuilder,
  ) {
    this.formGroup = formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      categoryId: ['', Validators.required],
      price: ['', Validators.required],
      isVisible: [false],
    });
  }

  get f() {
    return this.formGroup.controls;
  }

  createService() {
    if (!this.formGroup.valid) {
      return;
    }
    this.serviceService.createService(this.formGroup.value).subscribe({
      next: () => {
        this.router.navigate(['/my-services']);
      },
      error: (err) => {
        this.errorMessage = err.error.message || 'Create service error';
      },
    });
  }
}
