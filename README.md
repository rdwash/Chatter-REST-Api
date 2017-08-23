# Chatter-REST-Api
### RESTful API for Chat using WebApi

API Project: **ChatterApi.API**

Identity Server Project: **ChatterApi.IdSvr**

Identity Server Users (un/pw): _Rob/secret_ OR _David/secret_ 

Domain (repository) Project: **ChatterApi.Domain**

Data Transfer Project: **ChatterApi.DTO**

Constants Project: **ChatterApi.Constants**

The API uses a separate Identity Server for authentication, which is why each need to be deployed. The Identity Server needs SSL as well.

There are constant variables used in ChatterApi.Constants project to denote server locations for deployment. You may need to update these locations (ChatterApi and IdSrv) onece both the API project and the Identity Server project are deployed to the a server. 

There is a scripts file (in the root of the project) for creating the database used by the application. You will have to update the connection string of the API project to ensure a proper connection once the database is deployed.

