package com.example.tradio.data.remote.dto.requests

data class CreateMessageRequest(
    val text: String,
    val serviceId: Int,
    val receiverId: String?
)