﻿namespace Recurly
 {
     /// <summary>
     /// Exception when a validation error prevents an object from being created, updated, or deleted.
     /// See the Errors collection for more information.
     /// </summary>
     public class RecurlyValidationException : RecurlyException
     {
         /// <summary>
         /// HTTP Status Code 422 is "Unprocessable Entity"
         /// </summary>
         internal const int HttpStatusCode = 422;

         internal RecurlyValidationException(Error[] errors)
             : base("The information being saved is not valid.", errors)
         { }

         internal RecurlyValidationException(string message, Error[] errors)
             : base(message, errors)
         { }
     }
 }