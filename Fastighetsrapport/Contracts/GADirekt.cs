using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using Fastighetsrapport.Network;
using LM.GADirekt;

namespace Fastighetsrapport.Contracts
{
  public class GADirekt
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="objektid"></param>
    /// <returns></returns>
    public IEnumerable<GemensamhetsanlaggningMemberType> GetGemensamhetsData(string objektid)
    {
      GemensamhetsanlaggningPortTypeClient client = new GemensamhetsanlaggningPortTypeClient();
     using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
      {
        HttpRequestMessageProperty httpRequestProperty = new HttpRequestMessageProperty();

        httpRequestProperty.Headers.Add("Authorization", "Bearer " + "StringToken");

        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
        DelagandeRegisterenhetFilter filter = new DelagandeRegisterenhetFilter();
        filter.ItemElementName = ItemChoiceType.objektidentitet;
        filter.Item = objektid;

        FindGemensamhetsanlaggningRequestType findRequest = new FindGemensamhetsanlaggningRequestType();
        findRequest.Item = filter;

        RegisterenhetsreferensType[] gemensamhetsanlaggningar = client.FindGemensamhetsanlaggning(findRequest);
        List<GemensamhetsanlaggningMemberType> gemensamhetsanlaggningarna = new List<GemensamhetsanlaggningMemberType>();

        gemensamhetsanlaggningar.ToList().ForEach(gemensamhetsanlaggning =>
        {
          GetGemensamhetsanlaggningRequestType request = new GetGemensamhetsanlaggningRequestType()
          {
            IncludeData = new GemensamhetsanlaggningDatasetType()
            {
              Items = new bool[] { true },
              ItemsElementName = new ItemsChoiceType1[] { ItemsChoiceType1.total }
            }
          };
          request.ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.objektidentitet };
          request.Items = new string[] { gemensamhetsanlaggning.objektidentitet };
          gemensamhetsanlaggningarna.AddRange(client.GetGemensamhetsanlaggning(request).GemensamhetsanlaggningMember);
        });

        return gemensamhetsanlaggningarna;
      }
    }
  }
}
