package com.example.tradio.data.remote.api

import com.example.tradio.data.remote.dto.responses.FilterDataResponse
import com.example.tradio.data.remote.dto.responses.ServiceListItemResponse
import com.example.tradio.data.remote.dto.responses.ServiceModelResponse
import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.Path
import retrofit2.http.Query

interface ServiceApi {
    @GET("services")
    suspend fun getServices(
        @Query("pageNumber") pageNumber: Int,
        @Query("pageSize") pageSize: Int,
        @Query("categoryId") categoryId: Int?,
        @Query("cityId") cityId: Int?
    ): Response<List<ServiceListItemResponse>>

    @GET("categories")
    suspend fun getCategories(
        @Query("parentCategoryId") parentCategoryId: Int? = null
    ): Response<List<FilterDataResponse>>

    @GET("countries")
    suspend fun getCountries(): Response<List<FilterDataResponse>>

    @GET("cities")
    suspend fun getCities(
        @Query("countryId") countryId: Int
    ): Response<List<FilterDataResponse>>

    @GET("Services/{serviceId}")
    suspend fun getService(
        @Path("serviceId") serviceId: Int
    ): Response<ServiceModelResponse>
}