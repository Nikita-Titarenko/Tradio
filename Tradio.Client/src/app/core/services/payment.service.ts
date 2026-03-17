import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ChatModel } from '../responses/chat.model';
import { CreateMessageModel } from '../requests/create-message.model';
import { CreatePaymentModel } from '../requests/create-payment.model';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  private apiUrl = 'http://localhost:5188/api/Payments';

  constructor(private http: HttpClient) {}

  createPayment(
    applicationUserServiceModel: CreatePaymentModel,
  ): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}`, applicationUserServiceModel);
  }
}
