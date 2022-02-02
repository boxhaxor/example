# This is a sample application.

# To build and run tests.
```
make
```
* This requires:
** docker
** dotnet 6+
** make Windows: (https://community.chocolatey.org/packages/make) otherwise use your linux package manager, eg: apt-get update && apt-get install -y make


# Run locally in docker
## [Startup the dependencies](/dependencies/README.md#)
This will start the app too, make sure you built first.  This might fail the first time. (TODO: fix.)
```
make start-dependencies
```

# Run in VS
Todo.

## Navigate to the app 
[home] (https://localhost:7096/)
[data-import] (https://localhost:7139/)


Scaffold for the app:
-Steeltoe (done)
-Something for metrics
-Docker compose file with dependencies (done)
--kafka (And whatever else needed for kafka) (Started)
--postgres (Started)
--external config
-2 services
-- Data import
-- home service



Technologies:
Kafka
Postgres
steeltoe

Optional:
External Configuration ?  What's the default here for steeltoe?
Unit tests
-I'll at least setup a project for it.
