
using ProjectName.Types;

namespace ProjectName.Interfaces
{
    public interface IArticleService
    {
        /// <summary>
        /// Creates an article with the specified details
        /// </summary>
        /// <param name="articleDto">The data transfer object containing article details</param>
        /// <returns>The ID of the created article</returns>
        /// <exception cref="DP422Exception">Thrown when mandatory arguments are null</exception>
        /// <exception cref="DP404Exception">Thrown when author, blog category, or tag does not exist</exception>
        /// <exception cref="DP500Exception">Thrown when an error occurs during database insertion</exception>
        Task<string> CreateArticle(CreateArticleDto articleDto);

        /// <summary>
        /// Get the specified article
        /// </summary>
        /// <param name="articleRequest">The request containing either article ID or name</param>
        /// <returns>The retrieved article</returns>
        /// <exception cref="DP422Exception">Thrown when ID and name are both null or empty</exception>
        /// <exception cref="DP404Exception">Thrown when the article does not exist</exception>
        Task<Article> GetArticle(ArticleRequestDto articleRequest);
    }
}

