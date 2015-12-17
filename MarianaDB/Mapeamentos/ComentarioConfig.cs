using MarianaDB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarianaDB.Mapeamentos
{
    public class ComentarioConfig : EntityTypeConfiguration<Comentario>
    {
        public ComentarioConfig()
        {
            ToTable("COMENTARIO");
            HasKey(x => x.id);

            Property(x => x.id)
                .HasColumnName("IDCOMENTARIO")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.admPost)
                .HasColumnName("ADMPOST")            
                .IsRequired();

            Property(x => x.email)
                .HasMaxLength(100)
                .HasColumnName("EMAIL");                

            Property(x => x.PaginaWEB)
                .HasMaxLength(100)
                .HasColumnName("PAGINAWEB");

            Property(x => x.Nome)
                .HasMaxLength(100)
                .HasColumnName("NOME")
                .IsRequired();

            Property(x => x.Descricao)
                .HasColumnName("DESCRICAO")
                .IsMaxLength()
                .IsRequired();

            Property(x => x.idPost)
                .HasColumnName("IDPOST")
                .IsRequired();

            Property(x => x.DataHora)
                .HasColumnName("DATAHORA")                
                .IsRequired();

            HasRequired(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.idPost);
        }
    }
}