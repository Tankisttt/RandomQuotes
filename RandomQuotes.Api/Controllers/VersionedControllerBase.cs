using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RandomQuotes.Resources;

namespace RandomQuotes.Api.Controllers
{
    /// <summary>
    ///     Base controller supports versioning
    /// </summary>
    [ApiController]
    public abstract class VersionedControllerBase : ControllerBase
    {
        /// <summary>
        /// Mapper instance
        /// </summary>
        protected readonly IMapper Mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper">Mapper dependency</param>
        protected VersionedControllerBase(IMapper mapper)
        {
            Mapper = mapper;
        }

        /// <summary>
        /// Returns action result for error as <see cref="ProblemDetails"/> with proper status code
        /// </summary>
        /// <param name="errorModel">Error model</param>
        private IActionResult ReturnError(ErrorModel errorModel)
        {
            var statusCode = GetStatusCode(errorModel.Kind);
            var problemDetails = new ProblemDetails
            {
                Type = errorModel.Key,
                Title = errorModel.Message,
                Instance = Request.Path,
                Status = (int)statusCode
            };

            if (errorModel.Errors is { Count: > 0 })
                problemDetails.Extensions[nameof(errorModel.Errors)] = errorModel.Errors;

            if (errorModel.Kind == ErrorKind.NotFound)
                Request.HttpContext.Response.Headers.Add("X-ServiceFabric", "ResourceNotFound");

            return new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status
            };
        }

        /// <summary>
        /// Returns ActionResult from the given WriteResult
        /// </summary>
        /// <remarks>
        /// Use generic param to define in which model successful result should be mapped 
        /// </remarks>
        /// <param name="source">Write result with error or data</param>
        /// <typeparam name="T">Desired response model</typeparam>
        protected IActionResult RenderResult<T>(IWriteResult<object> source)
        {
            if (!source.IsSuccess)
                return ReturnError(source.Error);

            if (source.ResultData == null)
                return new NoContentResult();

            var result = Mapper.Map<T>(source.ResultData);
            return new JsonResult(result);
        }

        /// <summary>
        /// Returns ActionResult from the given WriteResult
        /// </summary>
        /// <param name="source">Write result</param>
        protected IActionResult RenderResult(WriteResult source)
            => source.IsSuccess ? new NoContentResult() : ReturnError(source.Error);

        private static HttpStatusCode GetStatusCode(ErrorKind errorKind)
        {
            return errorKind switch
            {
                ErrorKind.Forbidden => HttpStatusCode.Forbidden,
                ErrorKind.NotFound => HttpStatusCode.NotFound,
                ErrorKind.Default => HttpStatusCode.BadRequest,
                _ => throw new ArgumentOutOfRangeException(nameof(errorKind), errorKind, null)
            };
        }
    }
}