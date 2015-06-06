using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RNDWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMyService" in both code and config file together.
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        void MyFunction(MyClass myClass);
        
        [OperationContract]
        MyClass MyFunction1();
        
        [OperationContract]
        MyClass MyFunction2(MyClass myClass);
        
    }
    [DataContract]
    public class MyClass
    {
        //[DataMember]
        //public string Name { get; set; }

        string name = "No Name";
        [DataMember]
        public string Name 
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
    }
}
