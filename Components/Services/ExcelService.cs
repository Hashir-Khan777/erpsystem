using OfficeOpenXml;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Services
{
    public class ExcelService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ExcelService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void ExportToExcel(List<AuditLog> data)
        {
            var excelPackage = new ExcelPackage();

            var worksheet = excelPackage.Workbook.Worksheets.Add("Data");

            for (int i = 0; i < data.Count(); i++)
            {
                worksheet.Cells[i + 1, 1].Value = data[i].EntityName;
                worksheet.Cells[i + 1, 2].Value = data[i].ActionType;
                worksheet.Cells[i + 1, 3].Value = data[i].RowNumber;
                worksheet.Cells[i + 1, 4].Value = data[i].User?.UserName;
                worksheet.Cells[i + 1, 5].Value = data[i].UserRole;
                worksheet.Cells[i + 1, 6].Value = TimeZoneInfo.ConvertTimeFromUtc(data[i].Timestamp, TimeZoneInfo.Local);
            }

            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "ExportedData.xlsx");
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            excelPackage.SaveAs(fileStream);
            fileStream.Close();
        }
    }
}
