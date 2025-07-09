using Application.DTO.Pagination;
using Domain.Extensions;

namespace Application.Extensions;

public static class SortExtensions
{
    public static bool CheckParameters(this SortModel? sort)
    {
        if (sort is null)
        {
            return false;
        }

        if (string.IsNullOrEmpty(sort.OrderBy.Trim()))
        {
            throw new InvalidOperationException("Sort.OrderBy value is missed");
        }

        if (sort.Ordering.ToLower() != "asc" && sort.Ordering.ToLower() != "desc")
        {
            throw new InvalidOperationException("Sort.Ordering value can be asc or desc only");
        }

        return true;
    }
}
