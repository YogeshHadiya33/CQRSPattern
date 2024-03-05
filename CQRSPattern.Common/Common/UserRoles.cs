using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSPattern.Common.Common;

public enum UserRoles
{
   [Description("Admin")]Admin,
   [Description("User")]User,
}
