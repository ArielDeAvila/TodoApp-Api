using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Server.Data.Entities;

namespace Todo.Server.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id).HasName("PK_User");

        builder.HasIndex(x => x.Email).IsUnique();
    }
}
