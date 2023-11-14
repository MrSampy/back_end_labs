FROM mcr.microsoft.com/mssql/server:2019-latest

ENV SA_PASSWORD=P@ssw0rd123
ENV ACCEPT_EULA=Y

EXPOSE 1433