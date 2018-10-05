# dotnet-dapper-mysql-lab #

## Setup ##
1. docker host mariadb

    1. Pull docker image

            docker pull mariadb

    2. Run up mariab with expose port and set passwd

            docker run -p 3306:3306 --name lab-mariadb -e MYSQL_ROOT_PASSWORD=pass.123 -d mariadb

2. Use *db-scripts>LAB>DDL>CreateUser.sql* to create Schema
3. Execute Test Case under Mariadb.Labs>Mariadb.Lab.Test