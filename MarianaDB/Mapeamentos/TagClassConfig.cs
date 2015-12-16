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
    public class TagClassConfig : EntityTypeConfiguration<TagClass>
    {
        public TagClassConfig()
        {
            ToTable("TAG");
            HasKey(x => x.Tag);

            Property(x => x.Tag)
                .HasColumnName("TAG")
                .HasMaxLength(20)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);         
        }
    }
}