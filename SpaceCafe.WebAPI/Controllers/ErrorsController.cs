//using System.ComponentModel.DataAnnotations;----->bunu sakın kullanma bu farklı ValidationException
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SpaceCafe.Application.Common.CustomExceptions;

namespace SpaceCafe.WebAPI.Controllers;

[Route("error")]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult HandleError()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();//
        var exception = context?.Error;

        if (exception is CustomValidationException validationException)
        {
            var modelState = new ModelStateDictionary();

            // Hataları ModelState içine ekle
            foreach (var error in validationException.Errors)
            {
                modelState.AddModelError(error.Key, error.Value);
            }

            var problemDetails = new ValidationProblemDetails(modelState)
            {
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest
            };

            return BadRequest(problemDetails);
        }
        /*
        if (exception is ValidationException validationException)
        {
            return Problem(
                detail: validationException.Message,
                statusCode: StatusCodes.Status400BadRequest);
        }*/

        else if (exception is DuplicateEmailError duplicateEmailException)
        {
            var (statusCode, message) = exception switch
            {
                DuplicateEmailError => (StatusCodes.Status409Conflict, "Email already exists."),
            };

            return Problem(
                statusCode: statusCode,
               title: message);
        }

        else if (exception is CustomException customException)
        {
            // CustomException'dan gelen başlık ve mesajı kullanıcıya döneriz
            return Problem(
                title: customException.Title,
                detail: customException.Message,
                statusCode: StatusCodes.Status400BadRequest);

        }

        return Problem(
             title: "Sunucu Hatası",
             detail: "Beklenmedik bir hata oluştu, lütfen daha sonra tekrar deneyiniz.",
             statusCode: StatusCodes.Status500InternalServerError);
    }

}

//if (exception is DuplicateEmailError duplicateEmailException)
//{
//    var (statusCode, message) = exception switch
//    {
//       DuplicateEmailError => (StatusCodes.Status409Conflict, "Email already exists."),
//    };

//    return Problem(
//        statusCode: statusCode,
//        title: message);
//}


/*
if (exception is CustomException customException)
{
    // CustomException'dan gelen başlık ve mesajı kullanıcıya döneriz
    return Problem(
        title: customException.Title,
        detail: customException.Message,
        statusCode: StatusCodes.Status400BadRequest);

}
*/
// Diğer hatalar için genel bir mesaj döneriz


//public class ErrorsController : ControllerBase
//{

//    [Route("/error")]

//    public IActionResult Error()
//    {
//        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
//        return Problem(statusCode: 400);
//    }

//}
