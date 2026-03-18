import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { PaymentModel } from '../core/responses/payment.model';
import { Observable } from 'rxjs/internal/Observable';
import { PaymentService } from '../core/services/payment.service';
import { ClimateService } from '../core/services/climate.service';
import { ClimateStatisticModel } from '../core/responses/climate-statistic.model';

@Component({
  selector: 'climates',
  templateUrl: './climates.component.html',
  styleUrls: ['./climates.component.css'],
  imports: [CommonModule, ReactiveFormsModule],
})
export class ClimatesComponent {
  climate$!: Observable<ClimateStatisticModel>;
  constructor(private climateService: ClimateService) {}

  ngOnInit(): void {
    this.climate$ = this.climateService.getClimateStatistic();
  }
}
