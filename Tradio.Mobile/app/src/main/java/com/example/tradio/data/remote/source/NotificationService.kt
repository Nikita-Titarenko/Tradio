package com.example.tradio.data.remote.source

import android.util.Log
import com.example.tradio.data.remote.dto.responses.MessageFromSignalRModel
import com.microsoft.signalr.HubConnection
import com.microsoft.signalr.HubConnectionBuilder
import com.microsoft.signalr.HubConnectionState
import kotlinx.coroutines.flow.MutableSharedFlow
import kotlinx.coroutines.flow.asSharedFlow

class NotificationService {

    private var hubConnection: HubConnection? = null

    private val _messages = MutableSharedFlow<MessageFromSignalRModel>(extraBufferCapacity = 64)
    val messages = _messages.asSharedFlow()

    private val hubUrl = "http://10.0.2.2:5188/chatHub"

    fun start(userId: String, onConnected: () -> Unit = {}) {
        if (hubConnection?.connectionState == HubConnectionState.CONNECTED) {
            onConnected()
            return
        }

        hubConnection = HubConnectionBuilder
            .create(hubUrl)
            .build()

        hubConnection?.on("ReceiveMessage", { message ->
            Log.d("SignalR", "Received object: $message")
            _messages.tryEmit(message)
        }, MessageFromSignalRModel::class.java)

        try {
            hubConnection?.start()?.blockingAwait()
            Log.d("SignalR", "Connection established successfully")

            joinUser(userId)
            onConnected()
        } catch (e: Exception) {
            Log.e("SignalR", "SignalR start error: ${e.localizedMessage}")
        }
    }

    fun joinToChats(chatIds: List<Int>) {
        if (hubConnection?.connectionState == HubConnectionState.CONNECTED) {
            for (chatId in chatIds) {
                Log.d("SignalR", "Sending AddToChatAsync for chat ID: $chatId")
                hubConnection?.send("AddToChatAsync", chatId)
            }
        } else {
            Log.e("SignalR", "Cannot join chats: Hub is not connected!")
        }
    }

    private fun joinUser(userId: String) {
        if (hubConnection?.connectionState == HubConnectionState.CONNECTED) {
            hubConnection?.send("AddUserAsync", userId)
        }
    }

    fun stop() {
        hubConnection?.stop()?.blockingAwait()
    }
}