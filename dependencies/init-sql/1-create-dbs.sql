CREATE USER docker;
ALTER USER docker PASSWORD 'docker_pass';
CREATE DATABASE home;
GRANT ALL PRIVILEGES ON DATABASE home TO docker;
CREATE DATABASE data_import;
GRANT ALL PRIVILEGES ON DATABASE data_import TO docker;