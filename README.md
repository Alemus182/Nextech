# Demo v 1.0

Baseline for construction of an API based on .net 7, under the proposed clean architecture approaches and the use of SOLID development principles.
As well as a single page application base in angular like a front end.

# Technology Stack
## Backend
    - .net 7
    - Minimal Apis
    - FluentValidation
    - Mediator
    - NLog
    - JWT Bearer token `autentication`
## Frontend
    - Angular 10
    - Angular Material
    - Boostrap
    - RxJs
# Assumptions
- The application uses InMemoryCache and it is refreshed every hour
- Authentication is based on username and password and it generates a JWT.
  The only validation that is carried out is that it is a valid email and
  that the password has more than 8 characters.
- The proposal is a basic query, so we can only filter by id and category. For more advanced searches, data consolidation work is required.
- The swagger UI in the api is open for testing propouses.
# Execution
## Backend
   - Install and open Visual Studio Code or Visual Studio
   - Download the .net 7 SDK available in [Web Site](https://dotnet.microsoft.com/es-es/download/dotnet/7.0)
   - Open the terminal and navigate to the folder "Api" and run:
```properties
dotnet run
```
   - It Will be open in: https://localhost:7245
   - For execute the test suite,In the terminal go to the folder "Test" and run:
```properties
dotnet test
``` 
 ## Frontend
 -   Install Visual Studio Code
 -   The Application is built in Angular 10, if you have a newer version installed of node.js 12.11.1, you need to use NVM (Node Version Manager). for Windows, you can download it in: [Website](https://github.com/coreybutler/nvm-windows#installation--upgrades) or download it through a package manager like chocolatey for Windows or Homebrew for mac.
 - After installation of NVM, open the terminal and install the corresponding node version
      ```properties
       nvm install 12.11.1
       nvm use 12.11.1
    ```
 - Navigate to the folder 'Web', and run:
	```properties
	  npm run ng serve
	``` 
      It Will be open in: http://localhost:4200/ 
# Demo
An implementation was carried out which is available at:
- [Web Site](https://wonderful-wave-0f1681e1e.4.azurestaticapps.net/)
- [Web Api](https://nextech-demo-api.azurewebsites.net/swagger)
