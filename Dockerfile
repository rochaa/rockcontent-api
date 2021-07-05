FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
COPY . /app

WORKDIR /app/src/RockContent.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app/dll
COPY --from=build-env /app/src/RockContent.Api/out /app/dll

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ROCKCONTENT_DATABASE_NAME=rockcontent

CMD ["dotnet", "RockContent.Api.dll"]