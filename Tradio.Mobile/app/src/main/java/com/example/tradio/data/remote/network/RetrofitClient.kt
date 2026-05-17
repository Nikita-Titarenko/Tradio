package com.example.tradio.data.remote.network

import com.example.tradio.data.remote.api.ChatApi
import com.example.tradio.data.remote.api.PaymentApi
import com.example.tradio.data.remote.api.ServiceApi
import com.example.tradio.data.remote.api.UserApi
import okhttp3.OkHttpClient
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object RetrofitClient {
    private const val BASE_URL = "http://10.0.2.2:5188/api/"

    var tokenProvider: (() -> String?) = { null }

    private val okHttpClient: OkHttpClient by lazy {
        OkHttpClient.Builder()
            .addInterceptor(AuthInterceptor { tokenProvider() })
            .build()
    }

    private val retrofit: Retrofit by lazy {
        Retrofit.Builder()
            .baseUrl(BASE_URL)
            .client(okHttpClient)
            .addConverterFactory(GsonConverterFactory.create())
            .build()
    }

    val userApi: UserApi by lazy {
        retrofit.create(UserApi::class.java)
    }

    val serviceApi: ServiceApi by lazy {
        retrofit.create(ServiceApi::class.java)
    }

    val chatApi: ChatApi by lazy {
        retrofit.create(ChatApi::class.java)
    }

    val paymentApi: PaymentApi by lazy {
        retrofit.create(PaymentApi::class.java)
    }
}