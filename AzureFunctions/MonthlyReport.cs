using Microsoft.Azure.WebJobs;
using Syncfusion.Pdf.Parsing;
using Syncfusion.XlsIO;
using Syncfusion.XlsIORenderer;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UserMicroService.Contracts;
using UserMicroService.DataTransferObjects;

namespace AzureFunctions
{
    public class MonthlyReport
    {
        private readonly IEmailService _emailService;

        public MonthlyReport(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [FunctionName("WeekReport")]
        public async Task Run([TimerTrigger("0 0 23 * * SUN")] TimerInfo myTimer)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5003/api/users/confirmedemail");
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);
            var users = await httpResponse.Content.ReadAsAsync<IEnumerable<UserForReadDto>>();

            foreach (var user in users)
            {
                HttpClient clientActs = new HttpClient();
                HttpRequestMessage httpRequestActivities = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5004/api/users/{user.UserProfileId}/activities/days/7");
                HttpResponseMessage httpResponseActivities = await clientActs.SendAsync(httpRequestActivities);
                var acts = await httpResponseActivities.Content.ReadAsAsync<IEnumerable<DayForChartDto>>();

                HttpClient clientEats = new HttpClient();
                HttpRequestMessage httpRequestEatings = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5006/api/users/{user.UserProfileId}/eatings/days/7");
                HttpResponseMessage httpResponseEatings = await clientEats.SendAsync(httpRequestEatings);
                var eats = await httpResponseEatings.Content.ReadAsAsync<IEnumerable<DayForChartDto>>();

                var list = eats.Zip(acts, (e, a) => 
                    new DayForChartDto { Day = e.Day, CurrentCalories = e.CurrentCalories - a.CurrentCalories });

                using ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;
                IWorkbook workbook = application.Workbooks.Create(1);
                
                IWorksheet sheet = workbook.Worksheets[0];

                sheet.Range["A1"].Text = "Date";
                sheet.Range["B1"].Text = "Calories";

                int i = 2;

                foreach (var item in list)
                {
                    sheet.Range[$"A{i}"].Text = $"{item.Day:MM/dd/yy}";
                    sheet.Range[$"B{i}"].Number = item.CurrentCalories;
                    i++;
                }

                IChartShape chart = sheet.Charts.Add();

                chart.ChartType = ExcelChartType.Line;
                chart.ChartTitle = "Week Report";
                IChartSerie calories = chart.Series.Add("Calories");
                calories.Values = sheet.Range["B2:B8"];
                calories.CategoryLabels = sheet.Range["A2:A8"];

                FileStream stream1 = new FileStream(@"C:\Users\user\Desktop\Chart.xlsx", FileMode.Create, FileAccess.ReadWrite);
                workbook.SaveAs(stream1);

                XlsIORenderer renderer = new XlsIORenderer();
                Syncfusion.Pdf.PdfDocument pdfDocument = renderer.ConvertToPDF(sheet);

                MemoryStream stream = new MemoryStream();
                pdfDocument.Save(stream);
                stream.Flush();
                stream.Position = 0;

                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(stream);
                loadedDocument.Pages.RemoveAt(1);
                loadedDocument.Save(stream);
                stream.Position = 0;

                await _emailService.SendEmailAsync(user.Email, "Week Report", null, stream);
            }
        }
    }
}
