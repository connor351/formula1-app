# formula1-app
A basic app to manage F1 drivers and teams using SQL Server, .NET Web API Backend, and a Vue 3 frontend.

## Tech Stack
**Backend:** .NET 8 Web API, Entity Framework Core, SQL Server LocalDB  
**Frontend:** Vue 3 (TypeScript), Axios, Leaflet  
**Testing:** xUnit

## Setup
### Prerequisites
- Visual Studio 2022 or newer
- .NET 8
- Node.js v24
- SQL Server LocalDB (preinstalled with Visual Studio)

### Backend Setup:
1. Open `FormulaOne.Api.sln` in Visual Studio
2. Open the Visual Studio Developer Powershell/Command Line
3. Navigate to the API project folder
4. Restore dependencies by running `dotnet restore`
5. Apply EF Core migrations by running `dotnet-ef database update`
    - If you encounter errors, try installing EF Core tools by running `dotnet tool install --global dotnet-ef`
6. The API should now be ready to run on localhost

### Frontend Setup:
1. Navigate to the FormulaOne.App folder using a terminal window
2. Install dependencies by running `npm install`
3. Make sure the URL and port in src/services/api.ts match the URL and port that the API is running on
4. Launch the server by running `npm run dev`

### Database
The database is pre-seeded with 10 teams, 20 drivers, and 24 circuits
You can freely add or remove teams/drivers using the Vue UI

## Features
- Driver and team management with full CRUD operations
- Server-side search and sorting
- Interactive map view of F1 circuits worldwide using Leaflet
- Business logic enforcement
    - Unique driver numbers required
    - Teams cannot be deleted if they contain drivers
    - Unable to have negative points
    - Driver numbers cannot be greater than 99
- RESTful APIs and responses
- Unit tested controllers (6 per controller due to time constraints)
- Request/Response DTOs
- Database design managed by EF Core using a code first approach

## Design Decisions and Trade Offs
### Search Functionality
Due to my strengths in backend development and familiarity with .NET Web APIs, I chose to handle searching server side rather than client side. This also reduces memory consumption on the client as only the data that is needed is handed over, meaning greater scalability and performance as the dataset grows.
There is a single search bar that filters over key fields such as name and nationality instead of a per column search, I think this offers a better user experience as searching is fast and easy.

### No Service Layer
As this is a short task, I chose to query the database directly from my controllers, this isn't something I would typically do, but it felt appropriate for the task at hand. Usually I would extract this into a service layer to separate concerns out into other classes so controllers don't handle multiple tasks.

### Leaflet Maps
Leaflet is a free and open source, requiring no API keys or account creation like MapBox or Google Maps API. It was also recommended by Claude (more in the AI section)

### Testing
Due to the scope of the assignment, 6 critical unit tests were created for the two main controllers, usually I would like a comprehensive set of unit tests that cover edge cases and general use of the controller and their respective methods.

Swagger was used to test API endpoints and ensure correct functionality on top of unit tests.

There are no unit tests included on the Vue end, since most logic is in the backend and Vue is served data by the API. UI tests were conducted manually to ensure you were able to add/edit/delete/search drivers and teams, and to check the map was interactive.

## Use of AI
### Tools
I chose to use Claude over ChatGPT or GitHub CoPilot to match the spec and team, as well as to compare the effectiveness against models I have previously used.

### Uses
The API side was mostly done independently as this is a technical test, so didn't want to generate everything purely with AI, and I am comfortable with .NET APIs. I found that Claude was useful in generating DTOs based on the entites that I gave it, and generating unit tests was a big time saver. Furthermore, the seeding of the SQL data would've taken me a long time to write myself, whereas Claude was able to generate it within seconds based on the entites that I presented it.

Having had no experience with Vue.js unti now, Claude was a key tool in getting the solution to a working standard. It walked me through the generation of the vue app, installing dependencies, and creating views/services. I ensured that each step was broken down so that I understood why certain recommendations were made. I was guiding Claude based on the brief, and making adjustments or recommendations based on the result.

When creating the first front end page, I encountered a cross origin resource sharing (CORS) error. This is something I haven't seen before due to all my current work being uploaded to the company intranet, so all requests come from the same domain. Claude was key in resolving this issue swiftly. I added a CORS policy exception in Program.cs, which allowed the app to communicate with the API.

Claude reviewed my README.md file to check it over for spelling and grammatical errors, as well as suggesting improvements for clarity and flow. I decided to create it and type it out myself for this task due to it being a technical test. However, I would usually use an AI tool to fully generate README files and other documentation, and review it myself afterwards.

### Reviews and Adjustments
I found that when generating entities/DTOs, Claude forgot to add any data annotations, so they were added manually and added as an additional migration.
Suggestions were made on the frontend for a better user experience that Claude has missed, these include:
- Title and nav bar creation
- Formula 1 branding/theme
- Success message after creating/editing/deleting a team/driver
- Including extra information about circuits when clicking a marker

