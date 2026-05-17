package com.example.tradio.data.remote.dto.responses

data class MessageModelResponse(
    val text: String?,
    val isYourMessage: Boolean,
    val creationDateTime: String?,
)