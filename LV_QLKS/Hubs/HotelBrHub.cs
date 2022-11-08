using Microsoft.AspNetCore.SignalR;

namespace LV_QLKS.Hubs
{
    public class HotelBrHub :Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("ReceiveMessage");
        }
    }
}
