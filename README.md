# TeaRoundPicker

## Prerequists

- [Net 5](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Docker](https://www.docker.com/products/docker-desktop)

Install global EntityFramework tool 

```console 
dotnet tool install --global dotnet-ef
```

## Setup
First we need to set up you're local environment and make sure you're database is up to date.

Open the command line in the root directory (wherever you've created the git repo)

Run the following 
```console
docker-compose up -d
```

_N.B. This will set up a local postgres database._

Now run 

```console
dotnet ef database update --project src/Persistence --startup-project src/WebAPI
```

_N.B. This will update the database schema to match the current database object in the dotnet project_

## Start Up

From the root directory run the following commands

```console
cd src/WebAPI
dotnet run
```
_This moves the context of the command line to the WebAPI src code and starts the WebAPI with the URL https://localhost:5001_

If you navigate to https://localhost:5001/swagger you can access the swagger documentation related to the API

## Add new migrations
If you update or create any new DbModels we need to update the EF migration script. The migration script 
will generate the sql needed to change the schema.

Run 
```console
dotnet ef migrations add ${TheNameOfTheNewMigrationScript} --project src/Persistence --startup-project src/WebAPI
```
