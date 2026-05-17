package com.example.tradio.data.remote.dto.responses

data class SignInResponse(
    val userId: String,
    val jwtToken: String,
    val emailConfirmed: Boolean
)