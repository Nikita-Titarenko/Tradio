package com.example.tradio.data.remote.dto.requests

data class RegisterRequest(
    val name: String,
    val email: String,
    val password: String,
    val confirmPassword: String,
    val cityId: Int
)