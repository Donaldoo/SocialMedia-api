using SocialMedia.Api.Common.Models;

namespace SocialMedia.Api.Common.Models;

public class OrderedPagedTransactionRequestDto : PagedRequestDto
{
    public string OrderColumn { get; set; } = "ScheduledDate";
    public bool OrderDescending { get; set; } = true;
    public string SearchValue { get; set; } = null;
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}