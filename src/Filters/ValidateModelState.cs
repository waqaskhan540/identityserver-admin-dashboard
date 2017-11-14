using IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class ValidateModelState : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validationErrors = string.Join(",",
                                          context.ModelState.Values.Where(E => E.Errors.Count > 0)
                                         .SelectMany(E => E.Errors)
                                         .Select(E => E.ErrorMessage)
                                         .ToArray());

                context.Result = new ObjectResult(BaseModel.Error(validationErrors));
            }
        }
    }
}
