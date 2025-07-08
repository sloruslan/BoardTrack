using Microsoft.AspNetCore.Mvc;

namespace Application.DTO.Pagination;

public class PageModel
{
    /// <summary>
    /// Максимальное количество возвращаемых записей. Если значение превышает максимальное, то будет использоваться максимальное значение. По умолчанию = 100
    /// </summary>
    /// <example>100</example>
    [BindProperty(Name = "limit")]
    public int Limit { get; set; } = 100;

    /// <summary>
    /// Смещение (отсчитываемое от 0) первого элемента, возвращенного в коллекции. По умолчанию = 0
    /// </summary>
    [BindProperty(Name = "offset")]
    public int Offset { get; set; } = 0;
}
