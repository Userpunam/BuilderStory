        #Word Story Builder API
The Word Story Builder API is a .NET Core Web API project that builds stories using a single word input. It uses AI to generate stories and supports storing, retrieving, counting words, and uploading images to stories.

 #Features Of StoryBuildetAPI Project
 
-Create Story from Word using AI

-Paginated Stories List

-Count Word Occurrence in a Story

-Upload Image for a Story

-AI Integration using OpenRouter (ChatGPT-like)

-PostgreSQL Database with Entity Framework Core

-Serilog for logging

-Swagger UI for API testing

#Technologies Used In Project 
-ASP.NET Core Web API (.NET 7/8)

-Entity Framework Core

-PostgreSQL

-AutoMapper

-Serilog

-Swagger (Swashbuckle)

-OpenRouter GPT API

-C#

 #API Endpoints
Method	Endpoint	Description For project-

POST	/api/story/create	  -Create a story from a word

GET	/api/story/paginated	-Get paginated list of stories

POST	/api/story/count	  -Count word occurrences in a story

POST	/api/story/upload   -image	Upload an image to a story

