package com.example.tradio.data.remote.source

import android.util.Log
import com.example.tradio.data.remote.api.PaymentApi
import com.example.tradio.data.remote.dto.requests.CreatePaymentRequest
import com.example.tradio.data.remote.dto.responses.PaymentModelResponse
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext

class PaymentService(private val paymentApi: PaymentApi) {

    suspend fun createPayment(request: CreatePaymentRequest): Boolean? {
        return withContext(Dispatchers.IO) {
            try {
                val response = paymentApi.createPayment(request)
                if (response.isSuccessful) {
                    true
                } else {
                    Log.e("PaymentService", "Payment API error: ${response.code()}")
                    false
                }
            } catch (e: Exception) {
                Log.e("PaymentService", "Network exception: ${e.localizedMessage}")
                null
            }
        }
    }

    suspend fun getPayments(): List<PaymentModelResponse>? {
        return withContext(Dispatchers.IO) {
            try {
                val response = paymentApi.getPayments()
                if (response.isSuccessful) {
                    response.body()
                } else {
                    Log.e("PaymentService", "Get payments error: ${response.code()}")
                    null
                }
            } catch (e: Exception) {
                Log.e("PaymentService", "Network exception fetching payments: ${e.localizedMessage}")
                null
            }
        }
    }
}