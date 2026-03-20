import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ServiceModel } from '../../../core/responses/service.model';
import { Observable } from 'rxjs';
import { ServiceService } from '../../../core/services/service.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'service',
  templateUrl: './service.component.html',
  styleUrl: './service.component.css',
  imports: [CommonModule, RouterLink, TranslateModule],
  host: { class: 'flex-column text-center' },
})
export class ServiceComponent {
  service$!: Observable<ServiceModel>;
  constructor(
    public translate: TranslateService,
    private serviceService: ServiceService,
    private activatedRoute: ActivatedRoute,
  ) {}
  ngOnInit(): void {
    const lang =
      this.translate.currentLang || localStorage.getItem('lang') || 'en';
    this.translate.use(lang);
    const serviceId = this.activatedRoute.snapshot.queryParams['serviceId'];
    if (serviceId) {
      this.service$ = this.serviceService.getService(serviceId);
    }
  }
}
