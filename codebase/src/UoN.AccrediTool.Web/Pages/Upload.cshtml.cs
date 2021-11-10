using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Web.Pages
{
    public class UploadModel : PageModel
    {
        [BindProperty]
        public IFormFile fileUpload {get; set;}

        private ZipArchive zip;

        private String errorString = "";

        private Boolean uploaded = false;

        private String zipRoot = "filestructure/"; 

        //=== vars from zip file===
        public String _ProjectName {get; set;}
        public String _ProjectDescription {get; set;}
        public UoProgramModel _Program;


        public async Task OnPostAsync()
        {
            if(fileUpload !=null && fileUpload.ContentType.Equals("application/x-zip-compressed"))
            {
                zip = new ZipArchive(fileUpload.OpenReadStream());
                proccessZip();
                uploaded = true;
            }
            else    
            {
                errorString = "Please upload a .zip file";
            }
                
        }


        //returns a string containing any errors that might have occured
        public String getErrorString()
        {
            return errorString;
        }

        public Boolean hasUpload()
        {
            return uploaded;
        }

        //proccess the uploaded zips into the api.
        private void proccessZip()
        {

            //===Get ProjectData===

            ZipArchiveEntry _ProjectData = zip.GetEntry(zipRoot + "ProjectData.xlsx"); // open ProjectData.xlsx



            

            SpreadsheetDocument _ProjectDataXLSX = getSpreadSheetFromZip(_ProjectData);


            // get data from ProjectData
            _ProjectName = getCellValue(_ProjectDataXLSX, "ProjectData", "A2");

            _ProjectDescription = getCellValue(_ProjectDataXLSX, "ProjectData", "B2");

            _ProjectDataXLSX.Close();

            //===Get ProgramData===

            _Program = new UoProgramModel();

            ZipArchiveEntry _ProgramData = zip.GetEntry(zipRoot + "Programs/ProgramData.xlsx"); 

            SpreadsheetDocument _ProgramDataXLSX = getSpreadSheetFromZip(_ProgramData);

            //TODO: getCellValue cannot handle anything but INT, this needs to be fixed

            //_Program.ProgramCode = int.Parse(getCellValue(_ProgramDataXLSX, "ProgramInfo", "A2"));      // set program code
            _Program.Name = getCellValue(_ProgramDataXLSX, "ProgramInfo", "B2");                        // set program name
            //_Program.FirstTermCode = int.Parse(getCellValue(_ProgramDataXLSX, "ProgramInfo", "C2"));    // set firstTermCode
            //_Program.MinUnits = int.Parse(getCellValue(_ProgramDataXLSX, "ProgramInfo", "D2"));         // set minUnits
            //_Program.Duration = int.Parse(getCellValue(_ProgramDataXLSX, "ProgramInfo", "E2"));         // set duration
            //_Program.MaxYears = int.Parse(getCellValue(_ProgramDataXLSX, "ProgramInfo", "F2"));         // set maxYears

            
        }


        private SpreadsheetDocument getSpreadSheetFromZip(ZipArchiveEntry zipEntry)
        {
            //Convert the DeflateStream into a memory stream, the stream given by zip.GetEntry does not have write permisions therfore it needs to be changed


            MemoryStream ms = new MemoryStream();

            zipEntry.Open().CopyTo(ms);

            SpreadsheetDocument _XLSX = SpreadsheetDocument.Open(ms, true);

            return _XLSX;
        }

        private string getCellValue(SpreadsheetDocument excel, string sheetName, string cellLocation)
        {
            //https://docs.microsoft.com/en-us/office/open-xml/how-to-retrieve-the-values-of-cells-in-a-spreadsheet for more info on this


            Sheet sheet = excel.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName).FirstOrDefault(); // get the sheet from the excel document

            WorksheetPart sheetPart = (WorksheetPart)(excel.WorkbookPart.GetPartById(sheet.Id)); // get the sheet part

            Cell cell = sheetPart.Worksheet.Descendants<Cell>().Where(c => c.CellReference == cellLocation).FirstOrDefault(); // get the cell from the sheet part

            // this statment is very temp, only handles strings
            return excel.WorkbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault().SharedStringTable.ElementAt(int.Parse(cell.InnerText)).InnerText; // return the text stored in the cell
        }
    }
}
