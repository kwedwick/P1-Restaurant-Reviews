# P1 ASP.NET Core + Identity C# Restaurant Reviewer
## Description
This is an MVC Restaurant Review application that utilizing Microsoft Identity deployed through Azure Web Services. Users are able to view all restaurants, CRUD their reviews, and search restaurants by name. 
    
## Table of Contents 
* [Installation](#Installation) 
* [License](#License)
* [Tech Stack](#Tech-Stack)
* [Er Diagram](#Er-Diagram)
* [Contribution](#Contribution) 
* [Test](#Test) 
    
## Installation
You need at least .NET5 to run this application. You'll need to download and use dotnet restore to download the required packages. In addition, you'll need a User Secret's file in the UI folder that contains your AZURE database information. If you want to take advantage of Email confirmations, you'll also need to setup a GMAIL SMTP secret as well.
    
![GitHub license](https://img.shields.io/badge/license-mit-blue.svg)
## License
    
This project falls under the mit license. Please visit [mit](https://choosealicense.com/licenses/mit) to learn more.

## Tech Stack

- C#
- ASP.NET MVC
- Xunit
- SQLServer DB
- EF Core 
- Serilog
- Microsoft Identity
- Azure Web Services
- Azure Dev Ops
- SonarCloud

## ER Diagram
![P0 Tables](./assets/P0%20ER%20Diagram.png)
## Contribution
I'd like to thank my groupmates Du Tran, Andrew Carson, and Mariah McMurren for their encouragement!

## Test
This application uses Unix Testing. Simply navigate to the Tests folder and use 'dotnet test'.
    
## Questions
The author of this project is Keegan Wedwick. You can reach them via [email](mailto:kwedwick@gmail.com).
To see more projects from this author, visit their [GitHub](https://github.com/kwedwick).