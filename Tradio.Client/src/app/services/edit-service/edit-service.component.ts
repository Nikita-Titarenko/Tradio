import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DropdownComponent } from '../../components/dropdown/dropdown.component';
import { ApiFormErrorService } from '../../core/services/api-form-error.service';
import { CategoryService } from '../../core/services/category.service';
import { ServiceService } from '../../core/services/service.service';

@Component({
  selector: 'edit-service',
  templateUrl: './edit-service.component.html',
  styleUrl: '../../../form.css',
  imports: [CommonModule, ReactiveFormsModule, DropdownComponent],
})
export class EditServiceComponent implements OnInit {
  serviceId!: number;
  formGroup: FormGroup;
  errorMessage: string = '';

  constructor(
    private serviceService: ServiceService,
    private router: Router,
    private categoryService: CategoryService,
    private apiFormErrorService: ApiFormErrorService,
    private activatedRoute: ActivatedRoute,
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

  ngOnInit(): void {
    this.serviceId = this.activatedRoute.snapshot.queryParams['serviceId'];
    this.serviceService.getService(this.serviceId).subscribe({
      next: (service) => {
        this.formGroup.patchValue(service);
      },
    });
  }

  get f() {
    return this.formGroup.controls;
  }

  editService() {
    if (!this.formGroup.valid) {
      return;
    }
    this.serviceService
      .updateService(this.serviceId, this.formGroup.value)
      .subscribe({
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
