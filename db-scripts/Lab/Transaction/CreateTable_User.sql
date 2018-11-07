create or replace table if not exists User
(
  Id   int auto_increment
    primary key,
  Name varchar(40) not null
);