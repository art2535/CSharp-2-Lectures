using ShopClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml.Serialization;
using System.IO;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace ShopClassLibrary_C224
{
    public class Stock : List<Product>
    {
        public List<Product> FilterCategory(string category)
        {
            var selectProduct = this.Where(p => p.Category == (Categories)Enum.Parse(typeof(Categories), category))
                .OrderBy(p => p.Name)
                .Select(p => p);
            return selectProduct.ToList();
        }

        public void LoadFromJson(string fileName)
        {
            this.AddRange(JsonSerializer.Deserialize<Stock>(File.ReadAllText(fileName)));
        }

        public void LoadFromXmlSer(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Stock));
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                this.AddRange(serializer.Deserialize(fileStream) as Stock);
            }
        }

        public void SaveToExcel(string fileName)
        {
            // Создаем новую книгу Excel
            var application = new Excel.Application();
            // Указываем количество листов равным 2
            application.SheetsInNewWorkbook = 2;
            // Добавляем рабочую книгу
            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
            // Называем листы
            application.Worksheets.Item[1].Name = "Список товара";
            application.Worksheets.Item[2].Name = "Статистика";
            // Выбираем рабочий лист - первый
            // worksheet - ссылка на application.Worksheets.Item[1] (первый лист книги)
            Excel.Worksheet worksheet = application.Worksheets.Item[1];
            // Вставляем заголовок вверху в объединенных ячейках
            // Координаты можно указать [][] или [,]
            // headerRange.Merge() - объедиенение ячеек
            Excel.Range headerRange = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[1, 5]];
            headerRange.Merge();
            headerRange.Value = "Состояние склада на " + DateTime.Today.ToString("dd.MM.yyyy");
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.Font.Size = 14;
            headerRange.Font.Italic = true;
            // Добавляем названия колонок
            // при обращении к ячейке сначала указывается номер ее столбца, а затем - номер строки
            worksheet.Cells[3, 1] = "Название товара";
            worksheet.Cells[3, 2] = "Категория";
            worksheet.Cells[3, 3] = "Цена";
            worksheet.Cells[3, 4] = "Наличие";
            worksheet.Cells[3, 5] = "Суммарная стоимость";
            // Добавляем данные по товарам
            int indexRow = 4; // начальная строка для вставки данных
            foreach (Product product in DataBaseProduct.Stock)
            {
                worksheet.Cells[indexRow, 1] = product.Name;
                worksheet.Cells[indexRow, 2] = product.Category;
                worksheet.Cells[indexRow, 3] = product.Price / 100;
                worksheet.Cells[indexRow, 4] = product.Count;
                worksheet.Cells[indexRow, 5].Formula = $"=C{indexRow}*D{indexRow}/100";
                worksheet.Cells[indexRow, 5].Select();
                // worksheet.Cells[indexRow][5].NumberFormat = "#,###.00";

                // System.Globalization.CultureInfo - региональные параметры Windows при запуске приложения
                // NumberFormat.CurrencyDecimalSeparator - разделитель десятичной точки (точка, запятая)
                // в зависимости от установленных настроек Windows
                worksheet.Cells[indexRow][5].NumberFormat =
                    "0" + System.Globalization.CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator + "00";
                indexRow++;
            }
            // Добавляем название к ячейкам для хранения ИТОГО
            Excel.Range sumRange = worksheet.Range[worksheet.Cells[indexRow, 1], worksheet.Cells[indexRow, 4]];
            sumRange.Merge();
            sumRange.Value = "ИТОГО";
            sumRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            sumRange.Font.Bold = true;
            // Рассчитываем сумму ИТОГО
            worksheet.Cells[indexRow, 5].Formula = $"=SUM(E4:E{indexRow - 1}";
            worksheet.Cells[indexRow, 5].NumberFormat =
                    "0" + System.Globalization.CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator + "00";

            // worksheet.Cells[indexRow][5].NumberFormat = "#,###.00";
            // Завершаем оформление таблицы
            // дабавление границ (внешних и внутренних)
            Excel.Range rangeBolder = worksheet.Range[worksheet.Cells[3, 1], worksheet.Cells[indexRow, 5]];
            rangeBolder.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                rangeBolder.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                rangeBolder.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                rangeBolder.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                rangeBolder.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                rangeBolder.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle =
                Excel.XlLineStyle.xlContinuous;
            // установка автоширины всех столбцов листа
            worksheet.Columns.AutoFit();
            // отобразить таблицу по завершении экспорта
            application.Visible = true;
            workbook.SaveAs(fileName);
        }

        public void SaveToJson(string fileName)
        {
            var options = new JsonSerializerOptions
            {
                IgnoreReadOnlyProperties = true,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };
            File.WriteAllText(fileName, JsonSerializer.Serialize(this));
        }

        public void SaveToXmlSer(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Stock));
            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fileStream, this);
            }
        }
    }
}
