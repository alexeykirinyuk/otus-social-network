# build container
sudo docker build -t otus-sn-api -f ./src/Otus.SocialNetwork/Dockerfile .

# run
sudo docker run -p 80:80 -e ASPNETCORE_ENVIRONMENT=Staging -d otus-sn-api