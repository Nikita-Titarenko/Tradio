import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ServiceService } from '../../core/services/service.service';
import { ServiceListItemModule } from '../../core/responses/service-list-item.model';
import { Observable } from 'rxjs';
import { ServiceItemComponent } from '../../components/service-item/service-item.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-my-services',
  templateUrl: './my-services.component.html',
  imports: [CommonModule, ServiceItemComponent, RouterLink],
})
export class MyServicesComponent implements OnInit {
  services$!: Observable<ServiceListItemModule[]>;

  constructor(private serviceService: ServiceService) {}

  ngOnInit(): void {
    this.services$ = this.serviceService.getServicesByUser();
  }
}
