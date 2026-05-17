package com.example.tradio.data.remote.dto.responses

data class ChatListItemResponse(
    val id: Int,
    val fullName: String?,
    val lastMessageText: String?,
    val lastMessageDateTime: String?
)