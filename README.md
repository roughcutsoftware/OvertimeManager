# OvertimeManager (PoC)  

## What is OvertimeManager  
- OvertimeManager is a proof-of-concept community project that attempts to tackle the challenge of overtime processing
- Using an article from Patriot Payroll System, the Overtime Rules Engine lays the foundation for processing any/most overtime laws for most States  
  - https://www.patriotsoftware.com/blog/payroll/overtime-laws-by-state/  
  - https://www.replicon.com/resource/new-york-minimum-wage/

## How to install/use/preview
- Clone the repo
- In Visual Studio, build the solution
  - This will take several minutes while npm re-installs packages for ReactJS
- The two (2) main projects are:
  - OvertimeManager.MVC5.Web (ensure is set as 'Startup Project')
  - OvertimeManager.MVC5.Web.Tests
- Once solution has built successfully
  - Run OvertimeManager.MVC5.Web
  - Click on the 'Companies' menu option
  - This will create the database via EF6 'Migrations'
  - Stop the web-app
  - Via SSMS, connect to: (localdb)\mssqllocaldb
  - Local OvertimeManager database
  - Navigate to OvertimeManager.MVC5.Web/_dbscripts 
  - Pull the *0200_seed-db-data.2021.06.14.sql* script into SSMS and run
    - This seeds the database 
  - From here you can re-run OvertimeManager.MVC5.Web
  - or
  - You can run the 'Tests' contained within -> OvertimeManager.MVC5.Web.Tests/Functional/OvertimeRulesTests.cs  
      - The three tests should pass
  
## Nuget Packages  
- Within any given project there is a packages.installed.txt text file
- Within the 'tests' project, you may need to re-install:
  - Shouldly
  - NUnit

## Future-State
- From here I'll probably begin refactoring the entire solution into a 'Clean' architecture design pattern, as well NetCore, EFCore and ReactJS (client)  
- To this point, there are three (3) placeholder projects:
  - OvertimeManager.Core
  - OvertimeManager.EFCore.Infrastructure
  - OvertimeManager.ReactJS.Web

#### *That's it for now - Happy Coding! - Roughcut Software*  