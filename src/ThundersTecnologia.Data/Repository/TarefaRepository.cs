using ThundersTecnologia.Business.Intefaces;
using ThundersTecnologia.Data.Context;
using ThundersTecnologia.Business.Models;

namespace ThundersTecnologia.Data.Repository
{
    public class TarefaRepository : Repository<Tarefas>, ITarefaRepository
    {
        public TarefaRepository(MeuDbContext context) : base(context)
        {
        }
    }
}
