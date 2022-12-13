using System;
using System.Collections.Generic;
using System.Drawing;
using FastExcel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace EvomatixTester.DataSource
{
    public class ExcelManager
    {
        private string sheet;
        private string filePath;
        private bool open;

        public ExcelManager()
        {
        }

     

        public void openWorkBook(String filePath, String sheet)
        {
            try
            {
                this.sheet = sheet;
                this.filePath = filePath;

                open = true;

            }
            catch (Exception e)
            {
                throw new Exception("Error Occurred while reading Excel Workbook", e);
            }
        }


        public List<Dictionary<string, object>> readExcelWithHeaders()
        {


            var inputFile = new FileInfo(filePath);

            //Create a worksheet
            Worksheet worksheet = null;

            Dictionary<int, String> headers = new Dictionary<int, string>();

            List<Dictionary<String, Object>> data = new List<Dictionary<string, object>>();

            

            // Create an instance of Fast Excel
            using (FastExcel.FastExcel fastExcel = new FastExcel.FastExcel(inputFile, true))
            {
                // Read the rows using worksheet name
                worksheet = fastExcel.Read(sheet);
                Row[] rows = worksheet.Rows.ToArray();

                Console.WriteLine(rows);
                int index = 0;
                foreach (Row row in rows){
                    if (index == 0)
                    {

                        foreach (Cell cell in row.Cells)
                        {
                            headers.Add(cell.ColumnNumber, (string)cell.Value);
                        }

                    }
                    else
                    {
                        Dictionary<String, Object> rowData = new Dictionary<string, object>();

                        foreach (Cell cell in row.Cells)
                        {
                   
                            if (headers.ContainsKey(cell.ColumnNumber) && !rowData.ContainsKey(headers[cell.ColumnNumber]))
                            {
                                rowData.Add(headers[cell.ColumnNumber], cell.Value);
                            }

                        
                        }

                        data.Add(rowData);

                    }

                    index++;

                }

                return data;


            }

        }


        public List<Dictionary<int, object>> readExcelWithOutHeaders()
        {

            var inputFile = new FileInfo(filePath);

            //Create a worksheet
            Worksheet worksheet = null;


            List<Dictionary<int, Object>> data = new List<Dictionary<int, object>>();



            // Create an instance of Fast Excel
            using (FastExcel.FastExcel fastExcel = new FastExcel.FastExcel(inputFile, true))
            {
                // Read the rows using worksheet name
                worksheet = fastExcel.Read(sheet);
                Row[] rows = worksheet.Rows.ToArray();

                Console.WriteLine(rows);
                foreach (Row row in rows)
                {
                    Dictionary<int, Object> rowData = new Dictionary<int, object>();
                    foreach (Cell cell in row.Cells)
                    {
                        rowData.Add(cell.ColumnNumber,cell.Value);
                    }
                    data.Add(rowData);
                }
            }


            return data;
        }

    }


}

