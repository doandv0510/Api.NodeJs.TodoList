using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApps.DAO
{
    public class BaseDAO
    {
        protected APIResponseData responseData_;
        protected string address_;
        public BaseDAO()
        {
            responseData_ = new APIResponseData();
        }
    }
}
