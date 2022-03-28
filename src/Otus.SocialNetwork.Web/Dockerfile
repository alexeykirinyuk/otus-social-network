FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ./src/Otus.SocialNetwork.Web ./src/Otus.SocialNetwork.Web

RUN dotnet publish -c Release -o out ./src/Otus.SocialNetwork.Web

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=build-env /app/out/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf