# 🎵 Guess The Song - Client

![GitHub last commit](https://img.shields.io/github/last-commit/Benjamin-Ramos/Guess-The-Song-Client?style=flat&logo=git&logoColor=white&color=0080ff)
![GitHub top language](https://img.shields.io/github/languages/top/Benjamin-Ramos/Guess-The-Song-Client?style=flat&color=0080ff)
![NuGet Version](https://img.shields.io/badge/nuget-v1.0.0-0080ff?logo=nuget&logoColor=white)

**Guess The Song Client** es una solución robusta diseñada para simplificar la creación de juegos musicales multijugador en tiempo real. Este cliente se enfoca en ofrecer una experiencia de usuario fluida, reactiva y altamente interactiva, optimizada para entornos Windows.

---

## ✨ Características Principales

* 🧩 **🔌 Integración con SignalR:** Facilita la comunicación multijugador en tiempo real entre los clientes y el servidor de forma bidireccional.
* 🎵 **🎧 Reproducción de Audio:** Sistema avanzado que gestiona la salida de sonido y el control de medios para una experiencia de juego inmersiva.
* 📊 **📝 Modelos de Datos Estructurados:** Definiciones claras de jugadores, rondas de juego e información de canciones para una gestión de estado eficiente.
* 🖥️ **🎨 Componentes de UI Dinámicos:** Implementación de interfaces responsivas con *data binding*, control de visibilidad y manejo de interacción de usuario.
* 🚀 **🔧 Arquitectura Modular:** Soporte para una personalización sencilla y despliegue ágil, diseñado específicamente para ecosistemas Windows.

---

## 🎮 Cómo Jugar

Sigue estos pasos para conectarte y empezar a competir:

1.  **Dirección del Servidor:** Al iniciar el cliente, ingresa la URL del servidor de SignalR. 
    * **Servidor de Prueba:** Puedes usar `http://guessthefsong.runasp.net/gamehub` para testear la conexión actualmente.
2.  **Identificación:** Introduce tu **Nombre de Usuario**. 
    * *Nota: El nombre debe ser único dentro de la sala; no se permiten duplicados en una misma sesión.*
3.  **Nombre de la Sala:** Ingresa el nombre de la sala a la que deseas unirte.
    * **Creación Automática:** Si el nombre de la sala no existe, el sistema la creará automáticamente para ti.
    * **Unirse:** Si la sala ya existe, entrarás directamente a la partida en curso.

---

## 🛠️ Stack Tecnológico

* **Lenguaje:** C# / .NET
* **Comunicación:** [Microsoft ASP.NET Core SignalR](https://learn.microsoft.com/en-us/aspnet/core/signalr/introduction)
* **Gestión de Paquetes:** NuGet
* **Suministro de Contenido:** El sistema utiliza la **API de iTunes** para obtener metadatos y fragmentos de canciones de alta calidad de forma dinámica.
---

## ⚖️ Aviso Legal y Créditos
Este proyecto utiliza la **iTunes Search API** para la obtención de metadatos y fragmentos de audio de 30 segundos. 
* El contenido musical y las carátulas son propiedad de sus respectivos autores y de Apple Inc.
* El uso de los fragmentos de audio se realiza con fines demostrativos y educativos bajo los términos de uso de la API de Apple.
---

## 🚀 Instalación y Configuración

### Requisitos Previos
* [.NET SDK](https://dotnet.microsoft.com/download) compatible con el proyecto.
* Acceso al servidor de SignalR configurado para el juego.

### Instalación vía NuGet
Puedes añadir el paquete a tu proyecto mediante la consola de administración de paquetes:

```bash
Install-Package Benjamin-Ramos.GuessTheSong.Client

