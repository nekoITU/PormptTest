using Microsoft.AspNetCore.Mvc;
using ProjectName.Types;
using ProjectName.ControllersExceptions;
using ProjectName.Interfaces;


namespace ProjectName.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateArticle(Request<CreateArticleDto> request)
        {
            return await SafeExecutor.ExecuteAsync(async () =>
            {
                var articleId = await _articleService.CreateArticle(request.Payload);
                return Ok(new Response<string> { Payload = articleId });
            });
        }

        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> GetArticle(Request<ArticleRequestDto> request)
        {
            return await SafeExecutor.ExecuteAsync(async () =>
            {
                var article = await _articleService.GetArticle(request.Payload);
                return Ok(new Response<Article> { Payload = article });
            });
        }
    }
}

// SafeExecutor.cs


namespace ProjectName
{
    public static class SafeExecutor
    {
        public static async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                // Convert the exception to a ResponseException using your serializer
                var responseException = ExceptionSerializer.Serialize(ex);

                // Now, return an OkObjectResult that wraps the ResponseException
                // This converts the ResponseException into an IActionResult, specifically an OkObjectResult
                var response = new Response<object>
                {
                    Payload = null, // No payload in case of exception
                    Exception = responseException
                };
                return new OkObjectResult(response);
            }
        }
    }
}