# This is a sample application.

# To build and run tests.
```
make
```
# This requires:
- docker
- dotnet 6+
- ef cli (dotnet tool install --global dotnet-ef)  or (dotnet tool update --global dotnet-ef)
- make -- For Windows: (https://community.chocolatey.org/packages/make) otherwise use your linux package manager, eg: apt-get update && apt-get install -y make


# Run locally in docker
## [Startup the dependencies](/dependencies/README.md#)
This will start the app too, make sure you built first.  This might fail the first time. (TODO: fix.)
```
make start-dependencies
```

# Run in VS
- Todo.

## Navigate to the app 
[home] (https://localhost:7096/)
[data-import] (https://localhost:7139/)


Scaffold for the app:
- Steeltoe (done)
- Something for metrics (Not done)
- Data import service (done)
- home service (done)


Technologies:
- Kafka (done)
- Postgres (done)
- Spring Config (done)

Optional:
- Unit tests (Created but they don't do much currently.  TODO)
