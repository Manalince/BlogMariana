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
    public class PostConfig : EntityTypeConfiguration<Post>
    {
        public PostConfig()
        {
            ToTable("POST");
            HasKey(x => x.id);

            Property(x => x.id)
                .HasColumnName("IDPOST")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Autor)
                .HasColumnName("AUTOR")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.dataPublicacao)
                .HasColumnName("DATAPUBLICACAO")
                .IsRequired();            

            Property(x => x.Descricao)
                .HasColumnName("DESCRICAO")
                .IsRequired();

            Property(x => x.Resumo)
                .HasMaxLength(1000)
                .HasColumnName("RESUMO")
                .IsRequired();

            Property(x => x.Titulo)
                .HasColumnName("TITULO")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.visivel)
                .HasColumnName("VISIVEL")
                .IsRequired();

            HasMany(x => x.Arquivos)
                .WithOptional()
                .HasForeignKey(x => x.idPost);

            HasMany(x => x.Comentarios)
                .WithOptional()
                .HasForeignKey(x => x.idPost);

            HasMany(x => x.Imagens)
                .WithOptional()
                .HasForeignKey(x => x.idPost);

            HasMany(x => x.Visitas)
                .WithOptional()
                .HasForeignKey(x => x.idPost);

            HasMany(x => x.TagPosts)
                .WithOptional()
                .HasForeignKey(x => x.idPost);
        }
    }
}
