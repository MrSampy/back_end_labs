# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime

WORKDIR /app

EXPOSE 7292

EXPOSE 5065

COPY --from=build /app/out .

CMD ["dotnet", "Lab1.dll"]