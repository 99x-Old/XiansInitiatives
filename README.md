
# Xian's Initiaives
Xians are innovative and most of the time they are spending a quite considerable amount of time contributing to the company initiatives. This is highly valued at 99X culture. At the moment all the information about initiatives is tracked in an ad-hoc manner. Xian's Initiatives is a system to keep track of initiatives and member activities.

### Prerequisites ###

* Angular CLI: 9.0.6
* Node: 12.16.1
* Dotnet SDK 3.1.301
* MS SQL server
* Azure cloud services

### Architecture Diagram
![Alt text](/XiansInitiatives.Assets/Architecture diagram.png?raw=true "Architecture_diagram")

### Database Diagram
![Alt text](/XiansInitiatives.Assets/Database Diagram.png?raw=true "Database_diagram")

### Azure services used ###

 - Azure devops - CI/CD  pipline configurations
 - Azure App service - Front-end
 - Virtual Machines - Back-end
 - Service Bus - Queue for notifications
 - Functions App - Send emails and cosmos DB activity log
 - SendGrid - Email service provider
 - SQL databases - Database
 - Storage account (Blob storage) - Store images
 - Cosmos DB - Activity log
 
### Other cloud services used ### 
 - CKEditor Ecosystem (Collaborative documentations)

### Test frameworks ###

* Jasmine & Karma
* Xunit
* Cypress (e2e testing)

### How to set up client app (Front-End) ###

* Run npm install in clientApp directory.
* Then run ng serve.

### How to set up WebAPI (Back-End) ###

* Run dotnet ef database update inside the WebAPI/WebAPI directory.(If dotnet ef tools not installed use this command: dotnet tool install --global dotnet-ef).
* If you have installed multiple dotnet SDKs in your machine, run this command first to create a global.json : dotnet new globaljson
* Then run dotnet run command.

### How to run tests ###

Front-End

* Run ng test inside the /ClientApp directory.

Back_End

 * Run dotnet test inside the  WebAPI/WebAPI.Test directory
E2E
 * Run npx cypress run in /ClientApp directory.

