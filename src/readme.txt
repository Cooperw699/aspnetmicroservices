command:
docker images

------
redis
pull docker
command:
docker pull redis
run in docker
command:
docker run -d -p 6379:6379 --name aspnetrun-redis redis

logs
docker logs -f aspnetrun-redis

run command in redis
docker exec -it aspnetrun-redis /bin/bash
redis-cli

----
postgres

docker pull postgres
$ docker run -d -p 5432:5432 --name distcount-postgres -e POSTGRES_PASSWORD=admin1234 postgres:alpine -c shared_buffers=256MB -c max_connections=200