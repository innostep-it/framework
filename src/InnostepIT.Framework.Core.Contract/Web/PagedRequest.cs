using System.ComponentModel.DataAnnotations;

namespace InnostepIT.Framework.Core.Contract.Web;

public class PagedRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public int Page { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int PageSize { get; set; }

    public string? SortColumnName { get; set; }
    public bool SortDirectionDesc { get; set; }
}