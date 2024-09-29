using AutoMapper;
using ThundersTecnologia.Api.ViewModels;
using ThundersTecnologia.Business.Models;

namespace ThundersTecnologia.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Tarefas, TarefasViewModel>().ReverseMap();
        }
    }
}