using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokaInfrestructura.Halper
{
    public class ReturnHelper<T>
    {
        public T Value { get; set; }
        public string MenssageSucess { get; set; }

        public List<string> Error { get; set; }

        public ReturnHelper()
        {
            this.Error = new List<string>();
        }
        public void AddError(string error)
        {
            this.Error.Add(error);
        }
        public bool Success
        {
            get { return (this.Error.Count == 0); }
        }
    }
}
