Just an example of ASP.NET Web API + React app with material-ui and typescript, containerized via Docker, created orchestration with docker-compose.
Docker-compose cluster contains 3 containers:
- React APP (port: 8080), for building the image: **docker build . -t react-nginx-app**
- ASP.NET Web Api (port: 80), for building the image: **docker build . -t ing-app-api**
- Portainer - service for monitoring of the cluster.

- To run it all in once: **docker compose up -d**
