using Core.Library.Attributes;
using Core.Library.Models;
using Core.Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Library.Services
{
    public class DefinePermisionService : IDefinePermision
    {
        public async Task<List<DefinePermisionModel>> LoadPermission()
        {
            List<DefinePermisionModel> definePermisions = new List<DefinePermisionModel>();

            Assembly assembly = Assembly.GetExecutingAssembly();
            var ctrls = assembly.GetTypes()
                .Where(ctype => typeof(Controller).IsAssignableFrom(ctype))
                .Select(type => new {
                    ty = type,
                    methods = type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                });

            if(ctrls != null && ctrls.Any())
            {
                definePermisions = ctrls.Select(ctrl => new DefinePermisionModel
                {
                    ServiceCode = (ctrl.ty.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.ServiceCode,
                    ServiceName = (ctrl.ty.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.ServiceName,
                    GroupCode = (ctrl.ty.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.GroupCode,
                    GroupName = (ctrl.ty.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.GroupName,
                    PermisionCode = (ctrl.ty.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.PermisionCode,
                    PermisionName = (ctrl.ty.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.PermisionName,
                    Description = (ctrl.ty.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.Description,
                    DependenceCode = (ctrl.ty.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.DependenceCode,
                    Order = (ctrl.ty.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.Order ?? 0,
                    All = (ctrl.ty.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.All ?? false,

                    //ControllerName = controller.ty.Name,
                    Actions = ctrl.methods.Select(act => new DefinePermisionModel
                    {
                        ServiceCode = (act.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.ServiceCode,
                        ServiceName = (act.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.ServiceName,
                        GroupCode = (act.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.GroupCode,
                        GroupName = (act.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.GroupName,
                        PermisionCode = (act.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.PermisionCode,
                        PermisionName = (act.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.PermisionName,
                        Description = (act.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.Description,
                        DependenceCode = (act.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.DependenceCode,
                        Order = (act.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.Order ?? 0,
                        All = (act.GetCustomAttribute(typeof(DefinePermisionAttribute)) as DefinePermisionAttribute)?.All ?? false,

                    }).ToList(),
                }).ToList();
            }

            return definePermisions;
        }
    }
}
