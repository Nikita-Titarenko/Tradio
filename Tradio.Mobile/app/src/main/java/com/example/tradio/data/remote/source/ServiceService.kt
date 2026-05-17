package com.example.tradio.data.remote.source

import com.example.tradio.data.remote.api.ServiceApi
import com.example.tradio.data.remote.dto.responses.FilterDataResponse
import com.example.tradio.data.remote.dto.responses.ServiceListItemResponse
import com.example.tradio.data.remote.dto.responses.ServiceModelResponse

class ServiceService(private val api: ServiceApi) {

    suspend fun getServices(
        pageNumber: Int,
        pageSize: Int,
        categoryId: Int?,
        cityId: Int?
    ): List<ServiceListItemResponse>? {
        val response = api.getServices(pageNumber, pageSize, categoryId, cityId)
        return if (response.isSuccessful) response.body() else null
    }

    suspend fun getCategories(parentCategoryId: Int? = null): List<FilterDataResponse>? {
        val response = api.getCategories(parentCategoryId)
        return if (response.isSuccessful) response.body() else null
    }

    suspend fun getCountries(): List<FilterDataResponse>? {
        val response = api.getCountries()
        return if (response.isSuccessful) response.body() else null
    }

    suspend fun getCities(countryId: Int): List<FilterDataResponse>? {
        val response = api.getCities(countryId)
        return if (response.isSuccessful) response.body() else null
    }

    suspend fun getService(serviceId: Int): ServiceModelResponse? {
        val response = api.getService(serviceId)
        return if (response.isSuccessful) response.body() else null
    }
}