import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { Observable } from 'rxjs/internal/Observable';
import { ClimateService } from '../../core/services/climate.service';
import { ClimateStatisticModel } from '../../core/responses/climate-statistic.model';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'climates',
  templateUrl: './climates.component.html',
  styleUrls: ['./climates.component.css'],
  imports: [CommonModule, ReactiveFormsModule, TranslateModule],
})
export class ClimatesComponent {
  climate$!: Observable<ClimateStatisticModel>;
  constructor(private climateService: ClimateService) {}

  ngOnInit(): void {
    this.climate$ = this.climateService.getClimateStatistic();
  }
}
