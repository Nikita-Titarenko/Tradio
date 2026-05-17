package com.example.tradio.data.remote.source

import android.content.Context
import android.util.Base64
import android.util.Log
import com.example.tradio.data.remote.api.UserApi
import com.example.tradio.data.remote.dto.requests.ConfirmEmailRequest
import com.example.tradio.data.remote.dto.requests.LoginRequest
import com.example.tradio.data.remote.dto.requests.RegisterRequest
import com.example.tradio.data.remote.dto.responses.RegisterResponse
import com.example.tradio.data.remote.dto.responses.SignInResponse
import com.example.tradio.data.remote.dto.responses.UserModelResponse
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext
import org.json.JSONObject

class UserService(private val api: UserApi, private val context: Context) {
    private val prefs = context.getSharedPreferences("auth", Context.MODE_PRIVATE)

    fun saveToken(token: String) {
        prefs.edit().putString("jwtToken", token).apply()
    }

    fun getToken(): String? {
        return prefs.getString("jwtToken", null)
    }

    fun logout() {
        prefs.edit().remove("jwtToken").apply()
    }

    fun getCurrentUserId(): String? {
        val token = getToken() ?: return null
        return try {
            val parts = token.split(".")
            if (parts.size < 2) return null

            val payload64 = parts[1]
            val decodedBytes = Base64.decode(payload64, Base64.URL_SAFE or Base64.NO_WRAP)
            val payloadString = String(decodedBytes, Charsets.UTF_8)

            val jsonObject = JSONObject(payloadString)

            jsonObject.optString("sub", null)
                ?: jsonObject.optString("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", null)
                ?: jsonObject.optString("nameid", null)
        } catch (e: Exception) {
            e.printStackTrace()
            null
        }
    }

    suspend fun login(request: LoginRequest): SignInResponse? {
        val response = api.login(request)
        return if (response.isSuccessful) response.body() else null
    }

    suspend fun register(request: RegisterRequest): RegisterResponse? {
        val response = api.register(request)
        return if (response.isSuccessful) response.body() else null
    }

    suspend fun confirmEmail(request: ConfirmEmailRequest): SignInResponse? {
        val response = api.confirmEmail(request)
        return if (response.isSuccessful) response.body() else null
    }

    suspend fun getUser(userId: String): UserModelResponse? {
        return withContext(Dispatchers.IO) {
            try {
                val response = api.getUser(userId)
                if (response.isSuccessful) {
                    response.body()
                } else {
                    Log.e("UserService", "Get user info error: ${response.code()} - ${response.errorBody()?.string()}")
                    null
                }
            } catch (e: Exception) {
                Log.e("UserService", "Exception fetching user info: ${e.localizedMessage}")
                null
            }
        }
    }
}