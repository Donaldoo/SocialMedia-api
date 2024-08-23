namespace SocialMedia.Api.Common.Models;

public class OrderedPagedRequestDto : PagedRequestDto
{
    public string OrderColumn { get; set; } = "CreatedAt";
    public bool OrderDescending { get; set; } = true;
    public string SearchValue { get; set; } = null;
}