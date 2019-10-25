using System;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;

namespace NetModular.Module.Common.Infrastructure.Repositories
{
    public class CommonDbContext : DbContext
    {
        public CommonDbContext(IDbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {
        }
    }
}
