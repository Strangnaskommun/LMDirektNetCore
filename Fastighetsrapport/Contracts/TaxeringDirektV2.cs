using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Fastighetsrapport.Models.TaxeringV2;

namespace Fastighetsrapport.Contracts
{
  public class TaxeringDirektV2
  {

    /// <summary>
    /// Get address info by address id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>JSON-data</returns>
    public IEnumerable<TaxeringsenhetMemberType> GetTaxeringsenhetData(string id)
    {
      string getreferense = "https://api.lantmateriet.se/distribution/produkter/taxering/v2/referens/beror/";
      getreferense += id;

      var xmlStringReference = Network.Request.Get(getreferense);

      XElement xmlTree = XElement.Parse(xmlStringReference);
      XNamespace ns = "http://namespace.lantmateriet.se/distribution/produkter/taxering/v2";

      List<string> idList = xmlTree.Descendants(ns + "Taxeringsenhetsreferens")
                  .Select(x => x.Element(ns + "id").Value)
                  .ToList();

      XElement requestBody = new XElement(ns + "IdRequest", idList.Select(l => new XElement(ns + "id", l)));

      string getEnheter = "https://api.lantmateriet.se/distribution/produkter/taxering/v2/";
      getEnheter += "?includeData=total";

      string xmlStringUnits = Network.Request.Post(getEnheter, requestBody);


      XmlSerializer serializer = new XmlSerializer(typeof(TaxeringsenhetResponseType));
      StringReader rdr = new StringReader(xmlStringUnits);

      TaxeringsenhetResponseType taxeringsenhetsResponse = (TaxeringsenhetResponseType)serializer.Deserialize(rdr);
      return taxeringsenhetsResponse.TaxeringsenhetMember;
    }
  }
}
