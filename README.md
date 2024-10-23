# Real-Time Collaborative Text Editor (Differential Synchronization)

## Overview

This is a **Real-Time Collaborative Text Editor** built using **Differential Synchronization**, enabling multiple users to edit the same document simultaneously while maintaining consistency across all clients. The project utilizes a **.NET backend** for handling business logic, **React** for the frontend interface, and **SQLite** as a temporary database for storing document states during sessions.

## Features

- **Real-Time Collaboration**: Multiple users can edit the same document concurrently, with synchronized changes reflected instantly for all participants.
- **Differential Synchronization**: Leveraging a diff-based synchronization algorithm to resolve conflicts and merge changes across users.
- **Frontend in React**: A user-friendly and dynamic code editor built with React.
- **Backend in .NET**: The backend is powered by .NET, handling synchronization logic and communications.
- **Temporary Storage**: Currently uses **SQLite** as a temporary database for session-based document storage (migration to a persistent client-server database is pending).

## Technologies Used

- **Frontend**: React.js
- **Backend**: .NET (ASP.NET Core)
- **Database**: SQLite (temporary)
- **Synchronization Algorithm**: Differential Synchronization
- **Other Tools**: WebSockets for real-time communication
