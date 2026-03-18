import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { PaymentService } from '../core/services/payment.service';
import { PaymentModel } from '../core/responses/payment.model';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'payments',
  templateUrl: './payments.component.html',
  styleUrls: ['./payments.component.css'],
  imports: [CommonModule, ReactiveFormsModule],
})
export class PaymentsComponent implements OnInit {
  payments$!: Observable<PaymentModel[]>;
  constructor(private paymentService: PaymentService) {}

  ngOnInit(): void {
    this.payments$ = this.paymentService.getPayments();
  }
}
