package com.example.tradio.features.payments

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.automirrored.filled.ArrowBack
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.text.style.TextOverflow
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.example.tradio.data.remote.dto.responses.PaymentModelResponse
import com.example.tradio.data.remote.source.PaymentService
import kotlinx.coroutines.launch
import java.util.Locale

@Composable
fun PaymentsScreen(
    paymentService: PaymentService,
    onBack: () -> Unit
) {
    val scope = rememberCoroutineScope()
    var payments by remember { mutableStateOf<List<PaymentModelResponse>?>(null) }
    var isLoading by remember { mutableStateOf(true) }

    LaunchedEffect(Unit) {
        scope.launch {
            payments = paymentService.getPayments()
            isLoading = false
        }
    }

    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp)
    ) {
        Row(
            modifier = Modifier
                .fillMaxWidth()
                .padding(bottom = 16.dp),
            verticalAlignment = Alignment.CenterVertically
        ) {
            IconButton(onClick = onBack) {
                Icon(
                    imageVector = Icons.AutoMirrored.Filled.ArrowBack,
                    contentDescription = "Назад"
                )
            }
            Spacer(modifier = Modifier.width(8.dp))
            Text(
                text = "Історія оплат",
                fontSize = 24.sp,
                fontWeight = FontWeight.Bold
            )
        }

        if (isLoading) {
            Box(modifier = Modifier.fillMaxSize(), contentAlignment = Alignment.Center) {
                CircularProgressIndicator()
            }
        } else {
            payments?.let { paymentList ->
                if (paymentList.isEmpty()) {
                    Box(modifier = Modifier.fillMaxSize(), contentAlignment = Alignment.Center) {
                        Text(
                            text = "Платежів не знайдено",
                            fontSize = 16.sp,
                            textAlign = TextAlign.Center,
                            color = MaterialTheme.colorScheme.onSurfaceVariant
                        )
                    }
                } else {
                    LazyColumn(
                        modifier = Modifier.fillMaxWidth(),
                        verticalArrangement = Arrangement.spacedBy(8.dp)
                    ) {
                        items(paymentList) { payment ->
                            Card(
                                modifier = Modifier.fillMaxWidth(),
                                shape = RoundedCornerShape(12.dp),
                                colors = CardDefaults.cardColors(
                                    containerColor = MaterialTheme.colorScheme.surfaceVariant.copy(alpha = 0.5f)
                                )
                            ) {
                                Row(
                                    modifier = Modifier
                                        .fillMaxWidth()
                                        .padding(16.dp),
                                    horizontalArrangement = Arrangement.SpaceBetween,
                                    verticalAlignment = Alignment.CenterVertically
                                ) {
                                    Column(modifier = Modifier.weight(1f)) {
                                        Text(
                                            text = payment.serviceName ?: "Послуга",
                                            fontWeight = FontWeight.SemiBold,
                                            fontSize = 16.sp,
                                            maxLines = 1,
                                            overflow = TextOverflow.Ellipsis
                                        )

                                        Spacer(modifier = Modifier.height(4.dp))

                                        Row(
                                            verticalAlignment = Alignment.CenterVertically,
                                            horizontalArrangement = Arrangement.spacedBy(8.dp)
                                        ) {
                                            Text(
                                                text = if (payment.areYouProvider) "Виконавець" else "Замовник",
                                                fontSize = 12.sp,
                                                fontWeight = FontWeight.Medium,
                                                color = if (payment.areYouProvider) Color(0xFF4CAF50) else MaterialTheme.colorScheme.primary
                                            )

                                            payment.creationDateTime?.let { date ->
                                                Text(
                                                    text = date.take(16).replace("T", " "),
                                                    fontSize = 12.sp,
                                                    color = MaterialTheme.colorScheme.outline
                                                )
                                            }
                                        }
                                    }

                                    val prefix = if (payment.areYouProvider) "+₴" else "-₴"
                                    val priceColor = if (payment.areYouProvider) Color(0xFF4CAF50) else MaterialTheme.colorScheme.error

                                    Text(
                                        text = "$prefix${String.format(Locale.US, "%.2f", payment.price)}",
                                        fontWeight = FontWeight.Bold,
                                        fontSize = 16.sp,
                                        color = priceColor
                                    )
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}