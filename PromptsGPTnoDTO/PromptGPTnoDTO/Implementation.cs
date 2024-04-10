using ProjectName.Interfaces;
using ProjectName.Types;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ProjectName.ControllersExceptions;


namespace ProjectName.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IDbConnection _dbConnection;

        public ArticleService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<string> CreateArticle(CreateArticleDto request) // createArtcile ******************************** PASCALCASE
        {
            // Validate all fields of request.payload are not null except for [Summary, Body, BlogCategories, GoogleDriveId, Tags]
            if (string.IsNullOrEmpty(request.Title) || request.AuthorId == Guid.Empty || string.IsNullOrEmpty(request.Langcode) || request.ImageId == Guid.Empty || request.PDF == Guid.Empty)
            {
                HandleException("DP-422", "Mandatory argument is null", "Business");
            }

            // Fetch author from database by id from argument ID
            var author = await _dbConnection.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Author WHERE Id = @AuthorId", new { request.AuthorId }); //Authors **********
            if (author == null)
            {
                HandleException("DP-404", "Author not found", "Technical");
            }

            //if author found print its id
            Console.WriteLine(author.Name);

            // Fetch blog categories from database
            foreach (var categoryId in request.BlogCategories)
            {
                var blogCategory = await _dbConnection.QueryFirstOrDefaultAsync<BlogCategory>("SELECT * FROM BlogCategory WHERE Id = @CategoryId", new { CategoryId = categoryId }); //BlogCategories ******
                if (blogCategory == null)
                {
                    HandleException("DP-404", "Blog Category not found", "Technical");
                }
            }

            // Fetch tags from database
            var tagsList = new List<Tag>();
            foreach (var tagName in request.Tags)
            {
                var tag = await _dbConnection.QueryFirstOrDefaultAsync<Tag>("SELECT * FROM Tag WHERE Name = @TagName", new { TagName = tagName }); //Tags ********
                if (tag != null)
                {
                    tagsList.Add(tag);
                }
                else
                {
                    HandleException("DP-404", "Tag not found", "Technical");
                }
            }

            // Create new Article object
            var article = new Article
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                AuthorId = request.AuthorId,
                Summary = request.Summary,
                Body = request.Body,
                GoogleDriveID = request.GoogleDriveID,
                HideScrollSpy = request.HideScrollSpy,
                ImageId = request.ImageId,
                PDF = request.PDF,
                Langcode = request.Langcode,
                Status = request.Status,
                Sticky = request.Sticky,
                Promote = request.Promote,
                CreatorId = request.User,
                ChangedUser = request.User,
                Created = DateTime.Now,
                Changed = DateTime.Now
            };

            // Create new list of ArticleBlogCategories
            var articleBlogCategories = new List<ArticleBlogCategory>();
            foreach (var BlogcategoryId in request.BlogCategories)
            {
                var articleBlogCategory = new ArticleBlogCategory
                {
                    Id = Guid.NewGuid(),
                    ArticleId = article.Id,
                    CategoryId = BlogcategoryId
                };
                articleBlogCategories.Add(articleBlogCategory);
            }

            // Create new list of ArticleTags
            var articleTags = new List<ArticleTags>();
            foreach (var tag in tagsList)
            {
                var articleTag = new ArticleTags
                {
                    Id = Guid.NewGuid(),
                    ArticleId = article.Id,
                    TagId = tag.Id
                };
                articleTags.Add(articleTag);
            }

            // Insert data in a single SQL transaction
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    await _dbConnection.ExecuteAsync("INSERT INTO Articles VALUES (@Id, @Title, @AuthorId, @Summary, @Body, @GoogleDriveID, @HideScrollSpy, @ImageId, @PDF, @Langcode, @Status, @Sticky, @Promote, @CreatorId, @ChangedUser, @Created, @Changed)",
                        new
                        {
                            article.Id,
                            article.Title,
                            article.AuthorId,
                            article.Summary,
                            article.Body,
                            article.GoogleDriveID,
                            article.HideScrollSpy,
                            article.ImageId,
                            article.PDF,
                            article.Langcode,
                            article.Status,
                            article.Sticky,
                            article.Promote,
                            article.CreatorId,
                            article.ChangedUser,
                            article.Created,
                            article.Changed
                        }, transaction);

                    foreach (var articleBlogCategory in articleBlogCategories)
                    {
                        await _dbConnection.ExecuteAsync("INSERT INTO ArticleBlogCategories VALUES (@Id, @ArticleId, @CategoryId)",
                            new { articleBlogCategory.Id, articleBlogCategory.ArticleId, articleBlogCategory.CategoryId }, transaction);
                    }

                    foreach (var articleTag in articleTags)
                    {
                        await _dbConnection.ExecuteAsync("INSERT INTO ArticleTags VALUES (@Id, @ArticleId, @TagId)",
                            new { articleTag.Id, articleTag.ArticleId, articleTag.TagId }, transaction);
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    HandleException("DP-500", "Error occurred during article creation", "Technical");
                }
            }

            return article.Id.ToString();
        }

        public async Task<Article> GetArticle(ArticleRequestDto request) // getArtcile ******************************** PASCALCASE
        {
            // Validate request
            if (request.Id == Guid.Empty && string.IsNullOrEmpty(request.Name))
            {
                HandleException("DP-422", "Invalid request", "Business");
            }

            Article? article = null;
            if (request.Id != Guid.Empty)
            {
                article = await _dbConnection.QueryFirstOrDefaultAsync<Article>("SELECT * FROM Articles WHERE Id = @ArticleId", new { ArticleId = request.Id });
            }
            else if (!string.IsNullOrEmpty(request.Name))
            {
                article = await _dbConnection.QueryFirstOrDefaultAsync<Article>("SELECT * FROM Articles WHERE Title = @ArticleName", new { ArticleName = request.Name });
            }

            if (article == null)
            {
                HandleException("DP-404", "Article not found", "Technical");
            }

            return article;
        }

        public static void HandleException(string code, string description, string category) //**private********
        {
            if (category == "Business")
            {
                throw new BusinessException(code, description, category);
            }
            else
            {
                throw new TechnicalException(code, description, category);
            }
        }
    }

    // New list Data Types
    public class ArticleBlogCategory
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }

        // public Guid CategoryId { get; set; } *****************************
        public string? CategoryId { get; set; }
    }

    public class ArticleTags
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public Guid TagId { get; set; }
    }
}