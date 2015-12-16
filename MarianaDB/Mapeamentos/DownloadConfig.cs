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
    public class DownloadConfig : EntityTypeConfiguration<Download>
    {
        public DownloadConfig()
        {
            ToTable("DOWNLOAD");
            HasKey(x => x.id);

            Property(x => x.id)
                .HasColumnName("IDDOWNLOAD")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.ip)
                .HasColumnName("IP")
                .HasMaxLength(100)
                .IsRequired();           

            Property(x => x.dataHora)
                .HasColumnName("DATAHORA")
                .IsRequired();

            Property(x => x.idArquivo)
                .HasColumnName("IDARQUIVO")
                .IsRequired();

            HasRequired(x => x.Arquivo)
                .WithMany()
                .HasForeignKey(x => x.idArquivo);
        }
    }
}