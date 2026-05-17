package com.example.tradio

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Scaffold
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.remember
import androidx.compose.ui.Modifier
import androidx.navigation.NavType
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import androidx.navigation.navArgument
import com.example.tradio.data.remote.network.RetrofitClient
import com.example.tradio.data.remote.source.ChatService
import com.example.tradio.data.remote.source.UserService
import com.example.tradio.data.remote.source.ServiceService
import com.example.tradio.data.remote.source.NotificationService
import com.example.tradio.data.remote.source.PaymentService
import com.example.tradio.features.HomeScreen
import com.example.tradio.features.auth.ConfirmEmailScreen
import com.example.tradio.features.auth.LoginScreen
import com.example.tradio.features.auth.RegisterScreen
import com.example.tradio.features.services.ServicesScreen
import com.example.tradio.features.services.ServiceScreen
import com.example.tradio.features.chats.ChatsScreen
import com.example.tradio.features.payments.PaymentsScreen
import com.example.tradio.ui.theme.TradioTheme

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContent {
            TradioTheme {
                AppNavigation()
            }
        }
    }

    @Composable
    fun AppNavigation() {
        val navController = rememberNavController()
        val context = androidx.compose.ui.platform.LocalContext.current
        val userService = remember { UserService(RetrofitClient.userApi, context) }
        RetrofitClient.tokenProvider = {
            userService.getToken()
        }
        val serviceService = remember { ServiceService(RetrofitClient.serviceApi) }
        val chatService = remember { ChatService(RetrofitClient.chatApi) }
        val paymentService = remember { PaymentService(RetrofitClient.paymentApi) }
        val notificationService = remember { NotificationService() }

        Scaffold(modifier = Modifier.fillMaxSize()) { innerPadding ->
            NavHost(
                navController = navController,
                startDestination = "home",
                modifier = Modifier.padding(innerPadding)
            ) {
                composable("home") {
                    LaunchedEffect(Unit) {
                        if (userService.getToken() != null) {
                            navController.navigate("services") {
                                popUpTo("home") { inclusive = true }
                            }
                        }
                    }

                    HomeScreen(
                        onNavigateToLogin = {
                            navController.navigate("login")
                        }
                    )
                }

                composable("login") {
                    LoginScreen(
                        onNavigateToRegister = {
                            navController.navigate("register")
                        },
                        onLoginSuccess = {
                            navController.navigate("services") {
                                popUpTo("login") { inclusive = true }
                            }
                        },
                        userService = userService
                    )
                }

                composable("register") {
                    RegisterScreen(
                        userService = userService,
                        onRegisterSuccess = { userId ->
                            navController.navigate("confirm-email/$userId")
                        },
                        onNavigateToLogin = { navController.navigate("login") }
                    )
                }

                composable("confirm-email/{userId}") { backStackEntry ->
                    val userId = backStackEntry.arguments?.getString("userId") ?: ""

                    ConfirmEmailScreen(
                        userId = userId,
                        userService = userService,
                        onConfirmSuccess = {
                            navController.navigate("services") {
                                popUpTo("confirm-email/{userId}") { inclusive = true }
                            }
                        }
                    )
                }

                composable("services") {
                    ServicesScreen(
                        serviceService = serviceService,
                        onServiceClick = { id ->
                            navController.navigate("service/$id")
                        },
                        onNavigateToChats = {
                            navController.navigate("chats")
                        },
                        onNavigateToPayments = {
                            navController.navigate("payments")
                        },
                        onLogout = {
                            userService.logout()
                            notificationService.stop()
                            navController.navigate("home") {
                                popUpTo("services") { inclusive = true }
                            }
                        }
                    )
                }

                composable(
                    route = "service/{serviceId}",
                    arguments = listOf(navArgument("serviceId") { type = NavType.IntType })
                ) { backStackEntry ->
                    val serviceId = backStackEntry.arguments?.getInt("serviceId") ?: 0
                    ServiceScreen(
                        serviceId = serviceId,
                        serviceService = serviceService,
                        onStartChat = { id ->
                            navController.navigate("chats?serviceId=$id")
                        }
                    )
                }

                composable(
                    route = "chats?serviceId={serviceId}",
                    arguments = listOf(navArgument("serviceId") {
                        type = NavType.IntType
                        defaultValue = -1
                    })
                ) { backStackEntry ->
                    val serviceId = backStackEntry.arguments?.getInt("serviceId").takeIf { it != -1 }
                    val currentUserId = userService.getCurrentUserId() ?: ""

                    ChatsScreen(
                        initialServiceId = serviceId,
                        chatService = chatService,
                        notificationService = notificationService,
                        userService = userService,
                        paymentService = paymentService,
                        currentUserId = currentUserId,
                        onBackToServices = { navController.navigate("services") }
                    )
                }

                composable("payments") {
                    PaymentsScreen(
                        paymentService = paymentService,
                        onBack = { navController.popBackStack() }
                    )
                }
            }
        }
    }
}