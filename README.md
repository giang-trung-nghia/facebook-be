# facebook-clone

## migrations
1. Add

`dotnet ef migrations add user_refresh_token_table --project D:\Project\facebook\test\facebook-be\Facebook.Infrastructure\Facebook.Infrastructure.csproj --startup-project D:\Project\facebook\test\facebook-be\Facebook.API\Facebook.API.csproj`

2. update database
   
`dotnet ef database update --project D:\Project\facebook\test\facebook-be\Facebook.Infrastructure\Facebook.Infrastructure.csproj --startup-project D:\Project\facebook\test\facebook-be\Facebook.API\Facebook.API.csproj`
