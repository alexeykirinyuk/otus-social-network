FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ./src/Persistence/Otus.SocialNetwork.Persistence.Abstranctions ./src/Persistence/Otus.SocialNetwork.Persistence.Abstranctions
COPY ./src/Persistence/Otus.SocialNetwork.Persistence ./src/Persistence/Otus.SocialNetwork.Persistence
COPY ./src/Otus.SocialNetwork.Domain ./src/Otus.SocialNetwork.Domain
COPY ./src/Otus.SocialNetwork.Application ./src/Otus.SocialNetwork.Application
COPY ./src/Otus.SocialNetwork ./src/Otus.SocialNetwork

RUN dotnet publish -c Release -o out ./src/Otus.SocialNetwork

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app/
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Otus.SocialNetwork.dll"]