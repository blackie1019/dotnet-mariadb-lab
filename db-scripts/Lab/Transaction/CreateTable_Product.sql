create or replace table if not exists Product
(
  Id   int auto_increment,
  Name varchar(40) charset utf8 not null,
  constraint Product_Id_uindex
  unique (Id)
);

alter table Product
  add primary key (Id);

