package com.example.tradio.features.chats

import android.util.Log
import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.lazy.rememberLazyListState
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Home
import androidx.compose.material.icons.filled.Send
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextOverflow
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.example.tradio.data.remote.dto.requests.CreateMessageRequest
import com.example.tradio.data.remote.dto.requests.CreatePaymentRequest
import com.example.tradio.data.remote.dto.responses.ChatListItemResponse
import com.example.tradio.data.remote.dto.responses.ChatModelResponse
import com.example.tradio.data.remote.dto.responses.MessageModelResponse
import com.example.tradio.data.remote.source.ChatService
import com.example.tradio.data.remote.source.NotificationService
import com.example.tradio.data.remote.source.PaymentService
import com.example.tradio.data.remote.source.UserService
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import java.util.Locale

@Composable
fun ChatsScreen(
    initialServiceId: Int?,
    chatService: ChatService,
    notificationService: NotificationService,
    userService: UserService,
    paymentService: PaymentService,
    currentUserId: String,
    onBackToServices: () -> Unit
) {
    val scope = rememberCoroutineScope()

    var chatType by remember { mutableStateOf("received") }
    var chatList by remember { mutableStateOf<List<ChatListItemResponse>>(emptyList()) }
    var activeChat by remember { mutableStateOf<ChatModelResponse?>(null) }
    var userBalance by remember { mutableStateOf<Double?>(null) }

    val activeChatMessages = remember { mutableStateListOf<MessageModelResponse>() }

    var messageText by remember { mutableStateOf("") }
    var errorMessage by remember { mutableStateOf("") }
    val scrollState = rememberLazyListState()

    LaunchedEffect(currentUserId) {
        if (currentUserId.isNotEmpty()) {
            scope.launch {
                try {
                    val user = userService.getUser(currentUserId)
                    userBalance = user?.creditCount
                } catch (e: Exception) {
                    Log.e("ChatsScreen", "Failed to fetch user balance: ${e.localizedMessage}")
                }
            }
        }
    }

    LaunchedEffect(currentUserId, chatList) {
        if (currentUserId.isNotEmpty() && chatList.isNotEmpty()) {
            scope.launch(Dispatchers.IO) {
                notificationService.start(currentUserId) {
                    val chatIds = chatList.map { it.id }
                    notificationService.joinToChats(chatIds)
                }
            }
        }
    }

    LaunchedEffect(Unit) {
        notificationService.messages.collect { incomingMsg ->
            withContext(Dispatchers.Main) {
                if (activeChat != null && incomingMsg.applicationUserServiceId == activeChat?.applicationUserServiceId) {
                    val newUiMessage = MessageModelResponse(
                        text = incomingMsg.text,
                        creationDateTime = incomingMsg.creationDateTime,
                        isYourMessage = incomingMsg.senderId == currentUserId
                    )

                    val isDuplicate = activeChatMessages.any {
                        it.text == newUiMessage.text && it.creationDateTime == newUiMessage.creationDateTime
                    }

                    if (!isDuplicate) {
                        activeChatMessages.add(newUiMessage)
                    }
                }

                chatList = chatList.map { chatItem ->
                    if (chatItem.id == incomingMsg.applicationUserServiceId) {
                        chatItem.copy(
                            lastMessageText = incomingMsg.text,
                            lastMessageDateTime = incomingMsg.creationDateTime
                        )
                    } else {
                        chatItem
                    }
                }
            }
        }
    }

    LaunchedEffect(chatType) {
        scope.launch {
            val res = if (chatType == "received") {
                chatService.getReceivedServiceChats()
            } else {
                chatService.getProvidedServiceChats()
            }
            chatList = res ?: emptyList()
        }
    }

    LaunchedEffect(initialServiceId) {
        if (initialServiceId != null && initialServiceId > 0) {
            scope.launch {
                val chat = chatService.getMessagesByService(initialServiceId)
                if (chat != null) {
                    activeChat = chat
                    activeChatMessages.clear()
                    activeChatMessages.addAll(chat.messages)
                }
            }
        }
    }

    LaunchedEffect(activeChatMessages.size) {
        if (activeChatMessages.size > 0) {
            scrollState.animateScrollToItem(activeChatMessages.size - 1)
        }
    }

    Row(modifier = Modifier.fillMaxSize()) {
        Column(
            modifier = Modifier
                .weight(0.4f)
                .fillMaxHeight()
                .background(MaterialTheme.colorScheme.surfaceVariant.copy(alpha = 0.4f))
                .padding(8.dp)
        ) {
            Row(
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(bottom = 8.dp),
                verticalAlignment = Alignment.CenterVertically,
                horizontalArrangement = Arrangement.SpaceBetween
            ) {
                IconButton(onClick = onBackToServices) {
                    Icon(
                        imageVector = Icons.Default.Home,
                        contentDescription = "Home",
                        tint = MaterialTheme.colorScheme.primary
                    )
                }

                userBalance?.let { balance ->
                    Text(
                        text = "Баланс: ₴${String.format(Locale.US, "%.2f", balance)}",
                        fontSize = 14.sp,
                        fontWeight = FontWeight.Bold,
                        color = MaterialTheme.colorScheme.primary,
                        modifier = Modifier.padding(end = 4.dp)
                    )
                }
            }

            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.spacedBy(4.dp)
            ) {
                Button(
                    onClick = { chatType = "received" },
                    modifier = Modifier.weight(1f),
                    colors = ButtonDefaults.buttonColors(
                        containerColor = if (chatType == "received") MaterialTheme.colorScheme.primary else MaterialTheme.colorScheme.surfaceVariant,
                        contentColor = if (chatType == "received") MaterialTheme.colorScheme.onPrimary else MaterialTheme.colorScheme.onSurfaceVariant
                    ),
                    contentPadding = PaddingValues(vertical = 4.dp)
                ) {
                    Text("Отримані", fontSize = 12.sp)
                }
                Button(
                    onClick = { chatType = "provided" },
                    modifier = Modifier.weight(1f),
                    colors = ButtonDefaults.buttonColors(
                        containerColor = if (chatType == "provided") MaterialTheme.colorScheme.primary else MaterialTheme.colorScheme.surfaceVariant,
                        contentColor = if (chatType == "provided") MaterialTheme.colorScheme.onPrimary else MaterialTheme.colorScheme.onSurfaceVariant
                    ),
                    contentPadding = PaddingValues(vertical = 4.dp)
                ) {
                    Text("Надані", fontSize = 12.sp)
                }
            }

            Spacer(modifier = Modifier.height(8.dp))

            LazyColumn(modifier = Modifier.fillMaxSize()) {
                items(chatList) { item ->
                    val isSelected = item.id == activeChat?.applicationUserServiceId
                    Column(
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(vertical = 4.dp)
                            .background(
                                color = if (isSelected) MaterialTheme.colorScheme.secondaryContainer else Color.Transparent,
                                shape = RoundedCornerShape(8.dp)
                            )
                            .clickable {
                                scope.launch {
                                    val fullChat = chatService.getMessagesByChat(item.id)
                                    if (fullChat != null) {
                                        activeChat = fullChat
                                        activeChatMessages.clear()
                                        activeChatMessages.addAll(fullChat.messages)
                                        errorMessage = ""
                                    } else {
                                        errorMessage = "Помилка завантаження чату"
                                    }
                                }
                            }
                            .padding(8.dp)
                    ) {
                        Text(text = item.fullName ?: "Користувач", fontWeight = FontWeight.Bold, fontSize = 14.sp)
                        Row(
                            modifier = Modifier.fillMaxWidth(),
                            horizontalArrangement = Arrangement.SpaceBetween
                        ) {
                            Text(
                                text = item.lastMessageText ?: "",
                                fontSize = 12.sp,
                                maxLines = 1,
                                overflow = TextOverflow.Ellipsis,
                                modifier = Modifier.weight(1f)
                            )
                            Text(
                                text = item.lastMessageDateTime?.take(16) ?: "",
                                fontSize = 10.sp,
                                color = Color.Gray
                            )
                        }
                    }
                }
            }
        }

        Box(
            modifier = Modifier
                .weight(0.6f)
                .fillMaxHeight()
                .padding(8.dp)
        ) {
            activeChat?.let { chat ->
                Column(modifier = Modifier.fillMaxSize()) {
                    Row(
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(bottom = 8.dp),
                        horizontalArrangement = Arrangement.SpaceBetween,
                        verticalAlignment = Alignment.CenterVertically
                    ) {
                        Column(
                            modifier = Modifier
                                .weight(1f)
                                .padding(end = 8.dp)
                        ) {
                            Text(
                                text = chat.fullName ?: "Чат",
                                fontWeight = FontWeight.Bold,
                                fontSize = 18.sp,
                                maxLines = 1,
                                overflow = TextOverflow.Ellipsis
                            )
                            Row(
                                modifier = Modifier.fillMaxWidth(),
                                verticalAlignment = Alignment.CenterVertically,
                                horizontalArrangement = Arrangement.spacedBy(4.dp)
                            ) {
                                Text(
                                    text = chat.serviceName ?: "",
                                    fontSize = 14.sp,
                                    color = MaterialTheme.colorScheme.primary,
                                    maxLines = 1,
                                    overflow = TextOverflow.Ellipsis,
                                    modifier = Modifier.weight(1f, fill = false)
                                )
                                chat.price?.let { price ->
                                    Text(
                                        text = "(₴${String.format(Locale.US, "%.2f", price)})",
                                        fontSize = 14.sp,
                                        fontWeight = FontWeight.Medium,
                                        color = MaterialTheme.colorScheme.outline
                                    )
                                }
                            }
                        }

                        if (chatType == "received" && chat.applicationUserServiceId != null) {
                            Button(
                                onClick = {
                                    scope.launch {
                                        try {
                                            val req = CreatePaymentRequest(
                                                applicationUserServiceId = chat.applicationUserServiceId
                                            )
                                            val success = paymentService.createPayment(req)
                                            if (success != null) {
                                                val updatedUser = userService.getUser(currentUserId)
                                                userBalance = updatedUser?.creditCount
                                            }
                                        } catch (e: Exception) {
                                            errorMessage = "Помилка проведення оплати"
                                        }
                                    }
                                },
                                colors = ButtonDefaults.buttonColors(containerColor = Color(0xFF4CAF50)),
                                contentPadding = PaddingValues(horizontal = 16.dp, vertical = 4.dp),
                                modifier = Modifier.wrapContentWidth()
                            ) {
                                Text("Оплатити", color = Color.White, fontSize = 14.sp)
                            }
                        }
                    }

                    if (errorMessage.isNotEmpty()) {
                        Text(text = errorMessage, color = MaterialTheme.colorScheme.error, fontSize = 12.sp)
                    }

                    LazyColumn(
                        state = scrollState,
                        modifier = Modifier
                            .fillMaxWidth()
                            .weight(1f)
                            .padding(vertical = 8.dp)
                    ) {
                        items(activeChatMessages) { msg ->
                            val alignment = if (msg.isYourMessage) Alignment.End else Alignment.Start
                            val bg = if (msg.isYourMessage) MaterialTheme.colorScheme.primaryContainer else MaterialTheme.colorScheme.surfaceVariant

                            Column(
                                modifier = Modifier
                                    .fillMaxWidth()
                                    .padding(vertical = 4.dp),
                                horizontalAlignment = alignment
                            ) {
                                Box(
                                    modifier = Modifier
                                        .background(bg, shape = RoundedCornerShape(12.dp))
                                        .padding(10.dp)
                                ) {
                                    Column {
                                        Text(text = msg.text ?: "", fontSize = 14.sp)
                                        Text(
                                            text = msg.creationDateTime?.take(16) ?: "",
                                            fontSize = 9.sp,
                                            color = Color.Gray,
                                            modifier = Modifier.align(Alignment.End)
                                        )
                                    }
                                }
                            }
                        }
                    }

                    Row(
                        modifier = Modifier.fillMaxWidth(),
                        verticalAlignment = Alignment.CenterVertically,
                        horizontalArrangement = Arrangement.spacedBy(8.dp)
                    ) {
                        OutlinedTextField(
                            value = messageText,
                            onValueChange = { messageText = it },
                            placeholder = { Text("Напишіть повідомлення...") },
                            modifier = Modifier.weight(1f),
                            maxLines = 3
                        )
                        IconButton(
                            onClick = {
                                if (messageText.isNotBlank()) {
                                    scope.launch {
                                        val req = CreateMessageRequest(
                                            text = messageText,
                                            serviceId = chat.serviceId,
                                            receiverId = chat.applicationUserId
                                        )
                                        val sentMsg = chatService.createMessage(req)
                                        if (sentMsg != null) {
                                            val isDuplicate = activeChatMessages.any {
                                                it.text == sentMsg.text && it.creationDateTime == sentMsg.creationDateTime
                                            }
                                            if (!isDuplicate) {
                                                activeChatMessages.add(sentMsg)
                                            }
                                            messageText = ""
                                        }
                                    }
                                }
                            }
                        ) {
                            Icon(imageVector = Icons.Default.Send, contentDescription = "Send")
                        }
                    }
                }
            } ?: Box(
                modifier = Modifier.fillMaxSize(),
                contentAlignment = Alignment.Center
            ) {
                Text("Виберіть чат для початку спілкування", color = Color.Gray)
            }
        }
    }
}