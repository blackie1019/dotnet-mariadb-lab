create table LabMariabDB.User
(
	Id int auto_increment,
	Name varchar(50) charset utf8 not null,
	BalanceAmount decimal(20,8) default 0.00000000 not null,
	DateCreated datetime default current_timestamp() not null,
	DateUpdated datetime null,
	constraint User_Id_uindex
		unique (Id)
)
;

alter table LabMariabDB.User
	add primary key (Id)
;

