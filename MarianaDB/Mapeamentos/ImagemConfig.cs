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
    public class ImagemConfig : EntityTypeConfiguration<Imagem>
    {
        public ImagemConfig()
        {
            ToTable("IMAGEM");
            HasKey(x => x.id);

            Property(x => x.id)
                .HasColumnName("IDIMAGEM")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Extensao)
                .HasMaxLength(10)
                .HasColumnName("EXTENSAO")
                .IsRequired();

            Property(x => x.Bytes)                
                .HasColumnName("BYTES")
                .IsRequired();                     

            Property(x => x.idPost)
                .HasColumnName("IDPOST")
                .IsRequired();

            HasRequired(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.idPost);
        }
    }
}