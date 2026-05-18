#include <WiFi.h>
#include <HTTPClient.h>
#include "DHT.h"
#include <ArduinoJson.h>

#define DHTPIN 15
#define DHTTYPE DHT22

DHT dht(DHTPIN, DHTTYPE);

const char* ssid = "Wokwi-GUEST";
const char* password = "";

String serverUrl = "http://baba145da663.ngrok-free.app/api/Climates"; 

void setup() {
  Serial.begin(115200);
  dht.begin();

  WiFi.begin(ssid, password, 6);
  while (WiFi.status() != WL_CONNECTED) {
    delay(300);
    Serial.print(".");
  }

  Serial.println("\nWiFi connected!");
}

void loop() {
  float t = dht.readTemperature();
  float h = dht.readHumidity();

  if (isnan(t) || isnan(h)) {
    Serial.println("DHT read failed!");
    delay(2000);
    return;
  }

  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;

    http.begin(serverUrl); 

    http.addHeader("Content-Type", "application/json");
    http.addHeader("ngrok-skip-browser-warning", "true");

    http.addHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyZDEzMzQwYy1kOWFiLTQ0MzktYmQ3Yy04OTdjMTJmOGNlYmEiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NjMxMjI4MDksImV4cCI6MTc2NTcxNDgwOSwiaWF0IjoxNzYzMTIyODA5LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDMxNC8iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDMxNC8ifQ.URfM5raV-tR0PAhmtwutFTV4_kvyKNvDyS4X2te-GZc");

    StaticJsonDocument<100> doc;
    doc["temperature"] = t;
    doc["humidity"] = h;

    String json;
    serializeJson(doc, json);

    int code = http.POST(json);
    Serial.print("Server response code: ");
    Serial.println(code);

    http.end();
  }

  delay(5000);
}
