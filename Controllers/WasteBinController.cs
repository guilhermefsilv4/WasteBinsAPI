using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WasteBinsAPI.Models;
using WasteBinsAPI.Services;
using WasteBinsAPI.ViewModel;

namespace WasteBinsAPI.Controllers
{
    [ApiVersion(1)]
    [ApiController]
    [Authorize]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class WasteBinController : ControllerBase
    {
        private readonly IWasteBinService _service;
        private readonly IMapper _mapper;

        public WasteBinController(IWasteBinService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "USER,ADMIN")]
        public ActionResult<IEnumerable<WasteBinViewModel>> Get()
        {
            var wasteBins = _service.GetAll();
            var viewModelList = _mapper.Map<IEnumerable<WasteBinViewModel>>(wasteBins);
            return Ok(viewModelList);
        }

        [HttpGet]
        [ApiVersion(2)]
        public ActionResult<IEnumerable<WasteBinPaginationViewModel>> Get([FromQuery] int referencia = 0,
            [FromQuery] int tamanho = 10)
        {
            var wasteBins = _service.GetAllReference(referencia, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<WasteBinViewModel>>(wasteBins);
            if (viewModelList.IsNullOrEmpty())
            {
                return NotFound(new { message = "No waste bins found for the given parameters." });
            }

            var viewModel = new WasteBinPaginationViewModel
            {
                WasteBins = viewModelList,
                PageSize = tamanho,
                Ref = referencia,
                NextRef = viewModelList.Last().Id
            };
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "USER,ADMIN")]
        public ActionResult<WasteBinViewModel> Get(int id)
        {
            var wasteBin = _service.GetById(id);

            if (wasteBin == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<WasteBinViewModel>(wasteBin);
            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Put(int id, WasteBinUpdateViewModel viewModel)
        {
            if (id != viewModel.Id)
                return BadRequest();

            var itemFound = _service.GetById(id);
            if (itemFound == null)
                return NotFound();

            _mapper.Map(viewModel, itemFound);
            _service.Update(itemFound);
            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Post([FromBody] WasteBinCreateViewModel viewModel)
        {
            var wasteBin = _mapper.Map<WasteBinModel>(viewModel);
            _service.Add(wasteBin);
            return CreatedAtAction(nameof(Get), new { id = wasteBin.Id }, wasteBin);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}