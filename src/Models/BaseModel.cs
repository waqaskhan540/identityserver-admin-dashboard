using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Models
{
    public class BaseModel
    {
        public bool success { get; set; }
        public object data { get; set; }
        public string message { get; set; }
        public int total { get; set; }

        public BaseModel()
        {

        }

        public BaseModel(bool success, object data = null, string message = "", int total = 0)
        {
            this.success = success;
            this.data = data;
            this.message = message;
            this.total = total;
        }

        public static BaseModel Create(bool success, object data = null, string message = "", int total = 0)
        {
            return new BaseModel() { success = success, data = data, message = message, total = total };
        }

        public static BaseModel Error(string message, object data = null, int total = 0)
        {
            return new BaseModel(false, data, message, total);
        }

        public static BaseModel Success(object data = null, int total = 0, string message = "")
        {
            return new BaseModel(true, data, message, total);
        }

        public static BaseModel Success(string message)
        {
            return new BaseModel
            {
                success = true,
                message = message
            };
        }
        public static BaseModel Success(string msg,object data)
        {
            return new BaseModel
            {
                success = true,
                message = msg,
                data = data
            };
        }

        public static BaseModel Error(string msg)
        {
            return new BaseModel
            {
                success = false,
                message = msg,
                data = null
            };
        }

       
    }
}
