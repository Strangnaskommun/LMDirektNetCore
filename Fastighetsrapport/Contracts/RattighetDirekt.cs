using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using Fastighetsrapport.Network;
using LM.RattighetDirekt;

namespace Fastighetsrapport.Contracts
{
  public class RattighetDirekt
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="objektid"></param>
    /// <returns></returns>
    public List<RattighetMemberType> GetRattighetData(string objektid)
    {
      RattighetPortTypeClient client = new RattighetPortTypeClient();

      using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
      {
        HttpRequestMessageProperty httpRequestProperty = new HttpRequestMessageProperty();

        httpRequestProperty.Headers.Add("Authorization", "Bearer " + "StringToken");

        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;

        List<RattighetMemberType> privileges = new List<RattighetMemberType>();

        RegisterenhetFilterType filter = new RegisterenhetFilterType()
        {
          ItemElementName = ItemChoiceType1.objektidentitet,
          Item = objektid
        };
        FindRattighetRequestType findRequest = new FindRattighetRequestType()
        {
          Item = filter
        };

        List<RattighetsreferensType> rattigheter = new List<RattighetsreferensType>();
        RattighetsreferensResponseType rattighet = client.FindRattighet(findRequest);
        if (rattighet.Items != null)
        {
          foreach (var item in rattighet.Items)
          {
            if (item is RattighetsreferensArrayPropertyType)
            {
              RattighetsreferensArrayPropertyType ans = item as RattighetsreferensArrayPropertyType;
              rattigheter.AddRange(ans.Rattighetsreferens);
            }
            if (item is RattighetsreferensType)
            {
              RattighetsreferensType ans = item as RattighetsreferensType;
              rattigheter.Add(ans);
            }
          }
        }

        rattigheter.ToList().ForEach(ratt =>
        {
          GetRattighetRequestType request = new GetRattighetRequestType();
          request.IncludeData = new RattighetDatasetType()
          {
            Items = new bool[] { true },
            ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.total }
          };
          request.Items = new string[] { ratt.objektidentitet };
          privileges.Add(client.GetRattighet(request).RattighetMember.FirstOrDefault());
        });

        return privileges;
      }
    }
  }
}