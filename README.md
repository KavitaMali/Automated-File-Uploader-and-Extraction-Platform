# Automated-File-Uploader-and-Extraction-Platform

This project is a cloud-native file upload and document extraction system built with .NET 8, Azure Blob Storage, API Management, and Logic Apps.

## 🔧 Features

- 📤 Upload files (PDF, TXT) via ASP.NET Core Web API
- ☁️ Files stored in Azure Blob Storage
- ⚙️ Triggered Logic App on blob upload
- 🧠 Text/PDF content extracted using Azure Form Recognizer
- 🔐 API secured and exposed via Azure API Management (APIM)

## 🛠️ Tech Stack

- .NET 8 (ASP.NET Core Web API)
- Azure App Service
- Azure Blob Storage
- Azure Logic Apps
- Azure API Management (APIM)
- Azure Form Recognizer

## 📦 Architecture

```plaintext
Client → APIM → ASP.NET Web API → Blob Storage
                            ↓
                     Trigger: Logic App
                            ↓
                   → Form Recognizer → Extracted JSON


