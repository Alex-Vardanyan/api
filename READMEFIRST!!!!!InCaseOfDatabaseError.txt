****In case of SOME database error(running the api for the first time)**** 
in Supermarket folder
>dotnet ef database drop -p .\Supermarket.Dal\ -s .\Supermarket.Api\
Then input y and press enter(to grant the permission)
>dotnet ef migrations remove -p .\Supermarket.Dal\ -s .\Supermarket.Api\
>dotnet ef migrations add InitailCreate -p .\Supermarket.Dal\ -s .\Supermarket.Api\ -o EfStructures/Migrations

Useful commands
>dotnet run
>dotnet watch run

https://localhost:5001/swagger    ****To access documentation