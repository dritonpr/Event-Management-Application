# Event Management Application

## Overview

The Event Management Application is a web application developed using ASP.NET Core and Blazor Server. It allows users to register, log in, and manage events efficiently. Users can create, edit, delete, and respond to events while receiving real-time feedback.

## Features

- **User Authentication:**
  - User registration and login functionality.
  - Cookie-based authentication for session management.

- **Event Management:**
  - View a list of events with details such as name, date, location, and response status.
  - Add new events with necessary details.
  - Edit existing events created by the user.
  - Delete events that the user created.
  - Respond to events and receive feedback on the response status.

- **Authorization:**
  - Users can only edit or delete events they created.

- **UI Components:**
  - Responsive design using Blazor Components for better user experience.
  - Form validation for user input.

## Technologies Used

- ASP.NET Core
- Blazor Server
- Entity Framework Core with SQLite
- HTML/CSS
- Bootstrap for styling

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or higher)
- SQLite database
- An IDE such as [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Installation

1. Clone the repository:
    
   git clone [<repository-url>](https://github.com/dritonpr/Event-Management-Application)
   cd EventManagement
