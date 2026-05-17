package com.example.tradio.data.remote.api

import com.example.tradio.data.remote.dto.requests.CreatePaymentRequest
import com.example.tradio.data.remote.dto.responses.PaymentModelResponse
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST

interface PaymentApi {
    @POST("payments")
    suspend fun createPayment(@Body request: CreatePaymentRequest): Response<Unit>

    @GET("payments/by-user")
    suspend fun getPayments(): Response<List<PaymentModelResponse>>
}