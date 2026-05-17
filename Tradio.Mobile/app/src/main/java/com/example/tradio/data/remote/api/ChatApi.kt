package com.example.tradio.data.remote.api

import com.example.tradio.data.remote.dto.requests.CreateMessageRequest
import com.example.tradio.data.remote.dto.responses.ChatListItemResponse
import com.example.tradio.data.remote.dto.responses.ChatModelResponse
import com.example.tradio.data.remote.dto.responses.MessageModelResponse
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Query

interface ChatApi {
    @GET("ApplicationUserServices/received-service")
    suspend fun getReceivedServiceChats(): Response<List<ChatListItemResponse>>

    @GET("ApplicationUserServices/provided-service")
    suspend fun getProvidedServiceChats(): Response<List<ChatListItemResponse>>

    @GET("Messages/by-service")
    suspend fun getMessagesByService(
        @Query("serviceId") serviceId: Int
    ): Response<ChatModelResponse>

    @GET("Messages/by-application-user-service")
    suspend fun getMessagesByChat(
        @Query("applicationUserServiceId") chatId: Int
    ): Response<ChatModelResponse>

    @POST("Messages")
    suspend fun createMessage(
        @Body request: CreateMessageRequest
    ): Response<MessageModelResponse>
}