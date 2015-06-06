using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RNDWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MyService.svc or MyService.svc.cs at the Solution Explorer and start debugging.
    public class MyService : IMyService
    {
        public void DoWork()
        {
        }

        public void MyFunction(MyClass myClass)
        {
            string name = myClass.Name;
        }


        public  MyClass MyFunction1()
        {
            MyClass myClass = new MyClass();
            myClass.Name = "Prasanjeet";
            return myClass;
        }


        public MyClass MyFunction2(MyClass myClass)
        {
            MyClass myClassReturn = new MyClass();
            myClassReturn.Name = "Prasanjeet" + myClass.Name;
            return myClassReturn;
        }
    }
}
