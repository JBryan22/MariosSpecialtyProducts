## Mario's Specialty Food Products

### Michael Woldemedihin

#### Web app for Mario's Specialty Food Products, 10.20.17

## By Michael Woldemedihin

Inspired by [beerconnoisseur.com](https://beerconnoisseur.com/), 10.20.17_

## Setup/Installation Requirements

* _Download and install [.NET Core 1.1 SDK](https://www.microsoft.com/net/download/core)_
* _Download and install [Mono](http://www.mono-project.com/download/)_
* _Download and install [MAMP](https://www.mamp.info/en/)_
* _Set MySQL Port to 8889_


* _Clone repository_
* _run `dotnet restore`_
* To create the database, go to the migrations folder and get the name of the last migration
* run `dotnet ef database update theLastMigrationName` (make sure you are in  the project directory not in the solution directory)
* If you encounter an error while building check the `.csproj` file for the following packages, and install if there is anything missing then run `dotnet restore` followed by the above command.
```
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore" Version="1.1.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="1.1.2" />
    <DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="1.1.2" PrivateAssets="All" />
    <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="1.0.0" />
    <PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="1.1.1" />
    <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="1.1.2" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="1.1.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="1.1.2" />
    <PackageReference Include="Microsoft.AspNetCore.StaticFiles" Version="1.1.2" />
  </ItemGroup>

```
* _run the program in your favorite browser
* _if you get a nullReferenceException thrown from the view, go to your database and prepopulte the database with dummy data and run the website again.

## Nuget Package Manager
* If you want the command line nuget.exe in order to create NuGet packages, that is still a separate download [Click here](https://www.nuget.org/downloads)
## Markdown
* To use markdown editor in visual studio dowload the extension from [here.](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor)
## Technologies Used
* _C#_
* _.NET Core_
* _MVC_
* _Entity Framework_
* _[Bootstrap 4](http://getbootstrap.com/getting-started/)_
* _[MySQL](https://www.mysql.com/)_

### License

Copyright (c) 2017 **Michael Woldemedihin**

*Licensed under the [MIT License](https://opensource.org/licenses/MIT)*


## Setup/Installation Requirements

* _Download and install [.NET Core 1.1 SDK](https://www.microsoft.com/net/download/core)_
* _Download and install [Mono](http://www.mono-project.com/download/)_
* _Download and install [MAMP](https://www.mamp.info/en/)_
* _Download and install [Visual Studio 2017](https://www.visualstudio.com/)_
* _Clone repository_

### Setup/Installation for Database
* In your terminal, navigate from the Solution folder to the project folder
* Enter `dotnet restore` in your terminal
* Enter `dotnet ef database udpate` in your terminal

##### Import data from the Cloned Repository
* _Click 'Open start page' in MAMP_
* _Under 'Tools', select 'phpMyAdmin'_
* _Click 'Import' tab_
* _Choose database file (from cloned repository folder)_
* _Click 'Go'_

## Technologies Used
* _C#_
* _.NET Co_
* _MVC_
* _Entity Framework_
* _[Bootstrap](http://getbootstrap.com/getting-started/)_
* _[MySQL](https://www.mysql.com/)_

### License

Copyright (c) 2017 **_Michael Woldemedihin_**

*Licensed under the [MIT License](https://opensource.org/licenses/MIT)*

