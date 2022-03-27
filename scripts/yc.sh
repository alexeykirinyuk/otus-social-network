# build container
sudo docker build -t otus-sn-api -f ./src/Otus.SocialNetwork/Dockerfile .

# run
MIGRATION_PATH=$(pwd)/../src/Persistence/Otus.SocialNetwork.Persistence/Migrations
sudo docker run --name otus-social-network-db -p 3306:3306 \
    -v $MIGRATION_PATH:/docker-entrypoint-initdb.d/ \
    -e "MYSQL_DATABASE=otus_social_network" \
    -e "MYSQL_ROOT_PASSWORD=mysql" \
    -d \
    mysql
    
sudo docker run -p 80:80 -e ASPNETCORE_ENVIRONMENT=Development -d otus-sn-api