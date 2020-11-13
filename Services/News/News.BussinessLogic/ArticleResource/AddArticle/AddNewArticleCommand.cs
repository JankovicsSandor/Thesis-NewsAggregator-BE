using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace News.BussinessLogic.ArticleResource.AddArticle
{
    public class AddNewArticleCommand : IRequest
    {
        [Required]
        public int FeedId { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 5, ErrorMessage = "Title must have at least 5 character")]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public string Link { get; set; }
        public DateTime PublishDate { get; set; }
        public string Picture { get; set; }
    }
}
