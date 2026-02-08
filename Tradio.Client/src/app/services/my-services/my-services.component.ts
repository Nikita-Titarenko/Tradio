import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ServiceItemComponent } from '../../components/service-item/service-item.component';
import { ServiceListItemModule } from '../../core/responses/service-list-item.model';
import { ServiceService } from '../../core/services/service.service';

@Component({
  selector: 'app-my-services',
  templateUrl: './my-services.component.html',
  imports: [CommonModule, ServiceItemComponent],
})
export class MyServicesComponent implements OnInit {
  services$!: Observable<ServiceListItemModule[]>;

  constructor(private serviceService: ServiceService) {}

  ngOnInit(): void {
    this.services$ = this.serviceService.getServicesByUser();
  }

  deleteService(id: number): void {
    this.services$ = this.services$.pipe(
      map((services) => services.filter((s) => s.id !== id)),
    );
  }
}
