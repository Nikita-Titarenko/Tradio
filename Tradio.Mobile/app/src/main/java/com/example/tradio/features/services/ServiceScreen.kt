package com.example.tradio.features.services

import androidx.compose.foundation.layout.*
import androidx.compose.material3.Button
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.example.tradio.data.remote.dto.responses.ServiceModelResponse
import com.example.tradio.data.remote.source.ServiceService
import kotlinx.coroutines.launch

@Composable
fun ServiceScreen(
    serviceId: Int,
    serviceService: ServiceService,
    onStartChat: (Int) -> Unit
) {
    val scope = rememberCoroutineScope()
    var service by remember { mutableStateOf<ServiceModelResponse?>(null) }
    var isLoading by remember { mutableStateOf(true) }

    LaunchedEffect(serviceId) {
        scope.launch {
            service = serviceService.getService(serviceId)
            isLoading = false
        }
    }

    Box(
        modifier = Modifier
            .fillMaxSize()
            .padding(24.dp)
    ) {
        if (isLoading) {
            CircularProgressIndicator(modifier = Modifier.align(Alignment.Center))
        } else {
            service?.let { item ->
                Column(
                    modifier = Modifier.fillMaxWidth(),
                    verticalArrangement = Arrangement.spacedBy(16.dp)
                ) {
                    Text(
                        text = item.name ?: "Без назви",
                        fontSize = 28.sp,
                        fontWeight = FontWeight.Bold
                    )

                    Text(
                        text = "${item.price} Кредитів",
                        fontSize = 20.sp,
                        color = MaterialTheme.colorScheme.primary,
                        fontWeight = FontWeight.SemiBold
                    )

                    if (item.description.isNotEmpty()) {
                        Text(
                            text = item.description,
                            fontSize = 16.sp,
                            color = MaterialTheme.colorScheme.onSurface,
                            modifier = Modifier.padding(vertical = 8.dp)
                        )
                    }

                    Spacer(modifier = Modifier.height(16.dp))

                    Text(
                        text = "Створено: ${item.creationDateTime?.take(10) ?: ""}",
                        fontSize = 14.sp,
                        color = MaterialTheme.colorScheme.onSurfaceVariant
                    )

                    Text(
                        text = "Користувач: ${item.applicationUserName ?: ""}",
                        fontSize = 16.sp,
                        fontWeight = FontWeight.Medium
                    )

                    Spacer(modifier = Modifier.weight(1f))

                    Button(
                        onClick = { onStartChat(item.id) },
                        modifier = Modifier.fillMaxWidth()
                    ) {
                        Text(text = "Почати чат")
                    }
                }
            } ?: Box(
                modifier = Modifier.fillMaxSize(),
                contentAlignment = Alignment.Center
            ) {
                Text(text = "Помилка завантаження послуги")
            }
        }
    }
}