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
    public class TagPostConfig : EntityTypeConfiguration<TagPost>
    {
        public TagPostConfig()
        {
            ToTable("TAGPOST");
            HasKey(x => x.id);

            Property(x => x.id)
                .HasColumnName("IDTAGPOST")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.idTagClass)
                .HasColumnName("TAG")
                .HasMaxLength(20)
                .IsRequired();

            HasRequired(x => x.TagClass)
                .WithMany()
                .HasForeignKey(x => x.idTagClass);

            Property(x => x.idPost)
                .HasColumnName("IDPOST")
                .IsRequired();

            HasRequired(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.idPost);
        }
    }
}