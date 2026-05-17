package com.example.tradio.features.services

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.automirrored.filled.ExitToApp
import androidx.compose.material.icons.filled.Email
import androidx.compose.material.icons.filled.Wallet
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.example.tradio.data.remote.dto.responses.FilterDataResponse
import com.example.tradio.data.remote.dto.responses.ServiceListItemResponse
import com.example.tradio.data.remote.source.ServiceService
import com.example.tradio.shared.DropdownComponent
import com.example.tradio.shared.ServiceItemComponent
import kotlinx.coroutines.launch

@Composable
fun ServicesScreen(
    serviceService: ServiceService,
    onServiceClick: (Int) -> Unit,
    onNavigateToChats: () -> Unit,
    onNavigateToPayments: () -> Unit,
    onLogout: () -> Unit
) {
    val scope = rememberCoroutineScope()

    var services by remember { mutableStateOf<List<ServiceListItemResponse>?>(null) }
    var isLoading by remember { mutableStateOf(true) }

    var categories by remember { mutableStateOf<List<FilterDataResponse>>(emptyList()) }
    var subcategories by remember { mutableStateOf<List<FilterDataResponse>>(emptyList()) }
    var countries by remember { mutableStateOf<List<FilterDataResponse>>(emptyList()) }
    var cities by remember { mutableStateOf<List<FilterDataResponse>>(emptyList()) }

    var selectedCategoryId by remember { mutableStateOf<Int?>(null) }
    var selectedSubcategoryId by remember { mutableStateOf<Int?>(null) }
    var selectedCountryId by remember { mutableStateOf<Int?>(null) }
    var selectedCityId by remember { mutableStateOf<Int?>(null) }

    val pageNumber = 1
    val pageSize = 10

    LaunchedEffect(Unit) {
        scope.launch {
            serviceService.getCategories()?.let { categories = it }
            serviceService.getCountries()?.let { countries = it }
        }
    }

    LaunchedEffect(selectedCategoryId) {
        selectedCategoryId?.let { id ->
            scope.launch {
                serviceService.getCategories(id)?.let { subcategories = it }
            }
        } ?: run {
            subcategories = emptyList()
            selectedSubcategoryId = null
        }
    }

    LaunchedEffect(selectedCountryId) {
        selectedCountryId?.let { id ->
            scope.launch {
                serviceService.getCities(id)?.let { cities = it }
            }
        } ?: run {
            cities = emptyList()
            selectedCityId = null
        }
    }

    LaunchedEffect(selectedSubcategoryId, selectedCountryId, selectedCityId) {
        isLoading = true
        scope.launch {
            val result = serviceService.getServices(
                pageNumber = pageNumber,
                pageSize = pageSize,
                categoryId = selectedSubcategoryId,
                cityId = selectedCityId
            )
            services = result
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
                .padding(bottom = 12.dp),
            horizontalArrangement = Arrangement.SpaceBetween,
            verticalAlignment = Alignment.CenterVertically
        ) {
            Text(
                text = "Послуги",
                fontSize = 24.sp,
                fontWeight = FontWeight.Bold,
                modifier = Modifier.weight(1f)
            )

            Row(
                horizontalArrangement = Arrangement.spacedBy(4.dp),
                verticalAlignment = Alignment.CenterVertically
            ) {
                IconButton(onClick = onNavigateToChats) {
                    Icon(
                        imageVector = Icons.Default.Email,
                        contentDescription = "Чати",
                        tint = MaterialTheme.colorScheme.primary
                    )
                }

                IconButton(onClick = onNavigateToPayments) {
                    Icon(
                        imageVector = Icons.Default.Wallet,
                        contentDescription = "Платежі",
                        tint = MaterialTheme.colorScheme.primary
                    )
                }

                IconButton(onClick = onLogout) {
                    Icon(
                        imageVector = Icons.AutoMirrored.Filled.ExitToApp,
                        contentDescription = "Вийти",
                        tint = MaterialTheme.colorScheme.error
                    )
                }
            }
        }

        Row(
            modifier = Modifier.fillMaxWidth(),
            horizontalArrangement = Arrangement.spacedBy(8.dp)
        ) {
            Column(modifier = Modifier.weight(1f)) {
                DropdownComponent(
                    placeholder = "Категорія",
                    items = categories,
                    selectedId = selectedCategoryId,
                    onItemSelected = { selectedCategoryId = it }
                )
                if (subcategories.isNotEmpty()) {
                    Spacer(modifier = Modifier.height(4.dp))
                    DropdownComponent(
                        placeholder = "Підкатегорія",
                        items = subcategories,
                        selectedId = selectedSubcategoryId,
                        onItemSelected = { selectedSubcategoryId = it }
                    )
                }
            }

            Column(modifier = Modifier.weight(1f)) {
                DropdownComponent(
                    placeholder = "Країна",
                    items = countries,
                    selectedId = selectedCountryId,
                    onItemSelected = { selectedCountryId = it }
                )
                if (cities.isNotEmpty()) {
                    Spacer(modifier = Modifier.height(4.dp))
                    DropdownComponent(
                        placeholder = "Місто",
                        items = cities,
                        selectedId = selectedCityId,
                        onItemSelected = { selectedCityId = it }
                    )
                }
            }
        }

        Spacer(modifier = Modifier.height(16.dp))

        if (isLoading) {
            Box(modifier = Modifier.fillMaxSize(), contentAlignment = Alignment.Center) {
                CircularProgressIndicator()
            }
        } else {
            services?.let { serviceList ->
                if (serviceList.isEmpty()) {
                    Box(modifier = Modifier.fillMaxSize(), contentAlignment = Alignment.Center) {
                        Text(
                            text = "Послуг не знайдено",
                            fontSize = 16.sp,
                            textAlign = TextAlign.Center
                        )
                    }
                } else {
                    LazyColumn(
                        modifier = Modifier
                            .fillMaxWidth()
                            .weight(1f)
                    ) {
                        items(serviceList) { service ->
                            ServiceItemComponent(
                                service = service,
                                onServiceClick = onServiceClick
                            )
                        }
                    }
                }
            }
        }
    }
}