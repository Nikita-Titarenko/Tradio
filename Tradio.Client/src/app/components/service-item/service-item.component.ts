import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ServiceListItemModule } from '../../core/responses/service-list-item.model';
import { Router, RouterLink } from '@angular/router';
import { ServiceService } from '../../core/services/service.service';

@Component({
  selector: 'service-item',
  templateUrl: './service-item.component.html',
  styleUrl: './service-item.component.css',
  imports: [CommonModule, RouterLink],
})
export class ServiceItemComponent {
  @Input() service!: ServiceListItemModule;
  @Input() editable: boolean = false;
  @Output() serviceDeleted = new EventEmitter<number>();

  constructor(
    private router: Router,
    private serviceService: ServiceService,
  ) {}

  navigateToService() {
    this.router.navigate(['/service'], {
      queryParams: { serviceId: this.service.id },
    });
  }

  deleteService() {
    this.serviceService.deleteService(this.service.id).subscribe({
      next: () => {
        this.serviceDeleted.emit(this.service.id);
      },
    });
  }
}
