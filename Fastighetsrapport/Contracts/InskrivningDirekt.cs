using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using Fastighetsrapport.Network;
using LM.InskrivningDirekt;

namespace Fastighetsrapport.Contracts
{
  public class InskrivningDirekt
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="objektid"></param>
    /// <returns></returns>
    public IEnumerable<InskrivningMemberType> GetInskrivningData(string objektid)
    {
      InskrivningPortTypeClient client = new InskrivningPortTypeClient();

      using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
      {
        HttpRequestMessageProperty httpRequestProperty = new HttpRequestMessageProperty();

        httpRequestProperty.Headers.Add("Authorization", "Bearer " + StringToken);

        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;

        InskrivningRegisterenhetFilterType filter = new InskrivningRegisterenhetFilterType();
        filter.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.objektidentitet };
        filter.Items = new string[] { objektid };

        GetInskrivningRequestType request = new GetInskrivningRequestType()
        {
          IncludeData = new InskrivningDatasetType()
          {
            Items = new bool[] { true },
            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.total }
          },
          Items = new object[] { filter }
        };

        return client.GetInskrivning(request).InskrivningMember;
      }
    }
  }
}