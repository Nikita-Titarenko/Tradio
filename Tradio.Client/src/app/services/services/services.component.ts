import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { Observable, BehaviorSubject, switchMap, of } from 'rxjs';
import { CategoryModel } from '../../core/responses/category.model';
import { CategoryService } from '../../core/services/category.service';
import { ServiceService } from '../../core/services/service.service';
import { ServiceListItemModule } from '../../core/responses/service-list-item.model';

@Component({
  selector: 'services',
  templateUrl: './services.component.html',
  host: {
    class: 'flex-column',
  },
  imports: [CommonModule, ReactiveFormsModule],
})
export class ServicesComponent implements OnInit {
  categories$!: Observable<CategoryModel[]>;
  subcategories$!: Observable<CategoryModel[]>;
  services$!: Observable<ServiceListItemModule[]>;
  hoverCategoryId?: number;
  private selectedSubcategoryId$ = new BehaviorSubject<number | undefined>(
    undefined,
  );
  pageNumber = 1;
  pageSize = 10;

  constructor(
    private categoryService: CategoryService,
    private serviceService: ServiceService,
  ) {}

  ngOnInit(): void {
    this.loadCategories();
    this.services$ = this.selectedSubcategoryId$.pipe(
      switchMap((subcategoryId) =>
        this.serviceService.getServices(this.pageNumber, this.pageSize, {
          categoryId: subcategoryId,
        }),
      ),
    );
  }

  loadCategories() {
    this.categories$ = this.categoryService.getCategories();
  }

  loadSubcategories(categoryId: number) {
    this.subcategories$ = this.categoryService.getCategories(categoryId);
    this.hoverCategoryId = categoryId;
  }

  removeCategories() {
    this.categories$ = of([]);
    this.subcategories$ = of([]);
  }

  chooseSubcategory(subcategoryId: number) {
    this.selectedSubcategoryId$.next(subcategoryId);
  }
}
