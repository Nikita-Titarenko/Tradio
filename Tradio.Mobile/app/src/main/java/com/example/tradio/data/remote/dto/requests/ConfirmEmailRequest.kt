package com.example.tradio.data.remote.dto.requests

data class ConfirmEmailRequest(
    val code: String,
    val userId: String
)