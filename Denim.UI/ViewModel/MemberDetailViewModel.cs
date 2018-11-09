using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denim.UI.ViewModel
{
    public class MemberDetailViewModel : IMemberDetailViewModel
    {
        public string MyName
        {
            get
            {
                return "강병철";
            }

            set
            {
                value = "강병철";
            }
        }

    }

    public interface IMemberDetailViewModel
    {
        string MyName { get; set; }
    }
}
