using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;

namespace Fastighetsrapport.Util
{
    public class MessageInspector : IClientMessageInspector
    {

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            string message = reply.ToString();
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            return new object();
        }
    }
        
    public class InspectorBehavior : IEndpointBehavior
    {
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {            
            clientRuntime.ClientMessageInspectors.Add(new MessageInspector());            
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {            
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {            
        }

        public void Validate(ServiceEndpoint endpoint)
        {            
        }
    }
}