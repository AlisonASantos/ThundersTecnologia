using AutoMapper;
using ThundersTecnologia.Api.ViewModels;
using ThundersTecnologia.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;
using ThundersTecnologia.Business.Models;

namespace ThundersTecnologia.API.Controllers
{
    [Route("api/tarefas")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly ITarefaService _tarefaService;
        private readonly IMapper _mapper;

        public TarefaController(ITarefaRepository tarefaRepository, ITarefaService tarefaService, IMapper mapper)
        {
            _tarefaRepository = tarefaRepository;
            _tarefaService = tarefaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TarefasViewModel>>> GetAll()
        {
            var result = _mapper.Map<List<TarefasViewModel>>(await _tarefaRepository.GetAll());

            if (result == null || !result.Any())
            {
                return Ok(new List<TarefasViewModel>());
            }

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TarefasViewModel>> GetId(Guid id)
        {
            var result = _mapper.Map<TarefasViewModel>(await _tarefaRepository.GetId(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TarefasViewModel>> Add(TarefasViewModel tarefasViewModel)
        {

            if (tarefasViewModel == null)
                return NotFound();

            var result = await _tarefaService.Add(_mapper.Map<Tarefas>(tarefasViewModel));

            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<TarefasViewModel>(result));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TarefasViewModel>> Update(TarefasViewModel tarefasViewModel)
        {
            var result = await _tarefaService.Update(_mapper.Map<Tarefas>(tarefasViewModel));

            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<TarefasViewModel>(result));

        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {

            var result = await _tarefaService.Delete(id);

            if (result == false)
                return NotFound();

            return Ok(result);
        }
    }
}
