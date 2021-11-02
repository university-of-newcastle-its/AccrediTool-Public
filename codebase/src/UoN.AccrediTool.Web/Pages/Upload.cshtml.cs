using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System.IO.Compression;



namespace UoN.AccrediTool.Web.Pages
{
    public class UploadModel : PageModel
    {
        [BindProperty]
        public IFormFile fileUpload {get; set;}

        private ZipArchive zip;

        private String errorString = "";


        public async Task OnPostAsync()
        {
            if(fileUpload !=null && fileUpload.ContentType.Equals("application/x-zip-compressed"))
            {
                zip = new ZipArchive(fileUpload.OpenReadStream());
                proccessZip();
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

        //proccess the uploaded zips into the api.
        private void proccessZip()
        {

        }
    }
}
