```
docker build -t efcore.pg-847 .
docker run --rm efcore.pg-847 host.docker.internal 5432 {username} {password}
```