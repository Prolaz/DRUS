using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace PubSubService
{
    public interface IPubSubContract
    {
        [OperationContract(IsOneWay = true)]
        void ValueChange(string Id, string Type, int Value);
    }
}
