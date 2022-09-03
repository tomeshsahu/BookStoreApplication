using BusinessLayer.Interface;
using DatabaseLayer.BookModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class BookController : ControllerBase
    {
        IBookBL bookBL;

        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }
     
        [HttpPost("AddBook")]
        public IActionResult AddBook(BookPostModel bookPostModel)
        {
            try
            { 
                var result = bookBL.AddBook(bookPostModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Added Successfully...!" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Book Added UnSuccessfully..." });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = this.bookBL.GetAllBooks();
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Get All Books Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Get All Book Unsuccessfull", });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int BookId)
        {
            try
            {
                var result = this.bookBL.DeleteBook(BookId);
                if (result != 0)
                {
                    return this.Ok(new { success = true, message = "Books Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = true, message = "Books Deleted UnSuccessfully" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetBookById")]
        public IActionResult GetBookById(int BookId)
        {
            try
            {
                var result = this.bookBL.GetBookById(BookId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Get Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = true, message = "Books Get UnSuccessfully" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("UpdateBook")]
        public IActionResult updateBook(int BookId,BookPostModel bookPostModel)
        {
            try
            {
                var result = this.bookBL.UpdateBook(BookId, bookPostModel);
                if(result!=null)
                {
                    return this.Ok(new { success = true, message = "Book Updated Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = true, message = "Book Not Updated " });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
       

    }
}
