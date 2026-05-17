package com.example.tradio.data.remote.dto.responses

data class ServiceModelResponse(
    val id: Int,
    val name: String?,
    val price: Double,
    val description: String,
    val creationDateTime: String?,
    val applicationUserName: String?
)