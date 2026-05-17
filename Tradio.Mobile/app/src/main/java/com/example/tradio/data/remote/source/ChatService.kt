package com.example.tradio.data.remote.source

import com.example.tradio.data.remote.api.ChatApi
import com.example.tradio.data.remote.dto.requests.CreateMessageRequest
import com.example.tradio.data.remote.dto.responses.ChatListItemResponse
import com.example.tradio.data.remote.dto.responses.ChatModelResponse
import com.example.tradio.data.remote.dto.responses.MessageModelResponse

class ChatService(private val api: ChatApi) {

    suspend fun getReceivedServiceChats(): List<ChatListItemResponse>? {
        val response = api.getReceivedServiceChats()
        return if (response.isSuccessful) response.body() else null
    }

    suspend fun getProvidedServiceChats(): List<ChatListItemResponse>? {
        val response = api.getProvidedServiceChats()
        return if (response.isSuccessful) response.body() else null
    }

    suspend fun getMessagesByService(serviceId: Int): ChatModelResponse? {
        val response = api.getMessagesByService(serviceId)
        return if (response.isSuccessful) response.body() else null
    }

    suspend fun getMessagesByChat(chatId: Int): ChatModelResponse? {
        val response = api.getMessagesByChat(chatId)
        return if (response.isSuccessful) response.body() else null
    }

    suspend fun createMessage(request: CreateMessageRequest): MessageModelResponse? {
        val response = api.createMessage(request)
        return if (response.isSuccessful) response.body() else null
    }
}