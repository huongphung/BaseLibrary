using Core.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Library.Services.Interfaces
{
    public interface IDefinePermision
    {
        Task<List<DefinePermisionModel>> LoadPermission();
    }
}
