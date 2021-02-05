using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourFinances.Authentication.Domain.Core.Models;

namespace YourFinances.Authentication.Infra.Data.Mapping
{
    internal partial class AccountMapping
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.UserEdition).WithMany().HasForeignKey(x => x.UserEditionId).IsRequired();
        }
    }
}
