using System;
using System.Collections.Generic;
using System.Linq;

namespace Fastighetsrapport.Util
{
  public static class VarderingsenhetTranslator
  {
    private static Dictionary<string, string> names = new Dictionary<string, string>()
        {
            { "SkogType", "Skog" },
            { "AkermarkType", "Åkermark" },
            { "BetesmarkType", "Betesmark" },
            { "EkonomibyggnadType", "Ekonomibyggnad" },
            { "ByggnadOvrigtKraftverkType", "Byggnad övrigt kraftverk" },
            { "MarkPaOvrigtKraftverkType", "Mark på övrigt kraftverk" },
            { "ByggnadVattenfallsenhetType", "Byggnad på Vattenfallsenhet" },
            { "MarkPaVattenfallsenhetType", "Mark på Vattenfallsenhet" },
            { "SmahusmarkType", "Småhusmark" },
            { "SmahusmarkLantbrukType", "Småhusmark lantbruk" },
            { "SmahusbyggnadType", "Småhusbyggnad" },
            { "SmahusbyggnadLantbrukType", "Småhusbyggnad lantbruk" },
            { "HyreshusmarkBostadType", "Hyreshusmark bostad" },
            { "HyreshusmarkLokalType", "Hyreshusmark lokal" },
            { "HyreshusbyggnadLokalType", "Hyreshusbyggnad lokal" },
            { "TaktmarkType", "Täktmark" },
            { "IndustrimarkType", "Industrimark" },
            { "IndustribyggnadProduktionskostnadType", "Industribyggnad produktionskostnad" },
            { "ProduktionslokalAvkastningType", "Produktionslokal avkastning" },
            { "KontorAvkastningType", "Kontor avkastning" },
            { "LagerAvkastningType", "Lager avkastning" },
            { "IndustribyggnadUnderUppforandeType", "Industribyggnad under uppforande" },
            { "SkogsimpedimentmarkType", "Skogsimpedimentmark" },
            { "SkogMedRestriktionType", "Skog med restriktion" }
        };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string Translate(string type)
    {
      var name = "";
      if (names.Keys.Contains(type))
      {
        name = names[type];
      }
      return name;
    }
  }
}