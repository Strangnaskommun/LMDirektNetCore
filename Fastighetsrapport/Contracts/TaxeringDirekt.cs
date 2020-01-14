using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using Fastighetsrapport.Network;
using LM.TaxeringDirekt;

namespace Fastighetsrapport.Contracts
{
  public class TaxeringDirekt
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="objektid"></param>
    /// <returns></returns>
    public IEnumerable<TaxeringsenhetMemberType> GetTaxeringsenhetData(string objektid)
    {
      TaxeringPortTypeClient client = new TaxeringPortTypeClient();

      using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
      {
        HttpRequestMessageProperty httpRequestProperty = new HttpRequestMessageProperty();

        httpRequestProperty.Headers.Add("Authorization", "Bearer " + StringToken);

        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;


        FindTaxeringsenhetRequestType findRequest = new FindTaxeringsenhetRequestType();
        findRequest.ItemElementName = ItemChoiceType.registerenhet;
        findRequest.Item = objektid;

        TaxeringsenhetsreferensType[] taxeringsenheter = client.FindTaxeringsenhet(findRequest);

        List<TaxeringsenhetMemberType> taxeringsenheterna = new List<TaxeringsenhetMemberType>();

        taxeringsenheter.ToList().ForEach(taxeringsenhet =>
        {
          GetTaxeringsenhetRequestType request = new GetTaxeringsenhetRequestType()
          {
            id = new string[] { taxeringsenhet.id },
            IncludeData = new DatasetType()
            {
              ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.total },
              Items = new bool[] { true }
            }
          };
          taxeringsenheterna.AddRange(client.GetTaxeringsenhet(request).TaxeringsenhetMember);
        });

        return taxeringsenheterna;
      }
    }
  }
}
