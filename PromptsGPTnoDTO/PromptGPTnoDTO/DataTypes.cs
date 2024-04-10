// File: Request.cs
namespace ProjectName.Types
{
    public class Request<T>
    {
        public RequestHeader Header { get; set; }
        public T Payload { get; set; }
    }
}

// File: RequestHeader.cs
namespace ProjectName.Types
{
    public class RequestHeader
    {
        public Guid Id { get; set; } //string *******
        public Guid Application { get; set; } //string *******
        public string Bank { get; set; }
    }
}

// File: Response.cs
namespace ProjectName.Types
{
    public class Response<T>
    {
        public T? Payload { get; set; }
        public ResponseException Exception { get; set; }
    }
}

// File: ResponseException.cs
namespace ProjectName.Types
{
    public class ResponseException : Exception
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}

// File: CreateArticleDto.cs
namespace ProjectName.Types
{
    public class CreateArticleDto
    {
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public string GoogleDriveID { get; set; }
        public bool HideScrollSpy { get; set; }
        public Guid ImageId { get; set; }
        public Guid PDF { get; set; }
        public string Langcode { get; set; }
        public bool Status { get; set; }
        public bool Sticky { get; set; }
        public bool Promote { get; set; }
        public Guid User { get; set; }
        public List<string> BlogCategories { get; set; }
        public List<string> Tags { get; set; }
    }
}

// File: Article.cs
namespace ProjectName.Types
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public string GoogleDriveID { get; set; }
        public bool HideScrollSpy { get; set; }
        public Guid ImageId { get; set; }
        public Guid PDF { get; set; }
        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }
        public string Langcode { get; set; }
        public bool Status { get; set; }
        public bool Sticky { get; set; }
        public bool Promote { get; set; }
        public Guid CreatorId { get; set; }
        public Guid ChangedUser { get; set; }
        public List<BlogCategory> BlogCategories { get; set; }
        public List<Tag> Tags { get; set; }
    }
}

// File: ArticleRequestDto.cs
namespace ProjectName.Types
{
    public class ArticleRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

// File: BlogCategory.cs
namespace ProjectName.Types
{
    public class BlogCategory
    {
        public Guid Id { get; set; }
        public Guid Parent { get; set; }
        public string Name { get; set; }
    }
}

// File: Author.cs
namespace ProjectName.Types
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
    }
}

// File: Tag.cs
namespace ProjectName.Types
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

