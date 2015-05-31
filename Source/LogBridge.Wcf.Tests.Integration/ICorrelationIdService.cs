using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace LogBridge.Wcf.Tests.Integration
{
    [ServiceContract]
    public interface ICorrelationIdService
    {
        [OperationContract]
        void SendMessage(string message);
    }
}
