create database ZP

use ZP

create table Sotr (ID int primary key, ФИО varchar(200), Телефон varchar(30), Электронная_почта varchar(100), Дата_приёма_на_работу date, Должность varchar(150))

create table Vremya_rab (ID int primary key, Сотрудник varchar(200), Время_начала_работы datetime, Время_окончания_работы datetime, Название varchar(200))

create table Doljnost (ID int primary key, Должность varchar(150), Заработная_плата decimal(30, 2))

create table Raschet (ID int primary key, Сотрудник varchar(200), Часы_работы decimal(30,2), Выплата_сотруднику decimal(30,2))

create table Users (ID int primary key, Логин varchar(80), Пароль varchar(30))

select * from Sotr
select * from Vremya_rab
select * from Doljnost
select * from Raschet
select * from Users

update Vremya_rab set Сотрудник = '' where Сотрудник = '' 
update Raschet set Сотрудник = '' where Сотрудник = '' 
update Vremya_rab set Время_начала_работы = '10.05.2023 12:00' where id = '0' 
update Vremya_rab set Время_окончания_работы = '10.11.2024 19:25' where id = '0' 
update Vremya_rab set Название = 'А А А (10.05.2023 12:00 - 10.11.2024 19:25)' where id = '0'

insert Sotr values ('2', 'б', '+7 (156) 165-1651', 'АА АА', '04.05.2023 1:19:42', 'Грузчик')
select Должность from Doljnost where id = '1'
ALTER TABLE Raschet ALTER COLUMN Часы_работы decimal(30,2)
ALTER TABLE Raschet ALTER COLUMN Часы_работы decimal(30,2)
update Vremya_rab set ID = '0' where ID = '1'
update Vremya_rab set Название = 'Петров Олег Шатунов (18.05.2023 12:30 - 18.05.2023 14:00)' where id = '2'
ALTER TABLE Vremya_rab add Название varchar(200)

ALTER TABLE Sotr ALTER COLUMN Телефон varchar(50)

select Raschet.ID, Raschet.Сотрудник,Raschet.Часы_работы,Doljnost.Заработная_плата,Raschet.Выплата_сотруднику from Raschet left join Sotr on Sotr.ФИО = Raschet.Сотрудник left join Doljnost on Doljnost.Должность = Sotr.Должность WHERE Raschet.ФИО LIKE '%jr%'
select Sotr.ID, Sotr.ФИО, Format(Sotr.Дата_приёма_на_работу,'dd.MM.yyyy') as Дата_приёма_на_работу, 
                Sotr.Телефон, Sotr.Электронная_почта, Doljnost.Должность, Doljnost.Заработная_плата from Sotr 
                left join Doljnost on Doljnost.Должность = Sotr.Должность WHERE  Sotr.ФИО LIKE '%%' and Doljnost.Должность = ''

delete from Sotr where id >= '0'
delete from Vremya_rab where id >= '0'
delete from Raschet where id >= '0'

select count(*) from Sotr where id >= '2'

delete from Raschet where id >= '1'
select Сотрудник from Vremya_rab where Vremya_rab.Название = 'Петров Олег Шатунов (2023.11.05 22:00)';
select Заработная_плата from Doljnost where Должность = 'Грузчик'
select Должность from Sotr where ФИО = 'Петров Олег Шатунов'
select Время_начала_работы from Vremya_rab where id = '0'
select count(*) from Doljnost
select Должность from Doljnost where id = '0'
select Должность from Sotr where id = '0'
select Название from Vremya_rab where id = '0'

update Vremya_rab set Сотрудник = 'Петров Олег Шатунов' where id = '0'

update Vremya_rab set Время_начала_работы = '2023.10.05 12:00' where id = '0'

update Vremya_rab set Время_окончания_работы = '2024.20.11 18:00' where id = '0'
update Vremya_rab set Название = 'Петров Олег Шатунов (Петров Олег Шатунов (18.05.2023 12:30 - 18.05.2023 14:00))' where id = '0'

update Sotr set ФИО = '' where id = ''
update Sotr set Дата_приёма_на_работу = '' where id = ''
update Sotr set Телефон = '' where id = ''
update Sotr set Электронная_почта = '' where id = ''
update Sotr set Должность = '' where id = ''

update Raschet set Сотрудник = 'Петров Олег Шатунов' where id = '0'
update Raschet set Часы_работы = '13207.42' where id = '0'
update Raschet set Выплата_сотруднику = '3168151985' where id = '0'
select Сотрудник from Vremya_rab where Название = ''

select Vremya_rab.ID, Sotr.ФИО, Format(Vremya_rab.Время_начала_работы, 'HH:mm  dd.MM.yyyy') as Время_начала_работы, Format(Vremya_rab.Время_окончания_работы, 'HH:mm  dd.MM.yyyy') 
as Время_окончания_работы from Vremya_rab left join Sotr on Sotr.ФИО = Vremya_rab.Сотрудник WHERE Сотрудник LIKE '%пет%'

insert Sotr values ('0', 'Петров Олег Шатунов', '+79304682461', 'SHOleg@mail.ru', '20220523', 'Грузчик')
insert Vremya_rab values('1', 'Петров Олег Шатунов', '2023.11.05 22:00', '2023.12.05 02:00', 'Петров Олег Шатунов (2023.11.05 22:00)') /*год-день-месяц*/ 
insert Doljnost values ('0', 'ГРузчик', '500')
insert Raschet values ('0', 'Петров Олег Шатунов', '4', '2000')
insert Users values ('0', 'Админ', '654987321')
insert Sotr values ('0', 'Петров Олег Шатунов', '+79304682461', 'SHOleg@mail.ru', '20220523', 'Грузчик')
insert Vremya_rab values('0', 'Петров Олег Шатунов', '2023.09.05 12:00', '2023.09.05 16:00')
	
insert Raschet values ('0', 'Петров Олег Шатунов', '4', '2000')
insert Users values ('1', 'Шатунов', '123')

insert Raschet values ('2', 'Petrov Oleg Shatunov', '2', '1016.52') 

/*Реальное время работы*/
select 
		Vremya_rab.ID, Vremya_rab.Сотрудник, Format(Vremya_rab.Время_начала_работы, 'HH:mm  dd.MM.yyyy') as Время_начала_работы, 
		Format(Vremya_rab.Время_окончания_работы, 'HH:mm  dd.MM.yyyy') as Время_окончания_работы 
from Vremya_rab
left join Sotr on Sotr.ФИО = Vremya_rab.Сотрудник

/*Сотрудники*/
select 
		Sotr.ID, Sotr.ФИО, Format(Sotr.Дата_приёма_на_работу,'dd.MM.yyyy') 
		as Дата_приёма_на_работу, Sotr.Телефон, Sotr.Электронная_почта, 
		Doljnost.Должность, Doljnost.Заработная_плата
from Sotr
left join Doljnost on Doljnost.Должность = Sotr.Должность

/*Расчёт зарплаты*/
select 
		Raschet.ID, 
		Sotr.ФИО,
		Raschet.Часы_работы,
		Doljnost.Заработная_плата,
		Raschet.Выплата_сотруднику
from Raschet
left join Sotr on Sotr.ФИО = Raschet.Сотрудник
left join Doljnost on Doljnost.Должность = Sotr.Должность

select * from Doljnost
select * from Users
