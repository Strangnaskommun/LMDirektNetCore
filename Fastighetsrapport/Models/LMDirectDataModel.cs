using Fastighetsrapport.Contracts;
using LM.GADirekt;
using LM.InskrivningDirekt;
using LM.MarkregleringDirekt;
using LM.RattighetDirekt;
using LM.TaxeringDirekt;
using Fastighetsrapport.Models.Belagenhetsadress;
using Fastighetsrapport.Models.Fastigheter;
using System.Collections.Generic;

namespace Fastighetsrapport.Models
{
    public class PropertyInfo
    {
        //public RegisterenhetType Registerenhet { get; set; }
        public IEnumerable<RegisterenhetMemberType> Registerenhet { get; set; }
        public IEnumerable<BelagenhetsadressMemberType> Adresser { get; set; }
        public IEnumerable<InskrivningMemberType> Inskrivningsinformation { get; set; }
        public IEnumerable<TaxeringsenhetMemberType> Taxeringsenheter { get; set; }
        public IEnumerable<MarkregleringMemberType> Markregleringsinformation { get; set; }
        public IEnumerable<RattighetMemberType> Rattighetsinformation { get; set; }
        public IEnumerable<GemensamhetsanlaggningMemberType> GAinformation { get; set; }
        public string[] ExportItems { get; set; }
    }

    public static class LMDirectDataModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objektid"></param>
        /// <returns></returns>
        public static IEnumerable<RegisterenhetMemberType> GetFastigheterData(string objektid)
        {
            FastigheterDirekt fastighetDirekt = new FastigheterDirekt();
            
            return fastighetDirekt.GetFastigheterData(objektid);

            //RegisterenhetMemberType response = fastighetDirekt.GetFastigheterData(objektid);
            //RegisterenhetType member = null;
            //if (response.RegisterenhetMember != null)
            //{
            //    member = response.RegisterenhetMember.FirstOrDefault().Item;
            //}
            //return member;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objektid"></param>
        /// <returns></returns>
        public static IEnumerable<BelagenhetsadressMemberType> GetBelagenhetsAddressData(string objectid)
        {
            BelagenhetsadressDirekt adressDirekt = new BelagenhetsadressDirekt();
            return adressDirekt.GetBelagenhetsadressData(objectid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objektid"></param>
        /// <returns></returns>
        public static IEnumerable<InskrivningMemberType> GetEnlistmentData(string objektid)
        {
            InskrivningDirekt inskrivningDirekt = new InskrivningDirekt();
            return inskrivningDirekt.GetInskrivningData(objektid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objektid"></param>
        /// <returns></returns>
        public static IEnumerable<MarkregleringMemberType> GetGroundControlData(string objektid)
        {
            MarkregleringDirekt markregleringDirekt = new MarkregleringDirekt();
            return markregleringDirekt.GetMarkregleringData(objektid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objektid"></param>
        /// <returns></returns>
        public static IEnumerable<RattighetMemberType> GetPrivilegeData(string objektid)
        {
            RattighetDirekt rattighetDirekt = new RattighetDirekt();
            return rattighetDirekt.GetRattighetData(objektid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objektid"></param>
        /// <returns></returns>
        public static IEnumerable<TaxeringsenhetMemberType> GetTaxationData(string objektid)
        {
            TaxeringDirekt taxeringDirekt = new TaxeringDirekt();
            return taxeringDirekt.GetTaxeringsenhetData(objektid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objektid"></param>
        /// <returns></returns>
        public static IEnumerable<GemensamhetsanlaggningMemberType> GetCommonfacilitiesData(string objektid)
        {
            GADirekt GADirekt = new GADirekt();
            return GADirekt.GetGemensamhetsData(objektid);
        }
    }
}