package com.example.tradio.data.remote.dto.requests

import com.google.gson.annotations.SerializedName

data class CreatePaymentRequest(
    @SerializedName("applicationUserServiceId")
    val applicationUserServiceId: Int
)