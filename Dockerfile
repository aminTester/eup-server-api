# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the EUP API project file and restore dependencies
COPY ["EUP.csproj", "EUP/"]
RUN dotnet restore "EUP/EUP.csproj"

# Copy the entire project and publish it
COPY . .
WORKDIR "/EUP"
RUN dotnet publish "EUP.csproj" -c Release -o /app/publish

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the published files from the build stage
COPY --from=build /app/publish .

# Set the entry point to run the API
ENTRYPOINT ["dotnet", "EUP.dll"]
