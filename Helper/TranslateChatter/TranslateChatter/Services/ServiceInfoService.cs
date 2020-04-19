using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranslateChatter.Services
{
    public class ServiceInfo
    {
        public string Name { get; set; }
        public string BaseUrl { get; set; }
    }

    public class ServiceInfoService
    {
        public List<ServiceInfo> ServiceInfos { get; set; }
        public string this[string name] => ServiceInfos.FirstOrDefault(x => x.Name == name)?.BaseUrl;
    }
}
