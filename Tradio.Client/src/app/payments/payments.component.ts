import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { PaymentService } from '../core/services/payment.service';
import { PaymentModel } from '../core/responses/payment.model';
import { Observable } from 'rxjs/internal/Observable';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'payments',
  templateUrl: './payments.component.html',
  styleUrls: ['./payments.component.css'],
  imports: [CommonModule, ReactiveFormsModule],
})
export class PaymentsComponent implements OnInit {
  payments$!: Observable<PaymentModel[]>;
  constructor(
    public translate: TranslateService,
    private paymentService: PaymentService,
  ) {}

  ngOnInit(): void {
    this.payments$ = this.paymentService.getPayments();
  }
}
