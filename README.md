# TeleHealth
==============

This is a simple dashboard to add and display patient details. 
The solution consists of API Controller along with a simple UI.

Running Instruction:
====================

Download the project from git repo and open in VS 2019.

Build and Run. (UI build is also handled through the .Net build process) 

Local URL : https://localhost:44300/

Swagger API Documentation : https://localhost:44300/swagger/index.html


Technologies Used : 
===================

- C# .Net Core 2.2 / React JS   
- SQLite DB for database
- SeriLog for logging (log files are written into Log folder inside the solution)
- XUnit / NSubstitute for Unit Testing
- SwashBuckle for Swagger API documentation
- VS 2019


Things Achieved :
=================

- API controller implemented with server side validation / simple authentication / unit testing with Dependency Injection
- DB integration
- Simple UI with certain front end validation and working functionality with console logging
- Logging and Error Handling 


Things to improve :
=====================

- UI look and feel can be improved
- Front end unit tests to be written
- Front end validation for all input fields
- Front end  logging to be pushed to some persistent location (may be elmah)
- Server side unit tests for data access layer
- A sophisticated Authentication technology can be implented (ex: OAuth)
- Create build pipeline and deployment scripts
- Deploy the project to Azure Web Apps + SQL
- API keys for authentication can be retrieved at deployment from a secure place (ex: Azure Key Vault)
- Those secrets can be injected to settings at application start up



- Designed and Developed by Nirasha Gunasekera (March 2020)
