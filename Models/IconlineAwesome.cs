using System.Collections.Generic;

namespace Icon.Api.Models
{
    public class IconlineAwesome
    {
        public IEnumerable<IconType> IconsType { get; set; }
    }

    public class IconType
    {
        public string IconTypeName { get; set; }
        public IEnumerable<IconDetail> IconDetails { get; set; }
    }

    public class IconDetail
    {
        public string IconClass { get; set; }
        public string IconName { get; set; }
    }
}
