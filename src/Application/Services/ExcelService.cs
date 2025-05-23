﻿using Domain.Entities;
using NPOI.XSSF.UserModel;

namespace Application.Services
{
    public static class ExcelService
    {
        public static byte[] CreateFile<T>(List<T> Source)
        {
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("Sheet1");
            var rowHeader = sheet.CreateRow(0);
            
            var properties = typeof(T).GetProperties().ToList();
            if (typeof(T) == typeof(Movement))
            {
                properties = properties.Where(p => p.Name != "Id" && p.Name != "UserIdId" && p.Name != "User").ToList();
            }

            //header
            var font = workbook.CreateFont();
            font.IsBold = true;
            var style = workbook.CreateCellStyle();
            style.SetFont(font);

            var colIndex = 0;
            foreach (var property in properties)
            {

                var cell = rowHeader.CreateCell(colIndex);
                cell.SetCellValue(property.Name);
                cell.CellStyle = style;
                colIndex++;
            }
            //end header


            //content
            var rowNum = 1;
            foreach (var item in Source)
            {
                var rowContent = sheet.CreateRow(rowNum);

                var colContentIndex = 0;
                foreach (var property in properties)
                {
                    var cellContent = rowContent.CreateCell(colContentIndex);
                    var value = property.GetValue(item, null);

                    if (value == null)
                    {
                        cellContent.SetCellValue("");
                    }
                    else if (property.PropertyType == typeof(string))
                    {
                        cellContent.SetCellValue(value.ToString());
                    }
                    else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
                    {
                        cellContent.SetCellValue(Convert.ToInt32(value));
                    }
                    else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
                    {
                        cellContent.SetCellValue(Convert.ToDouble(value));
                    }
                    else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                    {
                        var dateValue = (DateTime)value;
                        cellContent.SetCellValue(dateValue.ToString("yyyy-MM-dd"));
                    }
                    else cellContent.SetCellValue(value.ToString());

                    colContentIndex++;
                }

                rowNum++;
            }

            //end content


            var stream = new MemoryStream();
            workbook.Write(stream);
            var content = stream.ToArray();

            return content;
        }
    }
}
