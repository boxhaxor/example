.DEFAULT_GOAL := all

all: build-solution run-unit-tests docker-build-data-import docker-build-home
	echo done

build-solution:
	cd src/; dotnet build;

run-unit-tests: build-solution
	cd src/; dotnet test;

docker-build-data-import:
	docker build -t data-import-service -f src/data-import/Dockerfile .

docker-build-home:
	docker build -t home-service -f src/home/Dockerfile .

#make start-dependencies
start-dependencies:
	cd dependencies/; docker-compose up -d;

#make stop-dependencies
stop-dependencies:
	cd dependencies/; docker-compose down;

