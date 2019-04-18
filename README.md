# User-Management
The project loads list of users on project load from external source and stores them in the DB in "Users" collection.
Source: https://jsonplaceholder.typicode.com/users 
You need to provide the ability to Add new user to the same collection, update and delete by Id.  
You need to provide the ability to search Users by the following filters on the same function (POST Method). 
- Search By name
- Search By username
- Search By address.zipcode
- search By company.name
You can have multiple search filters at the same time. 
Search response returns list of users matching the filter criteria.
All API Methods should be authenticated using username and password in the request header.   
Expose the Web API using swagger.
