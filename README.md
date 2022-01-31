# This is a sample application.

# To build and run tests.
```
make
```
* This requires:
** docker
** dotnet 6+
** make Windows: (http://gnuwin32.sourceforge.net/packages/make.htm) otherwise use your linux package manager, eg: apt-get update && apt-get install -y make


# Run locally in docker
## [Startup the dependencies](/dependencies/README.md#)
This will start the app too, make sure you built first.  This might fail the first time. (TODO: fix.)
```
make start-dependencies
```

# Run in VS
Todo.

## Navigate to the app
[home] (http://localhost:8881)
[data-import] (http://localhost:8882)


Scaffold for the app:
-Steeltoe
-Something for metrics
-Docker compose file with dependencies
--kafka (And whatever else needed for kafka)
--postgres
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
