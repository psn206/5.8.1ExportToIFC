using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportToIFC
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Файл IFC (*.ifc)|*.ifc";

            if (saveFileDialog.ShowDialog() == true)
            {
                IFCExportOptions options = new IFCExportOptions();

                using (var ts = new Transaction(doc, "Export to IFC"))
                {
                    ts.Start();
                    doc.Export(Path.GetDirectoryName(saveFileDialog.FileName), saveFileDialog.SafeFileName, options);
                    ts.Commit();
                }
            }

            TaskDialog.Show("Сообщение", "Файл записан!");
            return Result.Succeeded;
        }
    }
}
