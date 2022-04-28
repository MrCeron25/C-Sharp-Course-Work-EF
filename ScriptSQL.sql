/*
use [master];
IF EXISTS (SELECT * FROM SYS.DATABASES WHERE NAME='course_work_EF')
BEGIN
	DROP DATABASE course_work_EF;
END
CREATE DATABASE course_work_EF;
*/
use course_work_EF;

drop table if exists order_details;
drop table if exists orders;
drop table if exists buyer;
drop table if exists properties;
drop table if exists products;
drop table if exists [system];

drop table if exists [system];
create table [system](
	[system_user_id] bigint identity primary key,
	[login] nvarchar(255) not null,
	[password] nvarchar(255) not null,
	[is_admin] bit not null default 0,
	[buyer_id] bigint unique
);

drop table if exists products;
create table products(
	[product_id] bigint not null identity primary key,
	[product_name] nvarchar(max) not null,
	[unit_price] money
);

drop table if exists properties;
create table properties(
	[product_id] bigint not null,
	[attribute] nvarchar(max) not null,
	[value] nvarchar(max) not null,
	FOREIGN KEY ([product_id]) REFERENCES products ([product_id])
);

drop table if exists buyer;
create table buyer(
	buyer_id bigint identity primary key,
	[name] nvarchar(255) not null,
	[surname] nvarchar(255) not null,
	[sex] nvarchar(1) not null
	check([sex] = '�' or [sex] = '�'),
	FOREIGN KEY (buyer_id) REFERENCES [system] ([system_user_id])
);

drop table if exists orders;
create table orders(
	order_id bigint identity primary key,
	[buyer_id] bigint not null,
	[order_date] datetime not null default GETDATE(),
	FOREIGN KEY ([buyer_id]) REFERENCES buyer (buyer_id)
);

drop table if exists order_details;
create table order_details(
	[order_id] bigint not null,
	[product_id] bigint not null,
	primary key ([order_id], [product_id]),
	[unit_price] money,
	[quantity] bigint,
	[total_price] as ([unit_price] * [quantity]),
	FOREIGN KEY ([order_id]) REFERENCES [orders] (order_id),
	FOREIGN KEY ([product_id]) REFERENCES products ([product_id])
);

insert into [system]([login],[password],[is_admin],[buyer_id]) values
('1', '1', 0, 1),
('2', '1', 1, 2),
('user3', 'BRQtbB0urH5UgQVfAZ9X', 1, 3),
('user4', 'LKhjjyegydQc4RyJGN20', 1, 4),
('user5', '9nznfooE20WqAviwV36J', 1, 5),
('user6', 'ysz10fxfJJwB5go4wWuZ', 0, 6),
('user7', 'odpf2WfpoJNeFwxqMxbw', 0, 7),
('user8', 'hnXRdYghr4DsWuodYM01', 0, 8),
('user9', 'tn9W4nm144D1swMTiJzv', 0, 9),
('user10', 'Y6QFMVD6m4PWguNwqTyP', 0, 10);

insert into buyer([name], [surname], sex) values
('������', '�����', '�'),
('������', '�������', '�'),
('������', '�������', '�'),
('�������', '�����', '�'),
('��������', '����', '�'),
('���������', '�����', '�'),
('��������', '��������', '�'),
('���������', '�������', '�'),
('�������������', '����', '�'),
('�������', '�����', '�');

insert into orders([buyer_id]) values
(1),
(2),
(3),
(4),
(5),
(6),
(7),
(8),
(9),
(10);

insert into products(product_name, unit_price) values
('���������� GIGABYTE GeForce RTX 2060 D6 6G', 50500),
('���������� PowerColor AMD Radeon RX 6600 Fighter', 52000),
('23.8" ������� LG 24EA430V-B', 15600),
('24" ������� Samsung S24D332H', 16000),
('23.8" ������� HP M24f', 15400),
('���������� ��������� A4Tech Bloody B810RC', 7999),
('���������� ��������� Logitech G413', 7999),
('���������� ��������� MSI GK50 ELITE', 7999),
('���� ������������ Lenovo Go USB-C Wireless Mouse', 5050),
('������� ��������� Kensington K72337EU', 5150),
('���� ������������/��������� HP Omen Vector', 5050),
('��������� ������������� Jabra EVOLVE 30 II UC Stereo', 5050),
('�������� TWS JBL Live Free NC +', 10200),
('�������������� ��������� Nacon RIG800LXV2', 21500);

insert into order_details(order_id, product_id, unit_price, quantity) values
(1, 1, 50500, 2),
(1, 14, 21500, 2),
(2, 2, 52000, 1),
(3, 3, 15600, 1),
(4, 4, 16000, 1),
(5, 5, 15400, 2),
(6, 6, 7999, 4),
(7, 7, 7999, 2),
(8, 8, 7999, 1),
(9, 9, 5050, 3),
(10, 10, 5150, 1);

--select * from order_details;

--insert into products(product_id, attribute, [value], unit_price) values
--(1, '', '', 3000);

/*
insert into order_details([order_id], [product_id]) values
(1),


drop table if exists order_details;
create table order_details(
	[order_id] bigint not null,
	[product_id] bigint not null,
	primary key ([order_id], [product_id]),
	[unit_price] money,
	[quantity]
*/