using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using Fastighetsrapport.Models.Fastigheter;

namespace Fastighetsrapport.Contracts
{
  public class FastigheterDirekt
  {

    /// <summary>
    /// Get fastighet info by fastighet id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>JSON-data</returns>
    public IEnumerable<RegisterenhetMemberType> GetFastigheterData(string objektid)
    {
      string url = "https://services.lantmateriet.se/distribution/produkter/fastighet/v2.1/";
      url += objektid + "?includeData=total";

      string xmlString = Network.Request.Get(url);

      XmlSerializer serializer = new XmlSerializer(typeof(RegisterenhetResponseType));
      StringReader rdr = new StringReader(xmlString);

      RegisterenhetResponseType registerenhetsResponse = (RegisterenhetResponseType)serializer.Deserialize(rdr);

            return registerenhetsResponse.RegisterenhetMember;
            
    }
  }
}