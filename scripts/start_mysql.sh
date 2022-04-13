#!/bin/bash
MIGRATION_PATH=$(pwd)/../src/Persistence/Otus.SocialNetwork.Persistence/Migrations

docker rm $(docker ps --filter name=otus-social-network-db -a -q) -f
docker run --name otus-social-network-db -p 3306:3306 \
    -e "MYSQL_DATABASE=otus_social_network" \
    -e "MYSQL_ROOT_PASSWORD=mysql" \
    -v "/Users/a.kirinyuk/mysql:/var/lib/mysql" \
    --platform linux/x86_64 -d \
    mysql