# MiniORMSystem
This is a console app project to practice and create something like ORM. I have used reflection, recursion and ADO.NET to create this dynamic mini orm system. I have tried to maintain some level of OOD concept. It can manage nested objects as well. 

## Demo
<img width="714" alt="demoORM" src="https://user-images.githubusercontent.com/61489549/202839301-8ba6e516-5885-420d-8962-371c158cc6de.PNG">

## Installation
### (Step-1) Clone the project

```bash
https://github.com/fa93/MiniORMSystem.git
```
### (Step-2) Create your own database manually 
<img width="132" alt="tablesORM" src="https://user-images.githubusercontent.com/61489549/202840188-79b88b65-f46b-4eaf-8964-5cfce967d10f.PNG">

### (Step-3) Add a file named DefaultConnection to keep your own connection string 

```bash
public readonly string ConnectionString;
        public DefaultConnection()
        {
            ConnectionString = "";
        }
```
### (Step-4) Add and Update Migrations by running the following commands on ``` Package Manager Console ```

```bash
dotnet ef migrations add AnyNameTable --project ProjectName --context DbContextName -o Data/Migrations
dotnet ef database update --project ProjectName --context DbContextName
```

### (Step-5) keep the program.cs file like the below and Run the project
<img width="270" alt="runORM" src="https://user-images.githubusercontent.com/61489549/202840400-a538a65d-7211-4337-b290-9858cdd04a02.PNG">

## Pre-requisites
Must be installed on your machine :
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- [MSSQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)


