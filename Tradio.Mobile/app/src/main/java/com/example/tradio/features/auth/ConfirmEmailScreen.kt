package com.example.tradio.features.auth

import androidx.compose.foundation.layout.*
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.unit.dp
import androidx.navigation.NavController
import com.example.tradio.data.remote.dto.requests.ConfirmEmailRequest
import com.example.tradio.data.remote.source.UserService
import kotlinx.coroutines.launch

@Composable
fun ConfirmEmailScreen(
    userId: String,
    onConfirmSuccess: () -> Unit,
    userService: UserService
) {
    var code by remember { mutableStateOf("") }
    var isLoading by remember { mutableStateOf(false) }
    var errorMessage by remember { mutableStateOf("") }

    val scope = rememberCoroutineScope()

    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {
        Text(
            text = "Confirm Your Email",
            style = MaterialTheme.typography.headlineMedium
        )

        Spacer(modifier = Modifier.height(16.dp))

        OutlinedTextField(
            value = code,
            onValueChange = {
                code = it
                errorMessage = ""
            },
            label = { Text("Verification Code") },
            modifier = Modifier.fillMaxWidth(),
            isError = errorMessage.isNotEmpty(),
            enabled = !isLoading
        )

        if (errorMessage.isNotEmpty()) {
            Text(
                text = errorMessage,
                color = Color.Red,
                modifier = Modifier.padding(top = 8.dp),
                style = MaterialTheme.typography.bodySmall
            )
        }

        Spacer(modifier = Modifier.height(24.dp))

        Button(
            onClick = {
                scope.launch {
                    isLoading = true
                    try {
                        val request = ConfirmEmailRequest(code, userId)
                        val response = userService.confirmEmail(request)

                        if (response != null) {
                            userService.saveToken(response.jwtToken)
                            onConfirmSuccess()
                        } else {
                            errorMessage = "Email confirmation error"
                        }
                    } catch (e: Exception) {
                        errorMessage = e.localizedMessage ?: "Network error"
                    } finally {
                        isLoading = false
                    }
                }
            },
            enabled = code.isNotBlank() && !isLoading,
            modifier = Modifier.fillMaxWidth()
        ) {
            if (isLoading) {
                CircularProgressIndicator(
                    modifier = Modifier.size(24.dp),
                    color = Color.White,
                    strokeWidth = 2.dp
                )
            } else {
                Text("Confirm")
            }
        }
    }
}