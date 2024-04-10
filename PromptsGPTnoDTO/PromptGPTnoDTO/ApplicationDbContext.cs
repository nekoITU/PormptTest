using Microsoft.EntityFrameworkCore;
using ProjectName.Types;

namespace ProjectName.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }




        public DbSet<Article>? Article { get; set; }
        public DbSet<Author>? Author { get; set; }

        public DbSet<BlogCategory>? BlogCategory { get; set; }

        public DbSet<Tag>? Tag { get; set; }

        public DbSet<ArticleTags>? ArticleTags { get; set; }

        public DbSet<ArticleBlogCategory>? ArticleBlogCategory { get; set; }


        // Constructor to pass options to base class
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //    : base(options)
        //{
        //}

        //// Request method
        //public Response<T>? HandleRequest<T>(Request<T> request)
        //{
        //    // Handle request logic here
        //    // For demonstration purposes, let's just return an empty response
        //    return new Response<T>
        //    {
        //        Payload = default(T), // You may modify this according to your logic
        //        Exception = null
        //    };
        //}


        //// Response method
        //public void HandleResponse<T>(Response<T> response)
        //{
        //    // Handle response logic here
        //    // For demonstration purposes, let's just log the response
        //    Console.WriteLine($"Payload: {response.Payload}");
        //    if (response.Exception != null)
        //    {
        //        Console.WriteLine($"Exception: {response.Exception.Description}");
        //    }

        //}

    }
}
