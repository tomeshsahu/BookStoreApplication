create table Admin(
AdminId int Identity(1,1)Primary key,
Email varchar(100),
Password varchar(100)
);

insert into Admin values('tomesh@gmail.com', 'Tomesh@123');
insert into Admin values('admin@gmail.com', 'Admin@123');

select * from Admin


-----------------Login StoreProcedure---------


create procedure spAdminLogin(   
@Email varchar(100),  
@Password varchar(100)
)  
As  
Begin try  
select * from Admin where Email=@Email and Password=@Password;
end try  
Begin catch  
SELECT   
 ERROR_NUMBER() AS ErrorNumber,  
 ERROR_STATE() AS ErrorState,  
 ERROR_PROCEDURE() AS ErrorProcedure,  
 ERROR_LINE() AS ErrorLine,  
 ERROR_MESSAGE() AS ErrorMessage;  
END CATCH

exec spAdminLogin 'admin@gmail.com', 'Admin@123'