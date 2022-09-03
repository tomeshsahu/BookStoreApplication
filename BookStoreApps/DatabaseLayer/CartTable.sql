create table Cart(
CartId int Identity(1,1)Primary key,
BookId int FOREIGN KEY REFERENCES Books(BookId),
UserId int FOREIGN KEY REFERENCES Users(UserId),
BookQuantity int
)


select * from Cart

-------Create Store Procedure------
alter procedure spAddCart(
@BookId int,
@UserId int,  
@BookQuantity int
)  
As  
Begin try  
insert into Cart (BookId,UserId,BookQuantity) values (@BookId,@UserId,@BookQuantity) 
end try  
Begin catch  
SELECT   
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH

select * from Cart

----------DeleteCart Procedure--------

alter procedure spDeleteCart(
@CartId int,
@UserId int
)  
As  
Begin try  
delete from Cart where CartId=@CartId and UserId=@UserId
end try  
Begin catch  
SELECT   
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH


----------UpdateCart-----------
create procedure spUpdateCart(
@CartId int,
@UserId int,
@BookQuantity int
)  
As  
Begin try  
update Cart set BookQuantity=@BookQuantity where CartId=@CartId and UserId=@UserId
end try  
Begin catch  
SELECT   
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH



----------SpGetAllCart----------

alter procedure spGetAllCart(
@UserId int
)  
As  
Begin try  
select Cart.CartId,Cart.BookId,Cart.BookQuantity,Cart.UserId,
Books.Description,Books.BookName,Books.AuthorName,Books.ActualPrice,Books.DiscountedPrice,Books.BookImage
from Cart inner join Books on Cart.BookId=Books.BookId
where Cart.UserId=@UserId

end try  
Begin catch  
SELECT   
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH