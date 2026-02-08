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
import { DropdownComponent } from '../../components/dropdown/dropdown.component';
import { CategoryService } from '../../core/services/category.service';
import { ApiFormErrorService } from '../../core/services/api-form-error.service';

@Component({
  selector: 'app-create-service',
  templateUrl: './create-service.component.html',
  styleUrl: '../../../form.css',
  imports: [CommonModule, ReactiveFormsModule, DropdownComponent],
})
export class CreateServiceComponent {
  formGroup: FormGroup;
  errorMessage: string = '';

  constructor(
    private serviceService: ServiceService,
    private router: Router,
    private categoryService: CategoryService,
    private apiFormErrorService: ApiFormErrorService,
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
        this.apiFormErrorService.apply(this.formGroup, err.error.errors);
      },
    });
  }

  loadCategories = () => {
    return this.categoryService.getCategories();
  };

  loadSubcategories = (categoryId: number) => {
    return this.categoryService.getCategories(categoryId);
  };
}
