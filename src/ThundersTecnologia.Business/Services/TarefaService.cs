using ThundersTecnologia.Business.Intefaces;
using ThundersTecnologia.Business.Models;

namespace ThundersTecnologia.Business.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<Tarefas> Add(Tarefas tarefas)
        {
            return await _tarefaRepository.Add(tarefas);

        }

        public async Task<Tarefas> Update(Tarefas tarefas)
        {
            return await _tarefaRepository.Update(tarefas);
        }

        public async Task<bool> Delete(Guid id)
        {
            await _tarefaRepository.Delete(id);
            return true;
        }

        public void Dispose()
        {
            _tarefaRepository?.Dispose();
        }
    }
}
