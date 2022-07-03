using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatronTest.FileReading;

namespace PatronTest.Controllers
{
    [ApiController]
    [Route("")]
    public class FileReadController : ControllerBase
    {
        private readonly FileReader _fileReader;

        public FileReadController(FileReader fileReader)
        {
            _fileReader = fileReader;
        }

        [HttpGet]
        public async Task<ActionResult<string?>> GetFileAsync(string filename)
        {
            if (string.IsNullOrEmpty(filename.Trim()))
                return NotFound();
            return await _fileReader.ReadFileAsync(filename);
        }
    }
}
