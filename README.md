# 🌐 MetaVerse Chatbot Application

An AI-powered **Metaverse web application** built using **.NET Core Web API** and **Google Dialogflow**.
This project enables users to interact with the platform using a smart chatbot and explore features through natural language.

---

## 🚀 Features

* 🤖 Dialogflow-based AI Chatbot
* 💬 Real-time conversation handling
* 🧠 Intent-based NLP responses
* 🔗 Integrated with .NET backend
* ⚡ Fast and scalable API structure
* 🌐 Interactive Metaverse experience

---

## 🏗️ Tech Stack

* **Backend**: .NET Core Web API
* **Frontend**: HTML, CSS, JavaScript
* **AI Chatbot**: Google Dialogflow
* **Authentication**: Google Service Account (JSON Key)

---

## 📂 Project Structure

```
MetaVerse/
│── Controllers/
│   └── ChatController.cs
│── Models/
│── Services/
│── wwwroot/
│── appsettings.json
│── Program.cs
```

---

## ⚙️ Setup Instructions

### 1️⃣ Clone the Repository

```
git clone https://github.com/reactwithsmile/MetaVerse.git
cd MetaVerse
```

---

### 2️⃣ Setup Dialogflow

* Create a Dialogflow Agent
* Add intents (Greeting, Help, Navigation, etc.)
* Train the chatbot

---

### 3️⃣ Add Credentials

Download your **Service Account JSON file**

Set environment variable:

```
setx GOOGLE_APPLICATION_CREDENTIALS "D:\path\dialogflow.json"
```

---

### 4️⃣ Configure Project ID

Update `appsettings.json`:

```
{
  "Dialogflow": {
    "ProjectId": "your-project-id"
  }
}
```

---

### 5️⃣ Run the Application

```
dotnet run
```

Open browser:

```
https://localhost:xxxx
```

---

## 💡 How It Works

1. User sends message
2. .NET API sends request to Dialogflow
3. Dialogflow detects intent
4. Response returned to UI

---

## 🔥 Example Intents

* Greeting → "Hi", "Hello"
* Help → "I need help"
* Status → "I am good"
* Navigation → "How to use this?"

---

## ⚠️ Common Issues

**1. Credentials Error**
→ Set `GOOGLE_APPLICATION_CREDENTIALS`

**2. Project ID Missing**
→ Add in `appsettings.json`

**3. Bot Not Responding Properly**
→ Add more training phrases in Dialogflow

---

## 📈 Future Improvements

* 🌍 Live deployment
* 🎨 Improved UI/UX
* 🔐 Authentication system
* 📊 Analytics for chatbot

---

## 👩‍💻 Author

**Sneha**
.NET Developer | Backend Engineer

---

## ⭐ Support

If you like this project, give it a ⭐ on GitHub!
