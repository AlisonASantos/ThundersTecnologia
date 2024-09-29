using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ThundersTecnologia.Api.Configuration;
using ThundersTecnologia.Api.ViewModels;
using ThundersTecnologia.API.Controllers;
using ThundersTecnologia.Business.Intefaces;
using ThundersTecnologia.Business.Models;

namespace ThundersTecnologia.Test.Controllers
{
    // Tests/TarefaControllerTests.cs
    public class TarefaControllerTests
    {
        private readonly Mock<ITarefaRepository> _mockRepo;
        private readonly Mock<ITarefaService> _mockServ;
        private readonly Mock<IMapper> _mockMap;
        private readonly TarefaController _controller;

        public TarefaControllerTests()
        {
            _mockRepo = new Mock<ITarefaRepository>();
            _mockServ = new Mock<ITarefaService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutomapperConfig>();
            });

            var mapper = config.CreateMapper();
            _controller = new TarefaController(_mockRepo.Object, _mockServ.Object, mapper);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WhenTarefasExistem()
        {
            // Arrange
            var tarefas = new List<Tarefas>
            {
                new Tarefas { Id = Guid.NewGuid(), Tarefa = "Tarefa 1" },
                new Tarefas { Id = Guid.NewGuid(), Tarefa = "Tarefa 2" }
            };

            _mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(tarefas);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTarefas = Assert.IsType<List<TarefasViewModel>>(okResult.Value);
            Assert.Equal(tarefas.Count, returnedTarefas.Count);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WhenTarefaExists()
        {
            // Arrange
            var id = Guid.NewGuid();

            var tarefa = new Tarefas { Id = id, Tarefa = "Test Tarefa" };
            _mockRepo.Setup(repo => repo.GetId(id)).ReturnsAsync(tarefa);

            // Act
            var result = await _controller.GetId(tarefa.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTarefa = Assert.IsType<TarefasViewModel>(okResult.Value);
            Assert.Equal(tarefa.Id, returnedTarefa.Id);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenTarefaDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.GetId(id)).ReturnsAsync((Tarefas)null);

            // Act
            var result = await _controller.GetId(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Post_ReturnsCreatedResult_WhenTarefaIsValid()
        {
            // Arrange
            var id = Guid.NewGuid();
            TarefasViewModel tarefaViewModel = new TarefasViewModel();

            var tarefa = new Tarefas { Id = id, Tarefa = "New Tarefa" };
            _mockRepo.Setup(repo => repo.Add(tarefa)).ReturnsAsync(tarefa);
            _mockServ.Setup(service => service.Add(It.IsAny<Tarefas>())).ReturnsAsync(tarefa);

            // Act

            tarefaViewModel.Id = tarefa.Id;
            tarefaViewModel.Tarefa = tarefa.Tarefa;
            var result = await _controller.Add(tarefaViewModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTarefa = Assert.IsType<TarefasViewModel>(okResult.Value);
            Assert.Equal(tarefa.Id, returnedTarefa.Id);
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenTarefaIsUpdated()
        {
            // Arrange
            var id = Guid.NewGuid();
            TarefasViewModel tarefaViewModel = new TarefasViewModel();

            var tarefa = new Tarefas { Id = id, Tarefa = "Updated Tarefa" };
            _mockRepo.Setup(repo => repo.Update(tarefa)).ReturnsAsync(tarefa);
            _mockServ.Setup(service => service.Update(It.IsAny<Tarefas>())).ReturnsAsync(tarefa);

            // Act
            tarefaViewModel.Id = tarefa.Id;
            tarefaViewModel.Tarefa = tarefa.Tarefa;
            var result = await _controller.Update(tarefaViewModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTarefa = Assert.IsType<TarefasViewModel>(okResult.Value);
            Assert.Equal(tarefa.Id, returnedTarefa.Id);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenTarefaIsDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.Delete(id)).Returns(Task.CompletedTask);
            _mockServ.Setup(service => service.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.True((bool)okResult.Value);
        }
    }

}
