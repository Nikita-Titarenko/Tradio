package com.example.tradio.data.remote.dto.responses

data class MessageFromSignalRModel(
    val id: Int,
    val text: String?,
    val creationDateTime: String?,
    val senderId: String?,
    val applicationUserServiceId: Int
)