import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ServiceListItemModule } from '../../core/responses/service-list-item.model';

@Component({
  selector: 'service-item',
  templateUrl: './service-item.component.html',
  imports: [CommonModule],
})
export class ServiceItemComponent {
  @Input() service!: ServiceListItemModule;
}
