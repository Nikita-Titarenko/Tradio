import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { CategoryModel } from "../../core/responses/category.model";
import { CategoryService } from "../../core/services/category.service";
import { ServiceService } from "../../core/services/service.service";
import { ServiceListItemModule } from "../../core/responses/service-list-item.model";

@Component({
    selector: 'services',
    templateUrl: './services.component.html',
    host: {
      class: 'flex-column'
    },
    imports: [
        CommonModule,
        ReactiveFormsModule
    ]
})

export class ServicesComponent implements OnInit{
    categories: CategoryModel[] = [];
    services: ServiceListItemModule[] = [];
    formGroup: FormGroup;
    pageNumber = 1;
    pageSize = 10;

    constructor(private categoryService: CategoryService, private serviceService: ServiceService, formBuilder: FormBuilder){
        this.formGroup = formBuilder.group({
            selectedCategory: ['']
        });
        this.formGroup.get('selectedCategory')!.valueChanges.subscribe(value => {
            this.serviceService.getServices(this.pageNumber, this.pageSize, {categoryId: value}).subscribe(data => {
                this.categories = data;
            })
        });
    }

    ngOnInit(): void {
        this.categoryService.getCategories().subscribe(data => {
            this.categories = data;
        });
        this.serviceService.getServices(this.pageNumber, this.pageSize).subscribe(data => {
            this.services = data;
        })
    }

}