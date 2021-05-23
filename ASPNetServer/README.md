# IO.Swagger - ASP.NET Core 2.0 Server

This API will allow my frontends to communicate with my backends. Should be the same in every implementation, but I'm just testing right now anyway.

## Run

Linux/OS X:

```
sh build.sh
```

Windows:

```
build.bat
```

## Run in Docker

```
cd src/IO.Swagger
docker build -t io.swagger .
docker run -p 5000:5000 io.swagger
```
