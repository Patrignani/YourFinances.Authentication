using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using YourFinances.Authentication.Domain.Core.Models;

namespace YourFinances.Authentication.Infra.Data.Mapping
{
    internal partial class ClientMapping
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.UserEdition).WithMany().HasForeignKey(x => x.UserEditionId).IsRequired();
        }
    }
}
