sp_helptext spResetPassword

-----------Register Procedure-----

create procedure spAddUser(  
@FullName varchar(100),   
@Email varchar(100),  
@Password varchar(100),  
@Mobile varchar (50)  
)  
As  
Begin try  
insert into Users(FullName,Email,Password,Mobile) values(@FullName,@Email,@Password,@Mobile)  
end try  
Begin catch  
SELECT   
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH



----------Login Procedure----------
create procedure spLoginUser(   
@Email varchar(100)  
)  
As  
Begin try  
select * from Users where Email=@Email  
end try  
Begin catch  
SELECT   
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH




--------Forgate Password----------

Create procedure spForgetPasswordUser(  
@Email varchar(100)  
)  
As  
Begin try  
select * from Users where Email=@Email   
end try  
Begin catch  
SELECT   
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH


------------Reset Password---------

Create procedure spResetPassword(  
@Email varchar(100),  
@Password varchar(100)  
)  
As  
Begin try  
update Users set Password=@Password where Email=@Email   
end try  
Begin catch  
SELECT   
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH