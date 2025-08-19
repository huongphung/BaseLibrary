using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Library.Enums
{
    public enum EExceptionType
    {
        [Code("ValidationException"), Description("Ngoại lệ xác thực")]
        ValidationException = 0,

        [Code("InternalServerError"), Description("Lỗi Server nội bộ")]
        InternalServerError = 1,
    }

    public enum EErrorCode
    {
        [Code("Validation"), Description("Xác thực")]
        Validation = 0,
    }
}
