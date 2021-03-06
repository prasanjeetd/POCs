﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RNDApplications.MyServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MyClass", Namespace="http://schemas.datacontract.org/2004/07/RNDWcfService")]
    [System.SerializableAttribute()]
    public partial class MyClass : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MyServiceReference.IMyService")]
    public interface IMyService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyService/DoWork", ReplyAction="http://tempuri.org/IMyService/DoWorkResponse")]
        void DoWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyService/DoWork", ReplyAction="http://tempuri.org/IMyService/DoWorkResponse")]
        System.Threading.Tasks.Task DoWorkAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyService/MyFunction", ReplyAction="http://tempuri.org/IMyService/MyFunctionResponse")]
        void MyFunction(RNDApplications.MyServiceReference.MyClass myClass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyService/MyFunction", ReplyAction="http://tempuri.org/IMyService/MyFunctionResponse")]
        System.Threading.Tasks.Task MyFunctionAsync(RNDApplications.MyServiceReference.MyClass myClass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyService/MyFunction1", ReplyAction="http://tempuri.org/IMyService/MyFunction1Response")]
        RNDApplications.MyServiceReference.MyClass MyFunction1();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyService/MyFunction1", ReplyAction="http://tempuri.org/IMyService/MyFunction1Response")]
        System.Threading.Tasks.Task<RNDApplications.MyServiceReference.MyClass> MyFunction1Async();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyService/MyFunction2", ReplyAction="http://tempuri.org/IMyService/MyFunction2Response")]
        RNDApplications.MyServiceReference.MyClass MyFunction2(RNDApplications.MyServiceReference.MyClass myClass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMyService/MyFunction2", ReplyAction="http://tempuri.org/IMyService/MyFunction2Response")]
        System.Threading.Tasks.Task<RNDApplications.MyServiceReference.MyClass> MyFunction2Async(RNDApplications.MyServiceReference.MyClass myClass);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMyServiceChannel : RNDApplications.MyServiceReference.IMyService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MyServiceClient : System.ServiceModel.ClientBase<RNDApplications.MyServiceReference.IMyService>, RNDApplications.MyServiceReference.IMyService {
        
        public MyServiceClient() {
        }
        
        public MyServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MyServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MyServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MyServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DoWork() {
            base.Channel.DoWork();
        }
        
        public System.Threading.Tasks.Task DoWorkAsync() {
            return base.Channel.DoWorkAsync();
        }
        
        public void MyFunction(RNDApplications.MyServiceReference.MyClass myClass) {
            base.Channel.MyFunction(myClass);
        }
        
        public System.Threading.Tasks.Task MyFunctionAsync(RNDApplications.MyServiceReference.MyClass myClass) {
            return base.Channel.MyFunctionAsync(myClass);
        }
        
        public RNDApplications.MyServiceReference.MyClass MyFunction1() {
            return base.Channel.MyFunction1();
        }
        
        public System.Threading.Tasks.Task<RNDApplications.MyServiceReference.MyClass> MyFunction1Async() {
            return base.Channel.MyFunction1Async();
        }
        
        public RNDApplications.MyServiceReference.MyClass MyFunction2(RNDApplications.MyServiceReference.MyClass myClass) {
            return base.Channel.MyFunction2(myClass);
        }
        
        public System.Threading.Tasks.Task<RNDApplications.MyServiceReference.MyClass> MyFunction2Async(RNDApplications.MyServiceReference.MyClass myClass) {
            return base.Channel.MyFunction2Async(myClass);
        }
    }
}
