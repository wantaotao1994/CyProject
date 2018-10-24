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
using CyProject.Schedule;
using CyProject.Model;

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

        /// <summary>
        /// 获取队列情况
        /// </summary>
        /// <returns>The task queue.</returns>
        /// <param name="id">Identifier.</param>
        public  int  GetTaskQueue(string id){
            return  TaskSchedule.GetIndex(id);

        }

        /// <summary>
        /// 二代测序
        /// </summary>
        /// <returns>The task one.</returns>
        /// <param name="files">Files.</param>
        [DisableRequestSizeLimit]
        public async Task<FastqToFasaQueryRes>  BeginTaskOne(List<IFormFile> files)
        {
            string tempBasePath =   CheckBaseTempPath();
            string inFastqPath = "";
            string iRefFaPath = "";
            if (Request.Form.Files.Count > 0)  //get  first
            {
                string fileFullPath = tempBasePath + DateTime.Now.Millisecond + Request.Form.Files[0].FileName;
                foreach (var item in files)
                {
                    using (var stream = new FileStream(fileFullPath, FileMode.Create))
                    {
                        
                        await item.CopyToAsync(stream);
                    }
                    if (item.Name.Contains(".fasq"))
                    {
                        inFastqPath = fileFullPath;
                    }
                    else if (item.Name.Contains(".fa"))
                    {
                        iRefFaPath = fileFullPath;
                    }
                }
                string guid = new Guid().ToString("N");
                HttpContext.Session.SetString("guid", guid);
                if (string.IsNullOrEmpty(inFastqPath) ||string.IsNullOrEmpty(iRefFaPath))
                {
                    return new FastqToFasaQueryRes()
                    {
                        Code = 500,
                        Message= "上传文件不正确"
                    };
                }
                TaskSchedule.Push(new KeyValuePair<string, Task2Model>(guid, new Task2Model()
                {
                    Id = guid,
                    InFastqPath = inFastqPath,
                    InRefFaPath = iRefFaPath,
                    IsComplete = false,

                }));
                return new FastqToFasaQueryRes()
                {
                    Code = 0,
                    Guid = guid
                };
            }

            return new FastqToFasaQueryRes()
            {
                Code = 500,

            };
        }

        [HttpPost]
        public async Task<IActionResult> UploadFastQFileAsync(List<IFormFile> files)
        {

            string tempBasePath =   CheckBaseTempPath();
            if (Request.Form.Files.Count > 0)  //get  first
            {
                string fileFullPath = tempBasePath + DateTime.Now.Millisecond + Request.Form.Files[0].FileName;
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

        private  string  CheckBaseTempPath(){
            var filePath = @"AppData/temp";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            return filePath;
        }
    }
}
