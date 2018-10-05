# dotnet-mariadb-lab #

## Setup ##
1. docker host mariadb:

    1. Pull docker image:

            docker pull mariadb

    2. Run up mariab with expose port and set passwd:

            docker run -p 3306:3306 --name lab-mariadb -e MYSQL_ROOT_PASSWORD=pass.123 -d mariadb

2. connect to MariaDB instance and create db *LabMariabDB*:

    CREATE DATABASE `LabMariabDB` /*!40100 DEFAULT CHARACTER SET latin1 */

3. Use *db-scripts>LAB>DDL>CreateUser.sql* to create Schema.
4. Insert first data manually, Name is *Blackie* and keep default values for remains. 
5. Execute Test Case under Mariadb.Labs>Mariadb.Lab.Test.