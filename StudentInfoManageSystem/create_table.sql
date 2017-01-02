create table college(
	Id int identity(1, 1) not null primary key,
	Name nvarchar(30) not null
)

create table major(
	Id int identity(1, 1) not null primary key,
	Name nvarchar(30) not null,
	CollegeId int not null,
	constraint FK_MAJOR_COLLEGE foreign key (CollegeId) references college on delete cascade
)

create table majorClass(
	Id int identity(1, 1) not null primary key,
	Name nvarchar(30) not null,
	MajorId int not null,
	constraint FK_MAJORCLASS_MAJOR foreign key (MajorId) references major on delete cascade
)

create table student(
	Id nvarchar(20) not null primary key,
	Name nvarchar(20) not null,
	MajorClassId int not null,
	Age int,
	Sex nvarchar(2),
	constraint FK_STUDENT_MAJORCLASS foreign key (MajorClassId) references majorClass on delete cascade
)

create table siuser(
	Id int identity(1, 1) not null primary key,
	Username nvarchar(20) not null,
	Pwd		nvarchar(20) not null,
	CollegeId int not null,
	constraint FK_SIUSER_COLLEGE foreign key (CollegeId) references college on delete cascade
)