# Use the official .NET SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy everything and publish the app
COPY . ./
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /app/out ./

# Copy Angular static files to expected location inside container
# This ensures the same path works in your Program.cs
COPY ./client/angular-app1/dist/angular-app1/browser ./client/angular-app1/dist/angular-app1/browser

# Expose port
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "YourProjectName.dll"]
