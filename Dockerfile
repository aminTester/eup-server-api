# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the EUP API project file into the container
COPY EUP.csproj ./

# Restore the dependencies
RUN dotnet restore "EUP.csproj"

# Copy the rest of the application code into the container
COPY . .

# Publish the application
RUN dotnet publish "EUP.csproj" -c Release -o /app/publish

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the published files from the build stage
COPY --from=build /app/publish .

# Set the entry point to run the API
ENTRYPOINT ["dotnet", "EUP.dll"]
