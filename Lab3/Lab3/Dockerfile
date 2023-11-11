# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime

WORKDIR /app

EXPOSE 5175

EXPOSE 7013

EXPOSE 80

ENV TZ=Europe/Kiev

COPY --from=build /app/out .

CMD ["dotnet", "Lab3.dll"]