using Denim.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denim.UI.Wrapper
{
    public class MemberWrapper : ModelWrapper<Member>
    {
        public MemberWrapper(Member model) : base(model)
        {

        }

        public int Id { get { return Model.Id;  } }

        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string OfficeId
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int? DepartmentId
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }

        public int? IpNumber
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }

        public bool? IsAllowMotion
        {
            get { return GetValue<bool?>() == null ? false : GetValue<bool?>(); }
            set { SetValue(value); }
        }

        public bool? IsAllowTypo
        {
            get { return GetValue<bool?>() == null ? false : GetValue<bool?>(); }
            set { SetValue(value); }
        }

        public bool? IsAllowBrand
        {
            get { return GetValue<bool?>() == null ? false : GetValue<bool?>(); }
            set { SetValue(value); }
        }

        public bool? IsAllowVFX
        {
            get { return GetValue<bool?>() == null ? false : GetValue<bool?>(); }
            set { SetValue(value); }
        }
    }
}
