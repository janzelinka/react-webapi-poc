docker compose down
cd ing-app-api
docker build . -t ing-app-api
cd..
cd ing-app-react
docker build . -t ing-app-react
cd ..
docker-compose up -d --force-recreate --renew-anon-volumes