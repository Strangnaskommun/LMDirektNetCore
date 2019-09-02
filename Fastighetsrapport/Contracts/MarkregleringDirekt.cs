using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using Fastighetsrapport.Network;
using LM.MarkregleringDirekt;

namespace Fastighetsrapport.Contracts
{
  public class MarkregleringDirekt
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="objektid"></param>
    /// <returns></returns>
    public List<MarkregleringMemberType> GetMarkregleringData(string objektid)
    {

      MarkregleringPortTypeClient client = new MarkregleringPortTypeClient();
      client.ClientCredentials.UserName.UserName = Creds.UserName;
      client.ClientCredentials.UserName.Password = Creds.Password;

      using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
      {
        HttpRequestMessageProperty httpRequestProperty = new HttpRequestMessageProperty();
        httpRequestProperty.Headers[System.Net.HttpRequestHeader.Authorization] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(client.ClientCredentials.UserName.UserName + ":" + client.ClientCredentials.UserName.Password));
        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;

        List<MarkregleringMemberType> croundControls = new List<MarkregleringMemberType>();

        RegisterenhetFilterType filter = new RegisterenhetFilterType()
        {
          ItemElementName = ItemChoiceType2.objektidentitet,
          Item = objektid
        };
        FindMarkregleringRequestType findRequest = new FindMarkregleringRequestType()
        {
          Item = filter
        };

        MarkregleringsreferensType[] markregleringar = client.FindMarkreglering(findRequest);

        markregleringar.ToList().ForEach(markreglering =>
        {
          GetMarkregleringRequestType request = new GetMarkregleringRequestType();
          request.IncludeData = new MarkregleringDatasetType()
          {
            Items = new bool[] { true },
            ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.total }
          };
          request.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.objektidentitet };
          request.Items = new string[] { markreglering.objektidentitet };
          croundControls.Add(client.GetMarkreglering(request).MarkregleringMember.FirstOrDefault());
        });

        return croundControls;
      }
    }
  }
}