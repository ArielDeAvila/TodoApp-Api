using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Server.Data.Entities;

namespace Todo.Server.Data.Configuration;

public class NoteTaskConfiguration : IEntityTypeConfiguration<NoteTask>
{
    public void Configure(EntityTypeBuilder<NoteTask> builder)
    {
        builder.HasKey(x => x.Id).HasName("PK_Task");

        builder.HasOne(u => u.User).WithMany(nt => nt.Tasks)
               .HasForeignKey(nt => nt.UserId)
               .HasConstraintName("FK_Task_User");

    }
}
