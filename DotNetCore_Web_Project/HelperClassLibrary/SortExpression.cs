using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace HelperClassLibrary
{
    public class SortExpression<T> where T : class
    {
        public SortExpression(Expression<Func<T, object>> sortBy, ListSortDirection sortDirection)
        {
            SortBy = sortBy;
            SortDirection = sortDirection;
        }

        public Expression<Func<T, object>> SortBy { get; set; }
        public ListSortDirection SortDirection { get; set; }
    }
}