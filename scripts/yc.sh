ssh akirinyuk@51.250.107.91

scp -r ./Otus.SocialNetwork akirinyuk@51.250.107.91:/home/akirinyuk/otus-sn/

# build container and push
docker build -t cr.yandex/crplr7um5r6f5265e93d/otus-sn-api:test-0 \
  -f ./src/Otus.SocialNetwork/Dockerfile --push .
  
# publish
docker push cr.yandex/crplr7um5r6f5265e93d/otus-sn-api:v1.0.1

# pull
sudo docker pull --platform linux/arm64 cr.yandex/crplr7um5r6f5265e93d/otus-sn-api:test-0

sudo docker run -p 80:80 -e ASPNETCORE_ENVIRONMENT=Development \
 --platform linux/arm64 \
 cr.yandex/crplr7um5r6f5265e93d/otus-sn-api:test-0
 
 sudo docker run cr.yandex/crplr7um5r6f5265e93d/otus-sn-api:test-0