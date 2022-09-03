use BookStoreApp

create table Books(
BookId int identity(1,1)Primary key,
BookName varchar(100),
Description varchar(max),
AuthorName varchar(50),
ActualPrice float,
DiscountedPrice float,
Quantity int,
BookImage varchar(max)
);


select * from Books
drop table Books

DROP PROCEDURE spAddBook;  
GO
--------------AddBook StoreProcedure-----------

create procedure spAddBook(  
@BookName varchar(100),
@Description varchar(max),   
@AuthorName varchar(50),  
@ActualPrice float,  
@DiscountedPrice float,
@Quantity int,  
@BookImage varchar(max)
)  
As  
Begin try  
insert into Books(BookName,Description,AuthorName,ActualPrice,DiscountedPrice,Quantity,BookImage) values(@BookName,@Description,@AuthorName,@ActualPrice,@DiscountedPrice,@Quantity,@BookImage)  
end try  
Begin catch  
SELECT   
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH


-----------GetAll StoreProcedure-----------


create procedure spGetAllBooks
As
Begin try
select * from Books
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

exec spGetAllBooks


---------------DeleteBook-----------

create procedure spDeleteBooks(@BookId int)
As
Begin try
delete from Books where BookId=@BookId;
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH


--------------GetBookById-----------

create procedure spGetBookById(@BookId int)
As
Begin try
select * from Books where BookId=@BookId;
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH



----------UpdateBook----------

create procedure spUpdateBook(
@BookId int,
@BookName varchar(100),
@Description varchar(max),   
@AuthorName varchar(50),  
@ActualPrice float,  
@DiscountedPrice float,
@Quantity int,  
@BookImage varchar(max)
)
As
Begin try
update Books set BookName=@BookName,Description=@Description,AuthorName=@AuthorName,ActualPrice=@ActualPrice,DiscountedPrice=@DiscountedPrice,Quantity=@Quantity,BookImage=@BookImage where BookId=@BookId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH