# 📚 Course Management System

A comprehensive desktop application built with WPF (.NET) for managing student courses, grades, and administrative tasks in educational institutions.

[![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![WPF](https://img.shields.io/badge/WPF-0078D4?style=for-the-badge&logo=windows&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
[![Entity Framework](https://img.shields.io/badge/Entity_Framework-512BD4?style=for-the-badge&logo=nuget&logoColor=white)](https://docs.microsoft.com/en-us/ef/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server)
[![XAML](https://img.shields.io/badge/XAML-0C54C2?style=for-the-badge&logo=xaml&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/xaml/)

## 📋 Table of Contents

- [Features](#-features)
- [Screenshots](#-screenshots)
- [Technologies Used](#-technologies-used)
- [System Requirements](#-system-requirements)
- [Installation](#-installation)
- [Database Setup](#-database-setup)
- [Configuration](#-configuration)
- [Usage](#-usage)
- [Project Structure](#-project-structure)
- [Contributing](#-contributing)
- [License](#-license)
- [Contact](#-contact)

## ✨ Features

### 🎓 Student Portal
- **Personal Dashboard**: View enrolled courses and grades
- **Course Information**: Access detailed course information
- **Grade Tracking**: Monitor academic progress in real-time
- **User-Friendly Interface**: Clean and intuitive design

### 👨‍💼 Administration Panel
- **Student Management**: View and manage student enrollments
- **Course Management**: Organize and track course offerings
- **Grade Assignment**: Update and manage student grades
- **Advanced Filtering**: Filter by student or course for quick access
- **Bulk Operations**: Load and manage multiple records efficiently

### 🔐 Security
- **Role-Based Access Control**: Separate interfaces for students and administrators
- **Secure Authentication**: User login with password protection
- **Data Validation**: Input validation to ensure data integrity

## 📸 Screenshots

> *Add screenshots of your application here*

```
[Login Screen] [Admin Dashboard] [Student Dashboard]
```

## 🛠 Technologies Used

| Technology | Description |
|------------|-------------|
| **C# 10.0** | Primary programming language |
| **WPF (Windows Presentation Foundation)** | UI framework for desktop applications |
| **XAML** | Markup language for UI design |
| **Entity Framework Core** | ORM for database operations |
| **SQL Server** | Relational database management system |
| **LINQ** | Query syntax for data manipulation |
| **.NET 6.0+** | Application framework |

## 💻 System Requirements

- **Operating System**: Windows 10/11 (64-bit)
- **Framework**: .NET 6.0 SDK or higher
- **Database**: SQL Server 2017 or higher / SQL Server Express
- **RAM**: Minimum 4GB (8GB recommended)
- **Storage**: 500MB free disk space
- **IDE**: Visual Studio 2022 (recommended) or Visual Studio Code

## 📥 Installation

### Prerequisites

1. **Install .NET SDK**
   ```bash
   # Download from: https://dotnet.microsoft.com/download
   # Verify installation
   dotnet --version
   ```

2. **Install SQL Server**
   ```bash
   # Download SQL Server Express (free):
   # https://www.microsoft.com/en-us/sql-server/sql-server-downloads
   ```

3. **Install Visual Studio 2022** (Optional but recommended)
   - Download from: https://visualstudio.microsoft.com/
   - Select ".NET desktop development" workload during installation

### Clone the Repository

```bash
# Clone the repository
git clone https://github.com/yourusername/CoursesManagementSYS.git

# Navigate to project directory
cd CoursesManagementSYS
```

### Restore Dependencies

```bash
# Restore NuGet packages
dotnet restore
```

## 🗄 Database Setup

### 1. Create Database

Run the following SQL script in SQL Server Management Studio (SSMS):

```sql
-- Create Database
CREATE DATABASE CourseManagementDB;
GO

USE CourseManagementDB;
GO

-- Create Users Table
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(50) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Student','Admin'))
);

-- Create Courses Table
CREATE TABLE Courses (
    CourseId INT IDENTITY(1,1) PRIMARY KEY,
    CourseName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(250)
);

-- Create StudentCourses Table
CREATE TABLE StudentCourses (
    StudentCourseId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    Grade DECIMAL(5,2) NULL,
    CONSTRAINT FK_Student FOREIGN KEY(StudentId) REFERENCES Users(UserId),
    CONSTRAINT FK_Course FOREIGN KEY(CourseId) REFERENCES Courses(CourseId)
);

-- Insert Sample Admin
INSERT INTO Users (UserName, Password, Role) 
VALUES ('admin', 'admin123', 'Admin');

-- Insert Sample Students
INSERT INTO Users (UserName, Password, Role) 
VALUES 
    ('john_doe', 'student123', 'Student'),
    ('jane_smith', 'student123', 'Student');

-- Insert Sample Courses
INSERT INTO Courses (CourseName, Description) 
VALUES 
    ('Mathematics', 'Advanced Mathematics Course'),
    ('Physics', 'Introduction to Physics'),
    ('Computer Science', 'Programming Fundamentals');

-- Insert Sample Enrollments
INSERT INTO StudentCourses (StudentId, CourseId, Grade) 
VALUES 
    (2, 1, 85.50),
    (2, 2, 90.00),
    (3, 1, 78.75);
```

### 2. Update Connection String

Open `CourseManagementDB.cs` and update the connection string with your SQL Server instance:

```csharp
string con = "Data Source=YOUR_SERVER_NAME;Initial Catalog=CourseManagementDB;Integrated Security=True;Trust Server Certificate=True";
```

Replace `YOUR_SERVER_NAME` with:
- `localhost` or `(local)` for local SQL Server
- `.\SQLEXPRESS` for SQL Server Express
- Your specific server name

## ⚙ Configuration

### Database Connection Options

**Integrated Security (Windows Authentication)**:
```csharp
"Data Source=localhost;Initial Catalog=CourseManagementDB;Integrated Security=True;Trust Server Certificate=True"
```

**SQL Server Authentication**:
```csharp
"Data Source=localhost;Initial Catalog=CourseManagementDB;User Id=your_username;Password=your_password;Trust Server Certificate=True"
```

## 🚀 Usage

### Running the Application

#### Using Visual Studio
1. Open `CoursesManagementSYS.sln` in Visual Studio
2. Press `F5` or click "Start" to run the application

#### Using Command Line
```bash
# Navigate to project directory
cd CoursesManagementSYS

# Build the project
dotnet build

# Run the application
dotnet run
```

### Default Login Credentials

**Administrator Account**:
- Username: `admin`
- Password: `admin123`

**Student Account**:
- Username: `john_doe`
- Password: `student123`

> ⚠️ **Security Note**: Change default passwords in production!

### Application Workflow

1. **Login**: Enter credentials on the login screen
2. **Admin Dashboard**: 
   - Select student/course from dropdowns
   - View enrollment records
   - Update grades
   - Filter and manage data
3. **Student Dashboard**:
   - View enrolled courses
   - Check current grades
   - Access course information

## 📁 Project Structure

```
CoursesManagementSYS/
│
├── Model/
│   ├── CourseManagementDB.cs      # DbContext and database configuration
│   ├── Users.cs                    # User entity model
│   ├── Courses.cs                  # Course entity model
│   └── StudentCourses.cs           # Student-Course relationship model
│
├── Views/
│   ├── Login.xaml                  # Login window XAML
│   ├── Login.xaml.cs               # Login window code-behind
│   ├── Administration.xaml         # Admin panel XAML
│   ├── Administration.xaml.cs      # Admin panel code-behind
│   ├── StudentInformation.xaml     # Student view XAML
│   └── StudentInformation.xaml.cs  # Student view code-behind
│
├── App.xaml                        # Application resources
├── App.xaml.cs                     # Application startup
└── CoursesManagementSYS.csproj     # Project file
```

## 🔧 Building from Source

### Debug Build
```bash
dotnet build --configuration Debug
```

### Release Build
```bash
dotnet build --configuration Release
```

### Publish Application
```bash
# Create self-contained executable
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. **Fork the repository**
2. **Create a feature branch**
   ```bash
   git checkout -b feature/AmazingFeature
   ```
3. **Commit your changes**
   ```bash
   git commit -m 'Add some AmazingFeature'
   ```
4. **Push to the branch**
   ```bash
   git push origin feature/AmazingFeature
   ```
5. **Open a Pull Request**

### Code Style Guidelines
- Follow C# coding conventions
- Use meaningful variable and method names
- Comment complex logic
- Write unit tests for new features

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🐛 Known Issues

- [ ] Grade validation needs enhancement
- [ ] Add password encryption
- [ ] Implement password recovery
- [ ] Add export functionality for reports

## 🔮 Future Enhancements

- [ ] Email notifications for grade updates
- [ ] Advanced reporting and analytics
- [ ] Multi-language support
- [ ] Mobile application version
- [ ] Cloud database integration
- [ ] Password hashing and encryption
- [ ] Attendance tracking module
- [ ] Course scheduling system

## 📞 Contact

**Project Maintainer**: 

- GitHub: [@Eng-ahmed-dev1](https://github.com/Eng-ahmed-dev1)
## Acknowledgments

- Microsoft for .NET and WPF framework
- Entity Framework Core team
- All contributors and supporters

---

<div align="center">

**⭐ Star this repository if you find it helpful!**

Made with ❤️ using WPF and .NET

</div>
