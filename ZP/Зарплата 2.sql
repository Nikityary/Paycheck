create database ZP

use ZP

create table Sotr (ID int primary key, ��� varchar(200), ������� varchar(30), �����������_����� varchar(100), ����_�����_��_������ date, ��������� varchar(150))

create table Vremya_rab (ID int primary key, ��������� varchar(200), �����_������_������ datetime, �����_���������_������ datetime, �������� varchar(200))

create table Doljnost (ID int primary key, ��������� varchar(150), ����������_����� decimal(30, 2))

create table Raschet (ID int primary key, ��������� varchar(200), ����_������ decimal(30,2), �������_���������� decimal(30,2))

create table Users (ID int primary key, ����� varchar(80), ������ varchar(30))

select * from Sotr
select * from Vremya_rab
select * from Doljnost
select * from Raschet
select * from Users

update Vremya_rab set ��������� = '' where ��������� = '' 
update Raschet set ��������� = '' where ��������� = '' 
update Vremya_rab set �����_������_������ = '10.05.2023 12:00' where id = '0' 
update Vremya_rab set �����_���������_������ = '10.11.2024 19:25' where id = '0' 
update Vremya_rab set �������� = '� � � (10.05.2023 12:00 - 10.11.2024 19:25)' where id = '0'

insert Sotr values ('2', '�', '+7 (156) 165-1651', '�� ��', '04.05.2023 1:19:42', '�������')
select ��������� from Doljnost where id = '1'
ALTER TABLE Raschet ALTER COLUMN ����_������ decimal(30,2)
ALTER TABLE Raschet ALTER COLUMN ����_������ decimal(30,2)
update Vremya_rab set ID = '0' where ID = '1'
update Vremya_rab set �������� = '������ ���� ������� (18.05.2023 12:30 - 18.05.2023 14:00)' where id = '2'
ALTER TABLE Vremya_rab add �������� varchar(200)

ALTER TABLE Sotr ALTER COLUMN ������� varchar(50)

select Raschet.ID, Raschet.���������,Raschet.����_������,Doljnost.����������_�����,Raschet.�������_���������� from Raschet left join Sotr on Sotr.��� = Raschet.��������� left join Doljnost on Doljnost.��������� = Sotr.��������� WHERE Raschet.��� LIKE '%jr%'
select Sotr.ID, Sotr.���, Format(Sotr.����_�����_��_������,'dd.MM.yyyy') as ����_�����_��_������, 
                Sotr.�������, Sotr.�����������_�����, Doljnost.���������, Doljnost.����������_����� from Sotr 
                left join Doljnost on Doljnost.��������� = Sotr.��������� WHERE  Sotr.��� LIKE '%%' and Doljnost.��������� = ''

delete from Sotr where id >= '0'
delete from Vremya_rab where id >= '0'
delete from Raschet where id >= '0'

select count(*) from Sotr where id >= '2'

delete from Raschet where id >= '1'
select ��������� from Vremya_rab where Vremya_rab.�������� = '������ ���� ������� (2023.11.05 22:00)';
select ����������_����� from Doljnost where ��������� = '�������'
select ��������� from Sotr where ��� = '������ ���� �������'
select �����_������_������ from Vremya_rab where id = '0'
select count(*) from Doljnost
select ��������� from Doljnost where id = '0'
select ��������� from Sotr where id = '0'
select �������� from Vremya_rab where id = '0'

update Vremya_rab set ��������� = '������ ���� �������' where id = '0'

update Vremya_rab set �����_������_������ = '2023.10.05 12:00' where id = '0'

update Vremya_rab set �����_���������_������ = '2024.20.11 18:00' where id = '0'
update Vremya_rab set �������� = '������ ���� ������� (������ ���� ������� (18.05.2023 12:30 - 18.05.2023 14:00))' where id = '0'

update Sotr set ��� = '' where id = ''
update Sotr set ����_�����_��_������ = '' where id = ''
update Sotr set ������� = '' where id = ''
update Sotr set �����������_����� = '' where id = ''
update Sotr set ��������� = '' where id = ''

update Raschet set ��������� = '������ ���� �������' where id = '0'
update Raschet set ����_������ = '13207.42' where id = '0'
update Raschet set �������_���������� = '3168151985' where id = '0'
select ��������� from Vremya_rab where �������� = ''

select Vremya_rab.ID, Sotr.���, Format(Vremya_rab.�����_������_������, 'HH:mm  dd.MM.yyyy') as �����_������_������, Format(Vremya_rab.�����_���������_������, 'HH:mm  dd.MM.yyyy') 
as �����_���������_������ from Vremya_rab left join Sotr on Sotr.��� = Vremya_rab.��������� WHERE ��������� LIKE '%���%'

insert Sotr values ('0', '������ ���� �������', '+79304682461', 'SHOleg@mail.ru', '20220523', '�������')
insert Vremya_rab values('1', '������ ���� �������', '2023.11.05 22:00', '2023.12.05 02:00', '������ ���� ������� (2023.11.05 22:00)') /*���-����-�����*/ 
insert Doljnost values ('0', '�������', '500')
insert Raschet values ('0', '������ ���� �������', '4', '2000')
insert Users values ('0', '�����', '654987321')
insert Sotr values ('0', '������ ���� �������', '+79304682461', 'SHOleg@mail.ru', '20220523', '�������')
insert Vremya_rab values('0', '������ ���� �������', '2023.09.05 12:00', '2023.09.05 16:00')
	
insert Raschet values ('0', '������ ���� �������', '4', '2000')
insert Users values ('1', '�������', '123')

insert Raschet values ('2', 'Petrov Oleg Shatunov', '2', '1016.52') 

/*�������� ����� ������*/
select 
		Vremya_rab.ID, Vremya_rab.���������, Format(Vremya_rab.�����_������_������, 'HH:mm  dd.MM.yyyy') as �����_������_������, 
		Format(Vremya_rab.�����_���������_������, 'HH:mm  dd.MM.yyyy') as �����_���������_������ 
from Vremya_rab
left join Sotr on Sotr.��� = Vremya_rab.���������

/*����������*/
select 
		Sotr.ID, Sotr.���, Format(Sotr.����_�����_��_������,'dd.MM.yyyy') 
		as ����_�����_��_������, Sotr.�������, Sotr.�����������_�����, 
		Doljnost.���������, Doljnost.����������_�����
from Sotr
left join Doljnost on Doljnost.��������� = Sotr.���������

/*������ ��������*/
select 
		Raschet.ID, 
		Sotr.���,
		Raschet.����_������,
		Doljnost.����������_�����,
		Raschet.�������_����������
from Raschet
left join Sotr on Sotr.��� = Raschet.���������
left join Doljnost on Doljnost.��������� = Sotr.���������

select * from Doljnost
select * from Users
