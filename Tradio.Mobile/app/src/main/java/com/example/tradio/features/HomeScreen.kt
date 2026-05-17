package com.example.tradio.features

import androidx.compose.foundation.Image
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.verticalScroll
import androidx.compose.material3.Button
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.example.tradio.R

@Composable
fun HomeScreen(modifier: Modifier = Modifier, onNavigateToLogin: () -> Unit) {
    Column(
        modifier = modifier
            .fillMaxSize()
            .padding(24.dp)
            .verticalScroll(rememberScrollState()),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {
        Image(
            painter = painterResource(id = R.drawable.hero_image),
            contentDescription = "Hero Image",
            modifier = Modifier
                .size(280.dp)
                .padding(bottom = 32.dp)
        )

        Text(
            text = "TRADIO",
            fontSize = 42.sp,
            fontWeight = FontWeight.Bold,
            textAlign = TextAlign.Center
        )

        Text(
            text = "Ваш надійний партнер у світі торгівлі.",
            fontSize = 18.sp,
            color = Color.Gray,
            textAlign = TextAlign.Center,
            modifier = Modifier.padding(vertical = 16.dp)
        )

        Spacer(modifier = Modifier.height(24.dp))

        Button(
            onClick = { onNavigateToLogin() },
            modifier = Modifier.fillMaxWidth(0.8f)
        ) {
            Text("ПОЧАТИ", fontSize = 16.sp)
        }
    }
}