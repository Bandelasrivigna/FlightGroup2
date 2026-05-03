using System.Linq.Expressions;

namespace Group2Flight.Models
{
    public class QueryOptions<T>
    {
        // public properties for sorting, filtering, and paging
        public Expression<Func<T, bool>> Where { get; set; } = null!;

        /* Code for working with Include strings */
        private string[] includes = Array.Empty<string>();

        // public write-only property for Include strings – accepts a string, converts it to
        // a string array, and stores in private string array field
        public string Includes {
            set => includes = value.Replace(" ", "").Split(',');
        }

        // public get method for Include strings - returns private string array, or
        // empty string array if private backing field is null
        public string[] GetIncludes() => includes;

        // read-only properties 
        public bool HasWhere => Where != null;

    }

}
