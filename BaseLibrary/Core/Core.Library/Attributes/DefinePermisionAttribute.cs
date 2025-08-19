using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Library.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class DefinePermisionAttribute : Attribute
    {
        public string serviceCode { get; set; }
        public string serviceName { get; set; }

        public string groupCode { get; set; }
        public string groupName { get; set; }

        public string permisionCode { get; set; }
        public string permisionName { get; set; }

        public string description { get; set; }
        public string dependenceCode { get; set; }
        public int order { get; set; }
        public bool all { get; set; }

        public DefinePermisionAttribute(
            string ServiceCode,
            string ServiceName,
            string GroupCode,
            string GroupName,
            string PermisionCode,
            string PermisionName,
            string Description,
            string DependenceCode = "",
            int Order = 0,
            string Dependence = "",
            bool All = false
        ){
            this.serviceCode = ServiceCode;
            this.serviceName = ServiceName;
            this.groupCode = GroupCode;
            this.groupName = GroupName;
            this.permisionCode = PermisionCode;
            this.permisionName = PermisionName;
            this.description = Description;
            this.dependenceCode = DependenceCode;
            this.order = Order;
            this.all = All;
        }

        public string ServiceCode
        {
            get { return this.serviceCode; }
        }

        public string ServiceName
        {
            get { return this.serviceName; }
        }

        public string GroupCode
        {
            get { return this.groupCode; }
        }

        public string GroupName
        {
            get { return this.groupName; }
        }

        public string PermisionCode
        {
            get { return this.permisionCode; }
        }

        public string PermisionName
        {
            get { return this.permisionName; }
        }

        public string Description
        {
            get { return this.description; }
        }

        public string DependenceCode
        {
            get { return this.dependenceCode; }
        }

        public int Order
        {
            get { return this.order; }
        }

        public bool All
        {
            get { return all; }
        }
    }
}
