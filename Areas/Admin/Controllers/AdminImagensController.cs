using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using site.Models;

namespace site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly static int LIMITE_DE_ARQUIVOS = 10;
        private static string IMAGES_PATH;

        private readonly ConfigurationImages _myConfig;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminImagensController(IOptions<ConfigurationImages> myConfig, IWebHostEnvironment webHostEnvironment)
        {
            _myConfig = myConfig.Value;
            _webHostEnvironment = webHostEnvironment;

            IMAGES_PATH = Path.Combine(_webHostEnvironment.WebRootPath, _myConfig.NomePastaImagensProdutos);
        }

        public IActionResult Index()
        {
            return View();
        }

        private bool IsValidFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Error : Arquivo(s) não selecionado(s)";
                return false;
            }

            if (files.Count > LIMITE_DE_ARQUIVOS)
            {
                ViewData["Erro"] = "Error : Quantidade de arquvios excedeu o limite!";
                return false;
            }
            return true;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (!IsValidFiles(files)) return View(ViewData);

            var filePathsName = new List<string>();

            foreach (var formFile in files)
            {
                if (formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".png") || formFile.FileName.Contains(".gif"))
                {
                    var fileNameWithPath = string.Concat(IMAGES_PATH, "\\", formFile.FileName);
                    filePathsName.Add(fileNameWithPath);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor, com tamanho total de: {files.Sum(f => f.Length)} bytes";
            ViewBag.Arquivos = filePathsName;

            return View(ViewData);
        }

        [HttpGet]
        public IActionResult GetImagens()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(IMAGES_PATH);
            FileInfo[] files = directoryInfo.GetFiles();

            if (files.Length == 0)
            {
                ViewData["Erro"] = $"Nenhum arquivo encontrado no servidor";
            }

            FileManagerModel model = new FileManagerModel()
            {
                PathImagesProdutos = _myConfig.NomePastaImagensProdutos,
                Files = files
            };
            return View(model);
        }

        public IActionResult DeleteFile(string fname)
        {
             string imagePath = Path.Combine(IMAGES_PATH + "\\", fname);

            if ((System.IO.File.Exists(imagePath)))
            {
                System.IO.File.Delete(imagePath);
                ViewData["Deletado"] = $"Arquivo(s) {imagePath} deletado com sucesso";
            }
            return View("index");
        }
    }
}
