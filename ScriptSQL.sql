/*
use [master];
IF EXISTS (SELECT * FROM SYS.DATABASES WHERE NAME='course_work_EF')
BEGIN
	DROP DATABASE course_work_EF;
END
CREATE DATABASE course_work_EF;
*/

use course_work_EF;

drop table if exists [system];
drop table if exists order_details;
drop table if exists orders;
drop table if exists properties;
drop table if exists products;
drop table if exists buyer;

drop table if exists buyer;
create table buyer(
	buyer_id bigint identity primary key,
	[name] nvarchar(255) not null,
	[surname] nvarchar(255) not null,
	[sex] nvarchar(1) not null
	check([sex] = 'Ж' or [sex] = 'М')
);

drop table if exists [system];
create table [system](
	[system_user_id] bigint primary key,
	[login] nvarchar(255) not null,
	[password] nvarchar(255) not null,
	[is_admin] bit not null default 0,
	FOREIGN KEY ([system_user_id]) REFERENCES buyer (buyer_id)
);

drop table if exists products;
create table products(
	[product_id] bigint not null identity primary key,
	[product_name] nvarchar(max) not null,
	[unit_price] money,
	[number_of_orders] bigint default 0 not null
);

drop table if exists properties;
create table properties(
	[product_id] bigint not null,
	[attribute] nvarchar(max) not null,
	[value] nvarchar(max) not null,
	FOREIGN KEY ([product_id]) REFERENCES products ([product_id])
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

GO
IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'Trigger_counter' AND [type] = 'TR')
BEGIN
      DROP TRIGGER Trigger_counter;
END;
GO

--SELECT * FROM sys.objects where [type] = 'TR'

GO
CREATE TRIGGER Trigger_counter
ON order_details
AFTER INSERT
AS
BEGIN
	declare @quantity bigint, @product_id bigint;
	-- set @quantity = (select quantity from inserted);
	-- set @product_id = (select product_id from inserted);

	DECLARE [cursor] CURSOR FOR 
	SELECT quantity, product_id
	FROM inserted;

	OPEN [cursor] 
	--считываем данные первой строки в наши переменные
	FETCH NEXT FROM [cursor] INTO @quantity, @product_id;

	WHILE (@@FETCH_STATUS = 0) BEGIN  
		
		update products
		set number_of_orders += @quantity
		where product_id = @product_id;

		FETCH NEXT FROM [cursor] INTO @quantity, @product_id;
	END;

	--закрываем курсор
	CLOSE [cursor];
	DEALLOCATE [cursor];
END;
GO

insert into buyer([name], [surname], sex) values
('Авдеев', 'Тихон', 'М'),
('Брагин', 'Любомир', 'М'),
('Жданов', 'Леонтий', 'М'),
('Рыбаков', 'Павел', 'М'),
('Кузнецов', 'Фрол', 'М'),
('Мясникова', 'Амира', 'Ж'),
('Тетерина', 'Антонина', 'Ж'),
('Алексеева', 'Аксинья', 'Ж'),
('Красильникова', 'Анна', 'Ж'),
('Гуляева', 'Анита', 'Ж');

insert into [system]([system_user_id],[login],[password],[is_admin]) values
(1,'1', '1', 0),
(2,'2', '1', 1),
(3,'user3', 'BRQtbB0urH5UgQVfAZ9X', 1),
(4,'user4', 'LKhjjyegydQc4RyJGN20', 1),
(5,'user5', '9nznfooE20WqAviwV36J', 1),
(6,'user6', 'ysz10fxfJJwB5go4wWuZ', 0),
(7,'user7', 'odpf2WfpoJNeFwxqMxbw', 0),
(8,'user8', 'hnXRdYghr4DsWuodYM01', 0),
(9,'user9', 'tn9W4nm144D1swMTiJzv', 0),
(10,'user10', 'Y6QFMVD6m4PWguNwqTyP', 0);

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
('Видеокарта GIGABYTE GeForce RTX 2060 D6 6G', 50500),
('Видеокарта PowerColor AMD Radeon RX 6600 Fighter', 52000),
('23.8" Монитор LG 24EA430V-B', 15600),
('24" Монитор Samsung S24D332H', 16000),
('23.8" Монитор HP M24f', 15400),
('Клавиатура проводная A4Tech Bloody B810RC', 7999),
('Клавиатура проводная Logitech G413', 7999),
('Клавиатура проводная MSI GK50 ELITE', 7999),
('Мышь беспроводная Lenovo Go USB-C Wireless Mouse', 5050),
('Трекбол проводная Kensington K72337EU', 5150),
('Мышь беспроводная/проводная HP Omen Vector', 5050),
('Проводная моногарнитура Jabra EVOLVE 30 II UC Stereo', 5050),
('Наушники TWS JBL Live Free NC +', 10200),
('Радиочастотная гарнитура Nacon RIG800LXV2', 21500);

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
--select * from products;
--select * from properties;
select * from system;
select * from buyer;

insert into properties(product_id, attribute, [value]) values
(1, 'Гарантия', '12 мес.'),
(1, 'Модель', 'GIGABYTE GeForce RTX 2060 D6 6G (rev. 2.0)'),
(1, 'Микроархитектура', 'NVIDIA Turing'),
(1, 'Объем видеопамяти', '6 ГБ'),
(2, 'Графический процессор', 'Radeon RX 6600'),
(2, 'Микроархитектура', 'AMD RDNA 2'),
(3, 'Модель', 'LG 24EA430V-B'),
(3, 'Максимальное разрешение', '1920x1080'),
(3, 'Диагональ экрана (дюйм)', '23.8"'),
(5, 'Максимальное разрешение', '1920x1080'),
(6, 'Тип клавиатуры', 'механическая'),
(7, 'Модель', 'Logitech G413 CARBON'),
(8, 'Материал корпуса', 'пластик, металл'),
(9, 'Модель', 'Lenovo Go USB-C Wireless Mouse');

