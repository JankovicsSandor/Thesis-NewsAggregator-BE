using News.API.BussinessLogic.GetArticle.Command;
using News.DataAccess.Database;
using NewsAggregator.Shared.Predicate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace News.API.BussinessLogic.GetArticle.PredicateBuild
{
    public class ArticlePredicateBuilder
    {
        private Expression<Func<Article, bool>> predicate;

        public ArticlePredicateBuilder()
        {
            predicate = PredicateBuilder.True<Article>();
        }

        public Expression<Func<Article, bool>> AddMinDate(DateTime minDate)
        {
            predicate = predicate.And(e => e.PublishDate >= minDate);
            return predicate;
        }

        public Expression<Func<Article, bool>> AddMaxDate(DateTime maxDate)
        {
            predicate = predicate.And(e => e.PublishDate <= maxDate);
            return predicate;
        }

        public Expression<Func<Article, bool>> AddContains(string containsString)
        {
            predicate = predicate.And(e => e.Title.Contains(containsString) || e.Description.Contains(containsString));
            return predicate;
        }

        public Expression<Func<Article, bool>> Build()
        {
            return predicate;
        }


        public class ArticlePredicateQueryBuilder
        {
            public static Expression<Func<Article, bool>> GetArticleQuery(GetArticleCommand model)
            {
                var config = new ArticlePredicateBuilder();
                if (!string.IsNullOrEmpty(model.Contains))
                {
                    config.AddContains(model.Contains);
                }
                if (model.MaxDate != DateTime.MinValue)
                {
                    config.AddMaxDate(model.MaxDate);
                }
                if (model.MinDate != DateTime.MinValue)
                {
                    config.AddMinDate(model.MaxDate);
                }

                return config.Build();
            }
        }
    }
}
