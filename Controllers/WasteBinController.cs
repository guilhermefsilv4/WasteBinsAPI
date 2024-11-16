using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WasteBinsAPI.Models;
using WasteBinsAPI.Services;

namespace WasteBinsAPI.Controllers
{
    [ApiVersion(1)]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class WasteBinController : ControllerBase
    {
        private readonly IWasteBinService _service;

        public WasteBinController(IWasteBinService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WasteBinModel>> Get()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<WasteBinModel> Get(int id)
        {
            var wasteBin = _service.GetById(id);

            if (wasteBin == null)
            {
                return NotFound();
            }

            return Ok(wasteBin);
        }
        
        [HttpPut("{id}")]
        public ActionResult Put(int id, WasteBinModel wasteBinModel)
        {
            var itemFound = _service.GetById(id);
            if (itemFound == null)
                return NotFound();
            
            _service.Update(itemFound);
            return NoContent();
        }

        [HttpPost]
        public ActionResult Post([FromBody] WasteBinModel viewModel)
        {
            _service.Add(viewModel);
            return CreatedAtAction(nameof(Get), new { id = viewModel.Id }, viewModel);
        }
        
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}