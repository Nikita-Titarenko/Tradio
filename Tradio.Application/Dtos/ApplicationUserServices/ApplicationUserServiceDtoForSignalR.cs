using Tradio.Application.Dtos.Messages;

namespace Tradio.Application.Dtos.ApplicationUserServices;

public class ApplicationUserServiceDtoForSignalR
{
    public string FullName { get; set; } = string.Empty;
    public MessageDtoForSingalR Message { get; set; } = default!;
}