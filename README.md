# RockContent likes

## Services
1. Login fake to tests
2. Query in article records
3. Like and dislike in articles

## Pattners
> Simple -> DDD, CQRS, Mediator

## Requirements

> .Net Core 5.0 -> https://dotnet.microsoft.com/download/dotnet/5.0

> Docker -> https://docs.docker.com/desktop/

## Local tests (docker)

> docker build -f Dockerfile -t rockcontent:latest .

> docker run --rm -d -p 3333:80 rockcontent

## Local tests (dotnet)

> dotnet restore

> dotnet publish -c Release -o out

> dotnet out/RockContent.Api.dll

## Swagger / Open API

> {{url}}/swagger

## Local automated tests

> dotnet test -v n