using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolshopApp2.Data;
using ToolshopApp2.Model;
using ToolshopApp2.View;

namespace ToolshopApp2.Controllers
{
    public class DishwasherOnStockController
    {       
        public static void OpenAddDishwasherWindow()
        {
            var _addDishwasherWindow = new AddDishwaserWindow();
            _addDishwasherWindow.ShowDialog();
        }
        public static void OpenEditDishwasherWindow(object row)
        {
            var dishwasherToEdit = row as DishwashersOnStock;
            var _editDishwasherWindow = new EditDishwaserWindow(dishwasherToEdit);
            
            _editDishwasherWindow.Pnc.Text = dishwasherToEdit.Pnc;
            _editDishwasherWindow.SerialNumber.Text = dishwasherToEdit.SerialNumber;
            _editDishwasherWindow.ScanDate.Text = dishwasherToEdit.ScanDate.ToShortDateString();
            _editDishwasherWindow.Assigment.Text = dishwasherToEdit.Assigment;
            _editDishwasherWindow.BegingSrz.Text = dishwasherToEdit.BegingSrz;
            _editDishwasherWindow.EndingSrz.Text = dishwasherToEdit.EndingSrz;
            _editDishwasherWindow.Comment.Text = dishwasherToEdit.Comments;

            _editDishwasherWindow.ShowDialog();
        }
        public static void UpdateDishwasherOnStock(DishwashersOnStock dishwasher)
        {
            var context = new DatabaseConnectionContext();
            context.Update(dishwasher);
            context.SaveChanges();
        }
        public static void ExportTabToExcel()
        {
            var workbook = new XLWorkbook();            
            workbook.Worksheets.Add(GetTableOfDishwashersOnStock(), "DishwashersOnStock");

            var savefiledialog = new SaveFileDialog();
            savefiledialog.FileName = "DishwashersOnStockExport" + DateTime.Now.ToShortDateString().Replace(".", "").Replace("/", "").Replace("-", "").Replace("\\", "") + DateTime.Now.Hour + DateTime.Now.Minute;
            savefiledialog.DefaultExt = "xlsx";
            savefiledialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            savefiledialog.FilterIndex = 1;
            savefiledialog.RestoreDirectory = true;

            if (savefiledialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                workbook.SaveAs(savefiledialog.FileName);
            }
        }
        private static DataTable GetTableOfDishwashersOnStock()
        {
            var context = new DatabaseConnectionContext();
            var DishwashersTable = new DataTable(typeof(DishwashersOnStock).Name);
            var list = context.DishwashersOnStock.ToArray<DishwashersOnStock>();
            PropertyInfo[] properties = typeof(DishwashersOnStock).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                DishwashersTable.Columns.Add(prop.Name);
            }
            foreach (var item in list)
            {
                var values = new object[properties.Length];
                for (var i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }
                DishwashersTable.Rows.Add(values);
            }
            return DishwashersTable;
        }
        public static IEnumerable<DishwashersOnStock> GetDishwashersOnStocks()
        {
            var context = new DatabaseConnectionContext();
            return context.DishwashersOnStock.ToList<DishwashersOnStock>();
        }
        public static void CreateDishwasherOnStockModels(string barcodes, string assigment, string scanDate, string beginingSrz, string endingSrz, string comments)
        {
            var context = new DatabaseConnectionContext();
            DishwashersOnStock dishwashersOnStock;                  
            foreach (var barcode in barcodes.Split('\n'))
            {
                dishwashersOnStock = new DishwashersOnStock
                {
                    Barcode = barcode,
                    Pnc = GetPncFromBarcode(barcode),
                    Elc = GetElcFromBarcode(barcode),
                    SerialNumber = GetSerialNumberFromBarcode(barcode),
                    Assigment = assigment,
                    ScanDate = DateTime.Parse(scanDate),
                    BegingSrz = beginingSrz,
                    EndingSrz = endingSrz,
                    Comments = comments
                };
                context.Add(dishwashersOnStock);
            }
            context.SaveChangesAsync();
        }
        private static string GetSerialNumberFromBarcode(string barcode)
        {
            return barcode.Substring(11);
        }
        private static string GetElcFromBarcode(string barcode)
        {
            return barcode.Substring(1, 9);
        }
           
        private static string GetPncFromBarcode(string barcode)
        {
            string pnc = "";
            if (barcode.Substring(1, 1) == "1")
            {
                pnc = "91" + barcode.Substring(1, 7);
            }
            else if (barcode.Substring(1, 1) == "5")
            {
                pnc = "911" + barcode.Substring(1, 6);
            }
            else if (barcode.Substring(1, 1) == "9")
            {
                pnc = "99" + barcode.Substring(1, 7);
            }
            else
            {
                MessageBox.Show("Nie potrafię rozpoznać PNC w bar code: " + barcode +". Sprawdź czy jest poprawny i popraw ręczie");
            }
            return pnc;
        }
    }
}

