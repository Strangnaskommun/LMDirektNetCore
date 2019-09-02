using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using Fastighetsrapport.Models.Belagenhetsadress;

namespace Fastighetsrapport.Contracts
{
  public class BelagenhetsadressDirekt
  {

    /// <summary>
    /// Get address info by address id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>JSON-data</returns>
    public IEnumerable<BelagenhetsadressMemberType> GetBelagenhetsadressData(string id)
    {
      //
      // 12345678-1234-1234-1234-123456789012?includeData=total&srid=3006
      //
      string url = "https://services.lantmateriet.se/distribution/produkter/belagenhetsadress/v4.1/registerenhet/";
      url += id + "?includeData=total";

      string xmlString = Network.Request.Get(url);

      XmlSerializer serializer = new XmlSerializer(typeof(BelagenhetsadressResponseType));
      StringReader rdr = new StringReader(xmlString);

      BelagenhetsadressResponseType belagenhetsadressResponse = (BelagenhetsadressResponseType)serializer.Deserialize(rdr);
      return belagenhetsadressResponse.BelagenhetsadressMember;
    }
  }
}