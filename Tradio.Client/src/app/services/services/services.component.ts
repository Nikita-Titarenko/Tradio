import { CommonModule } from '@angular/common';
import { Component, forwardRef, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  NG_VALUE_ACCESSOR,
  ReactiveFormsModule,
} from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Observable, of, startWith, switchMap } from 'rxjs';
import { DropdownComponent } from '../../components/dropdown/dropdown.component';
import { ServiceItemComponent } from '../../components/service-item/service-item.component';
import { ServiceListItemModule } from '../../core/responses/service-list-item.model';
import { CategoryService } from '../../core/services/category.service';
import { CityService } from '../../core/services/city.service';
import { CountryService } from '../../core/services/country.service';
import { ServiceService } from '../../core/services/service.service';

@Component({
  selector: 'services',
  templateUrl: './services.component.html',
  styleUrl: './services.component.css',
  host: {
    class: 'flex-column',
  },
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ServiceItemComponent,
    RouterLink,
    DropdownComponent,
    ReactiveFormsModule,
  ],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DropdownComponent),
      multi: true,
    },
  ],
})
export class ServicesComponent implements OnInit {
  services$!: Observable<ServiceListItemModule[]>;
  formGroup: FormGroup;
  pageNumber = 1;
  pageSize = 10;

  constructor(
    private categoryService: CategoryService,
    private serviceService: ServiceService,
    private countryService: CountryService,
    private cityService: CityService,
    formBuilder: FormBuilder,
  ) {
    this.formGroup = formBuilder.group({
      selectedSubcategoryId: [],
      selectedCityId: [],
    });
  }

  ngOnInit(): void {
    this.formGroup.valueChanges
      .pipe(
        startWith(this.formGroup.value),
        switchMap((value) =>
          this.serviceService.getServices(this.pageNumber, this.pageSize, {
            ...(value.selectedSubcategoryId != null && {
              categoryId: value.selectedSubcategoryId,
            }),
            ...(value.selectedCityId != null && {
              cityId: value.selectedCityId,
            }),
          }),
        ),
      )
      .subscribe((services) => (this.services$ = of(services)));
  }

  loadCategories = () => {
    return this.categoryService.getCategories();
  };

  loadSubcategories = (categoryId: number) => {
    return this.categoryService.getCategories(categoryId);
  };

  loadCountries = () => {
    return this.countryService.getCountries();
  };

  loadCities = (countryId: number) => {
    return this.cityService.getCities(countryId);
  };
}
