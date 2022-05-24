FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ./src/Persistence/Otus.SocialNetwork.Persistence.Abstranctions/Otus.SocialNetwork.Persistence.Abstranctions.csproj ./src/Persistence/Otus.SocialNetwork.Persistence.Abstranctions/
COPY ./src/Persistence/Otus.SocialNetwork.Persistence/Otus.SocialNetwork.Persistence.csproj ./src/Persistence/Otus.SocialNetwork.Persistence/
COPY ./src/Otus.SocialNetwork.Domain/Otus.SocialNetwork.Domain.csproj ./src/Otus.SocialNetwork.Domain/
COPY ./src/Otus.SocialNetwork.Application/Otus.SocialNetwork.Application.csproj ./src/Otus.SocialNetwork.Application/
COPY ./src/Otus.SocialNetwork/Otus.SocialNetwork.csproj ./src/Otus.SocialNetwork/

RUN dotnet restore ./src/Otus.SocialNetwork/Otus.SocialNetwork.csproj

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