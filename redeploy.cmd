docker compose down
cd app-api
docker build . -t app-api
cd..
cd app-react
docker build . -t app-react
cd ..
docker-compose up -d --force-recreate --renew-anon-volumes