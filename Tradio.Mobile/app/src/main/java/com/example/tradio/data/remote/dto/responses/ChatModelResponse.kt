package com.example.tradio.data.remote.dto.responses

data class ChatModelResponse(
    val id: Int,
    val serviceId: Int,
    val serviceName: String?,
    val fullName: String?,
    val applicationUserId: String?,
    val applicationUserServiceId: Int?,
    val isRecipient: Boolean,
    val price: Double,
    val messages: List<MessageModelResponse>
)