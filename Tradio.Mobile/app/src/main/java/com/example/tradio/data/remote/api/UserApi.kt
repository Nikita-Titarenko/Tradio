package com.example.tradio.data.remote.api

import com.example.tradio.data.remote.dto.requests.ConfirmEmailRequest
import com.example.tradio.data.remote.dto.requests.LoginRequest
import com.example.tradio.data.remote.dto.requests.RegisterRequest
import com.example.tradio.data.remote.dto.responses.RegisterResponse
import com.example.tradio.data.remote.dto.responses.SignInResponse
import com.example.tradio.data.remote.dto.responses.UserModelResponse
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Path

interface UserApi {
    @POST("users/login")
    suspend fun login(@Body request: LoginRequest): Response<SignInResponse>

    @POST("users/register")
    suspend fun register(@Body request: RegisterRequest): Response<RegisterResponse>

    @POST("users/confirm-email")
    suspend fun confirmEmail(@Body request: ConfirmEmailRequest): Response<SignInResponse>

    @GET("users/{userId}")
    suspend fun getUser(@Path("userId") userId: String): Response<UserModelResponse>
}