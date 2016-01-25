# ModernWebSampleApp
A simple Web application displaying and allowing the end user to manage departments and employees of an organization. 

The application code leverages a complete series of modern technologies:
 - an SQL Server database with two linked tables to store data;
 - Entity Framework as ORM to .NET;
 - ASP .NET WebAPI using custom DTOs to export and import data through REST;
 - AngularJS for the user interface of the single page HTML5 based app:
    - ngRoute for client side URL routing and displaying views accordingly;
    - $http to read and update data through the WebAPI host;
 - Bootstrap to style the user interface at minimal level.
