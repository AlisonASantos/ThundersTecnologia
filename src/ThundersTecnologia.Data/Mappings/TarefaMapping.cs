using ThundersTecnologia.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThundersTecnologia.Business.Models;

namespace ThundersTecnologia.Data.Mappings
{
    public class TarefaMapping : IEntityTypeConfiguration<Tarefas>
    {
        public void Configure(EntityTypeBuilder<Tarefas> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Tarefa)
                .IsRequired()
                .HasColumnType("varchar(200)");


            builder.ToTable("Tarefas");
        }
    }
}