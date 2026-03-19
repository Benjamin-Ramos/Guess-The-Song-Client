# 🎵 Guess The Song - Client

**Guess The Song Client** es una solución robusta diseñada para simplificar la creación de juegos musicales multijugador en tiempo real. Este cliente se enfoca en ofrecer una experiencia de usuario fluida, reactiva y altamente interactiva, optimizada para entornos Windows.

---

## ✨ Características Principales

- 🧩 🔌 **Integración con SignalR**  
  Facilita la comunicación multijugador en tiempo real entre los clientes y el servidor de forma bidireccional.

- 🎵 🎧 **Reproducción de Audio**  
  Sistema avanzado que gestiona la salida de sonido y el control de medios para una experiencia de juego inmersiva.

- 📊 📝 **Modelos de Datos Estructurados**  
  Definiciones claras de jugadores, rondas de juego e información de canciones para una gestión de estado eficiente.

- 🖥️ 🎨 **Componentes de UI Dinámicos**  
  Implementación de interfaces responsivas con data binding, control de visibilidad y manejo de interacción de usuario.

- 🚀 🔧 **Arquitectura Modular**  
  Soporte para una personalización sencilla y despliegue ágil, diseñado específicamente para ecosistemas Windows.

---

## 🎮 Cómo Jugar

Sigue estos pasos para conectarte y empezar a competir:

1. **Dirección del Servidor**  
   Al iniciar el cliente, ingresa la URL del servidor de SignalR.

   - 🔗 Servidor de prueba:  
     `http://guessthefsong.runasp.net/gamehub`

2. **Identificación**  
   Introduce tu nombre de usuario.  
   > ⚠️ El nombre debe ser único dentro de la sala.

3. **Nombre de la Sala**  
   Ingresa el nombre de la sala a la que deseas unirte.

   - 🆕 Si no existe, se creará automáticamente.
   - 🔄 Si ya existe, entrarás directamente a la partida.

---

## 🛠️ Stack Tecnológico

- **Lenguaje:** C# / .NET 8  
- **Comunicación:** Microsoft ASP.NET Core SignalR  
- **Gestión de paquetes:** NuGet  
- **Contenido musical:** API de iTunes  

---

## ⚖️ Aviso Legal y Créditos

Este proyecto utiliza la **iTunes Search API** para la obtención de metadatos y fragmentos de audio de 30 segundos.

- El contenido musical y las carátulas son propiedad de sus respectivos autores y de Apple Inc.
- El uso de los fragmentos de audio se realiza con fines demostrativos y educativos bajo los términos de uso de la API de Apple.

---

## 🚀 Instalación y Configuración

### 🔧 Requisitos Previos

- .NET SDK 8.0 o superior  
- Acceso a un servidor SignalR configurado  

### 📦 Instalación vía NuGet

#### Consola de paquetes:
```bash
Install-Package Benjamin-Ramos.GuessTheSong.Client
```
#### O usando la CLI de .NET:
```bash
dotnet add package Benjamin-Ramos.GuessTheSong.Client
```
## 📄 Licencia
Este proyecto es de código abierto bajo la licencia **MIT**.

## 👨‍💻 Autor
Desarrollado por **[Benjamin Ramos](https://github.com/Benjamin-Ramos)**
