create Table AddressTypeTable
(
	TypeId INT IDENTITY(1,1) PRIMARY KEY,
	AddressType varchar(255)
)
insert into AddressTypeTable values('Home'),('Office'),('Other');

select * from AddressTypeTable

drop table AddressTable
select * from AddressTable


create table AddressTable(
AddressId int Identity(1,1)Primary key,
Address varchar(max),
City varchar(100),
State varchar(100),
TypeId int 
FOREIGN KEY (TypeId) REFERENCES AddressTypeTable(TypeId),
UserId INT FOREIGN KEY (UserId) REFERENCES Users(UserId)
)

-------------Add Address Stored procedure--------

create procedure SPAddAddress
(
@Address varchar(max),
@City varchar(100),
@State varchar(100),
@TypeId int,
@UserId int
)
as
BEGIN
If Exists (select * from AddressTypeTable where TypeId = @TypeId)
begin
Insert into AddressTable values(@Address, @City, @State, @TypeId, @UserId);
end
Else
begin
select 2
end
End;

select * from AddressTable

--------Address Delete--------

alter procedure spDeleteAddress
(
@UserId int,
@AddressId int
)
As
Begin try
delete from AddressTable where UserId=@UserId and AddressId=@AddressId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH


--------Update Addresss---------

create procedure spUpdateAddress
(
	@AddressId int,
	@UserId int,
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int
)
as
BEGIN
If Exists (select * from AddressTypeTable where TypeId = @TypeId)
begin
Update AddressTable set Address = @Address, City = @City, State = @State , TypeId = @TypeId where AddressId = @AddressId and UserId=@UserId
end
Else
begin
select 2
end
End;



-----------getAddress----------
create procedure spGetAddress
(
@UserId int,
@AddressId int
)
As
Begin try
select * from AddressTable where UserId=@UserId and AddressId=@AddressId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH