.DEFAULT_GOAL := all

all: build-solution run-unit-tests docker-build-data-import docker-build-home restart-custom-services
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

#make restart-custom-services
#Should fix this to use profiles in the docker-compose incase user wants to run custom services without docker
restart-custom-services:
	cd dependencies/; docker-compose restart home-service data-import-service || true

#make stop-dependencies
stop-dependencies:
	cd dependencies/; docker-compose down;

