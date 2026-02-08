import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ServiceListItemModule } from '../../core/responses/service-list-item.model';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'service-item',
  templateUrl: './service-item.component.html',
  styleUrl: './service-item.component.css',
  imports: [CommonModule, RouterLink],
})
export class ServiceItemComponent {
  @Input() service!: ServiceListItemModule;
  @Input() editable: boolean = false;

  constructor(private router: Router) {}

  navigateToService() {
    this.router.navigate(['/service'], {
      queryParams: { serviceId: this.service.id },
    });
  }

  deleteService() {}
}
