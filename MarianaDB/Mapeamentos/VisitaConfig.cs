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
    public class VisitaConfig : EntityTypeConfiguration<Visita>
    {
        public VisitaConfig()
        {
            ToTable("VISITA");
            HasKey(x => x.id);

            Property(x => x.id)
                .HasColumnName("IDVISITA")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.ip)
                .HasColumnName("IP")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.dataHora)
                .HasColumnName("DATAHORA")
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