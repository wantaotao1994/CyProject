using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CyProject.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using CommanderHelper;
using CyProject.Model.JsonModel;

namespace CyProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }




        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> GetExcuteTask()
        {
            string id = HttpContext.Session.GetString("guid");
            if (string.IsNullOrEmpty(id))
            {

                bool check = CommandTaskQueue.CommandExcuteSet.ContainsKey(id);
                if (check)
                {
                    var command = CommandTaskQueue.CommandExcuteSet[id];
                    if (command.IsCommplete) //complete
                    {
                        string outPath = command.OutFilePath;

                        FileStream fileStream = new FileStream(outPath, FileMode.Open);

                        return File(fileStream, "text/comma-separated-values");
                    }
                    else
                    {
                        return Json(new GetExcuteTaskRes() {
                            Status = 1
                        });
                    }
                }

            }

            return Json(new GetExcuteTaskRes()
            {
                Status = 2,
                Message="错误 没有找到您的任务"

            });
        }


        [HttpPost]
        public async Task<IActionResult> UploadFastQFileAsync(List<IFormFile> files)
        {
            var filePath = @"AppData/temp";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (Request.Form.Files.Count > 0)  //get  first
            {
                string fileFullPath = filePath + DateTime.Now.Millisecond + Request.Form.Files[0].FileName;
                using (var stream = new FileStream(fileFullPath, FileMode.Create))
                {
                    await Request.Form.Files[0].CopyToAsync(stream);
                }

                string guid = CommandMap.FastqToFasta(fileFullPath, fileFullPath + ".out.fasta");
                HttpContext.Session.SetString("guid", guid);
                return Json(new FastqToFasaQueryRes()
                {
                    Code = 0,
                    Guid = guid
                });

            }

            return Json(new FastqToFasaQueryRes()
            {
                Code = 500,

            });
        }


        public IActionResult WaitResult(string id)
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
