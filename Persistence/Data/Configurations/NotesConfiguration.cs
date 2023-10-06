using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.Data.Configurations;
    public class NotesConfiguration : IEntityTypeConfiguration<Notes>
{
    public void Configure(EntityTypeBuilder<Notes> builder)
    {
        builder.ToTable("notes");
        builder.Property(x=>x.Subject)
        .IsRequired();
        builder.Property(x=>x.Note)
        .IsRequired();
        builder.HasOne(x=>x.Student)
        .WithMany(s => s.Notes)
        .HasForeignKey(x=>x.IdStudent);

    }
}