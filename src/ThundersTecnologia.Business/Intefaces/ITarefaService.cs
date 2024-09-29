using ThundersTecnologia.Business.Models;
using ThundersTecnologia.Business.Models;

namespace ThundersTecnologia.Business.Intefaces
{
    public interface ITarefaService : IDisposable
    {
        Task<Tarefas> Add(Tarefas tarefa);
        Task<Tarefas> Update(Tarefas tarefa);
        Task<bool> Delete(Guid id);
    }
}