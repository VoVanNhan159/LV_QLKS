using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using ShareModel;
using ShareModel.Custom;
using ShareModel.Paging;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using Radzen;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace LV_QLKS.Service
{
    public class RoomService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Rooms";
        JsonSerializerOptions _options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public async Task<List<Room>> GetAllRoom()
        {
            return await Http.GetFromJsonAsync<List<Room>>(baseurl);
        }
        public async Task<Room> GetRoom(int id)
        {
            return await Http.GetFromJsonAsync<Room>(baseurl + "/" + id);
        }
        public async Task<List<Room>> GetAllRoomOfOwner(string phone)
        {
            return await Http.GetFromJsonAsync<List<Room>>(baseurl + "/GetAllRoomOfOwner/" + phone);
        }
        public async Task<List<Room>> GetAllRoomOfHotel(int id)
        {
            var rooms = await Http.GetFromJsonAsync<List<Room>>(baseurl + "/GetAllRoomOfHotel/" + id);
            return rooms;
        }
        public double GetRateOfRoom(int id)
        {
            string starTemp = Http.GetStringAsync(baseurl + "/RateOfRoom/" + id).Result.ToString();
            double res = double.Parse(starTemp);
            return res;
        }
        //Thêm phòng
        public async Task<Room_Custom> AddRoom(Room_Custom room)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", room);
            return await response.Content.ReadFromJsonAsync<Room_Custom>();
        }
        public async Task<List<Room>> GetListRoomFilter(int hotelId, DateTime dayStart, DateTime dayEnd, int capacity)
        {
            return await Http.GetFromJsonAsync<List<Room>>(baseurl + "/GetListRoomFilter?hotelId=" + hotelId + "&dayStart=" + dayStart + "&dayEnd=" + dayEnd + "&capacity=" + capacity);
        }

        public async Task<Room_Custom> UpdateRoom(Room_Custom room)
        {
            var res = await Http.PutAsJsonAsync(baseurl + "/" + room.RoomId, room);
            if (res != null)
            {
                return await res.Content.ReadFromJsonAsync<Room_Custom>();
            }
            return null;
        }
        //Export
        public async Task<byte[]> ExportRoomToExcel(string phone)
        {
            var rooms = new List<Room>();
            try
            {
                rooms = await GetAllRoomOfOwner(phone);
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
            }
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");

                // simple way
                //workSheet.Cells.LoadFromCollection(rooms, true);

                // mutual
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells[1, 1].Value = "Mã phòng";
                workSheet.Cells[1, 2].Value = "Mã tầng";
                workSheet.Cells[1, 3].Value = "Mã loại phòng";
                workSheet.Cells[1, 4].Value = "Mã khách sạn";
                workSheet.Cells[1, 5].Value = "Tên phòng";
                workSheet.Cells[1, 6].Value = "Mô tả";
                workSheet.Cells[1, 7].Value = "Trạng thái (TRUE: đang sử dụng, FALSE: Tạm ngưng sử dụng)";

                int recordIndex = 2;
                foreach (var item in rooms)
                {
                    workSheet.Cells[recordIndex, 1].Value = item.RoomId;
                    workSheet.Cells[recordIndex, 2].Value = item.FloorId;
                    workSheet.Cells[recordIndex, 3].Value = item.TorId;
                    workSheet.Cells[recordIndex, 4].Value = item.HotelId;
                    workSheet.Cells[recordIndex, 5].Value = item.RoomName;
                    workSheet.Cells[recordIndex, 6].Value = item.RoomDescription;
                    workSheet.Cells[recordIndex, 7].Value = item.RoomStatus;
                    recordIndex++;
                }

                return package.GetAsByteArray();
            }
        }
    }
}
