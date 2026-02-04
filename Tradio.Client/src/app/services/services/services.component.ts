import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import {
  Observable,
  BehaviorSubject,
  switchMap,
  of,
  combineLatest,
} from 'rxjs';
import { CategoryModel } from '../../core/responses/category.model';
import { CategoryService } from '../../core/services/category.service';
import { ServiceService } from '../../core/services/service.service';
import { ServiceListItemModule } from '../../core/responses/service-list-item.model';
import { CountryModel } from '../../core/responses/country.model';
import { CityModel } from '../../core/responses/city.model';
import { CityService } from '../../core/services/city.service';
import { CountryService } from '../../core/services/country.service';
import { ServiceItemComponent } from '../../components/service-item/service-item.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'services',
  templateUrl: './services.component.html',
  host: {
    class: 'flex-column',
  },
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ServiceItemComponent,
    RouterLink,
  ],
})
export class ServicesComponent implements OnInit {
  categories$!: Observable<CategoryModel[]>;
  subcategories$!: Observable<CategoryModel[]>;
  countries$!: Observable<CountryModel[]>;
  cities$!: Observable<CityModel[]>;
  services$!: Observable<ServiceListItemModule[]>;
  hoverCategoryId?: number;
  hoverCountryId?: number;
  selectedSubcategory$ = new BehaviorSubject<CategoryModel | undefined>(
    undefined,
  );
  selectedCountry$ = new BehaviorSubject<CountryModel | undefined>(undefined);
  selectedCity$ = new BehaviorSubject<CityModel | undefined>(undefined);
  pageNumber = 1;
  pageSize = 10;

  constructor(
    private categoryService: CategoryService,
    private serviceService: ServiceService,
    private countryService: CountryService,
    private cityService: CityService,
  ) {}

  ngOnInit(): void {
    this.services$ = combineLatest([
      this.selectedSubcategory$,
      this.selectedCountry$,
      this.selectedCity$,
    ]).pipe(
      switchMap(([subcategory, country, city]) =>
        this.serviceService.getServices(this.pageNumber, this.pageSize, {
          categoryId: subcategory?.id,
          countryId: country?.id,
          cityId: city?.id,
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

  removeCountries() {
    this.countries$ = of([]);
    this.cities$ = of([]);
  }

  chooseSubcategory(subcategory: CategoryModel) {
    this.selectedSubcategory$.next(subcategory);
    this.removeCategories();
  }

  loadCountries() {
    this.countries$ = this.countryService.getCountries();
  }

  loadCities(countryId: number) {
    this.cities$ = this.cityService.getCities(countryId);
    this.hoverCountryId = countryId;
  }

  chooseCountry(country: CountryModel) {
    this.selectedCity$.next(undefined);
    this.selectedCountry$.next(country);
    this.removeCountries();
  }

  chooseCity(city: CityModel) {
    this.selectedCountry$.next(undefined);
    this.selectedCity$.next(city);
    this.removeCountries();
  }
}
