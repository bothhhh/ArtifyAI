# 🎨 ArtifyAI

**ArtifyAI** is a responsive web application developed using ASP.NET Core MVC. It explores the role of artificial intelligence in creative arts, including AI-generated images, music composition, and creative writing.

The project was developed as part of the Web Design and Programming unit at the University of Canberra.

## ✨ Features

- **Responsive Design** — Supports desktop, tablet, and mobile devices.
- **AI Image Gallery** — Browse AI-generated artwork uploaded by users.
- **User Authentication** — Register and log in using ASP.NET Core Identity.
- **Image Uploads** — Authenticated users can upload AI artwork.
- **Like System** — Users can like images once per login session, with asynchronous updates that do not reload the page.
- **Admin Management** — Administrators can edit image information, replace images, and delete submissions.
- **Dynamic Home Gallery** — Displays uploaded artwork ordered by popularity.
- **AI Tools Explorer** — Introduces AI tools for visual art, music, and writing.
- **Interactive Interface** — Includes scroll animations, hover effects, and category filtering.

## 🛠️ Technologies Used

| Category | Technologies |
|---|---|
| Backend | C#, ASP.NET Core MVC |
| Frontend | HTML5, CSS3, JavaScript |
| Styling | Bootstrap 5, Flexbox, CSS Grid |
| Database | SQLite |
| ORM | Entity Framework Core |
| Authentication | ASP.NET Core Identity |
| Development | Visual Studio Code, .NET SDK |
| Version Control | Git and GitHub |

## 📸 Screenshots

Screenshots of the application will be added here.

Suggested screenshots:
- Home page
- AI Image Gallery
- AI image details
- AI Tools page
- Mobile responsive layout

## 🚀 Getting Started

### Prerequisites

- .NET SDK compatible with the project
- Visual Studio Code or Visual Studio

### Installation

**1. Clone the repository**

```bash
git clone https://github.com/bothhhh/ArtifyAI.git
cd ArtifyAI
```

**2. Restore dependencies**

```bash
dotnet restore
```

**3. Apply database migrations**

```bash
dotnet ef database update
```

**4. Run the application**

```bash
dotnet run
```

Open the local URL displayed in your terminal.

> Some features require registration and login. Administrator access requires an account assigned to the Admin role.

## 📁 Project Structure

```text
ArtifyAI/
├── Controllers/
├── Data/
├── Models/
├── Views/
│   ├── Home/
│   ├── AIImages/
│   ├── AITools/
│   └── Shared/
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── images/
├── Program.cs
└── ArtifyAI.csproj
```

## 🎓 Project Background

ArtifyAI was created for Web Design and Programming (7175) at the University of Canberra.

The project demonstrates responsive web development, database integration, authentication, role-based authorisation, and interactive user interfaces using ASP.NET Core MVC.

## 📄 Credits

The website references third-party AI platforms and technologies for educational purposes. Media credits are provided on the Contact page.

## 👨‍💻 Developer

Developed by [bothhhh](https://github.com/bothhhh).
