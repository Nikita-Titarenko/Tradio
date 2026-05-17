package com.example.tradio.data.remote.dto.responses

import com.google.gson.annotations.SerializedName

data class PaymentModelResponse(
    @SerializedName("id")
    val id: Int,
    @SerializedName("applicationUserServiceId")
    val applicationUserServiceId: Int,
    @SerializedName("price")
    val price: Double,
    @SerializedName("creationDateTime")
    val creationDateTime: String?,
    @SerializedName("serviceName")
    val serviceName: String?,
    @SerializedName("areYouProvider")
    val areYouProvider: Boolean
)