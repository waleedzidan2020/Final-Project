using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrade.ViewModels
{
    public class APIViewModel
    {
        public bool Success { get; set; }
        public string? Massege { get; set; }
        public object? Data { get; set; }
        public string? url { get; set; }
        public string? Token { get; set; }
    }
}
