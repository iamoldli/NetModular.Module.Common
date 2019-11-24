using NetModular.Lib.Data.Abstractions.Attributes;

namespace NetModular.Module.Common.Domain.Dict
{
    public partial class DictEntity
    {
        [Ignore]
        public string GroupName { get; set; }
    }
}