import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreatePaymentModel } from '../requests/create-payment.model';
import { PaymentModel } from '../responses/payment.model';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  private apiUrl = 'http://localhost:7091/api/Payments';

  constructor(private http: HttpClient) {}

  createPayment(
    applicationUserServiceModel: CreatePaymentModel,
  ): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}`, applicationUserServiceModel);
  }

  getPayments(): Observable<PaymentModel[]> {
    return this.http.get<PaymentModel[]>(`${this.apiUrl}/by-user`);
  }
}
