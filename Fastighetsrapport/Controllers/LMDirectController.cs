using Fastighetsrapport.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace Fastighetsrapport.Controllers
{
  public class LMDirectController : Controller
  {
    public class FilterOptions
    {
      public string[] objektids { get; set; }
      public string[] categories { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objektid"></param>
    /// <param name="categories"></param>
    /// <returns></returns>
    private PropertyInfo GetData(string objektid, string[] categories)
    {
      PropertyInfo data = new PropertyInfo();
      List<Task> tasks = new List<Task>();

      tasks.Add(Task.Factory.StartNew(() =>
      {
        data.Inskrivningsinformation = LMDirectDataModel.GetEnlistmentData(objektid);
      }));

      tasks.Add(Task.Factory.StartNew(() =>
      {
        data.Markregleringsinformation = LMDirectDataModel.GetGroundControlData(objektid);
      }));

      tasks.Add(Task.Factory.StartNew(() =>
      {
        data.Rattighetsinformation = LMDirectDataModel.GetPrivilegeData(objektid);
      }));

      tasks.Add(Task.Factory.StartNew(() =>
      {
        data.GAinformation = LMDirectDataModel.GetCommonfacilitiesData(objektid);
      }));
      tasks.Add(Task.Factory.StartNew(() =>
      {
        data.Adresser = LMDirectDataModel.GetBelagenhetsAddressData(objektid);
      }));
      tasks.Add(Task.Factory.StartNew(() =>
      {
        data.Registerenhet = LMDirectDataModel.GetFastigheterData(objektid);
      }));
      tasks.Add(Task.Factory.StartNew(() =>
      {
        data.Taxeringsenheter = LMDirectDataModel.GetTaxationData(objektid);
      }));

      data.ExportItems = categories;

      Task.Factory.ContinueWhenAll(tasks.ToArray(), results => { }).Wait();

      return data;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objektids"></param>
    /// <param name="categories"></param>
    /// <returns></returns>
    private List<PropertyInfo> LoadRequests(string[] objektids, string[] categories)
    {
      List<Task> tasks = new List<Task>();
      List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
      foreach (string objektid in objektids)
      {
        tasks.Add(Task.Factory.StartNew(() =>
        {
          propertyInfos.Add(
              this.GetData(
                  objektid,
                  categories
              )
          );
        }));
      }
      Task.Factory.ContinueWhenAll(tasks.ToArray(), results => { }).Wait();
      return propertyInfos;
    }

    // GET: LMDirect
    /// <summary>
    /// 
    /// </summary>
    /// <param name="objektids"></param>
    /// <returns></returns>
    public ActionResult Index(string objektids)
    {
      List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
      if (objektids != null)
      {
        string[] objektidList = objektids.Split('_');
        string[] categories = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };
        propertyInfos = this.LoadRequests(objektidList, categories);
      }

      return View(propertyInfos);
    }

    // POST: LMDirect/filter
    /// <summary>
    /// 
    /// </summary>
    /// <param name="objektids"></param>
    /// <param name="categories"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName("filter")]
    public ActionResult Filter(string json)
    {
      FilterOptions filter = JsonConvert.DeserializeObject<FilterOptions>(json);
      List<PropertyInfo> propertyInfos = this.LoadRequests(filter.objektids, filter.categories);

      propertyInfos = propertyInfos.ToList<PropertyInfo>();
      return View("Index", propertyInfos);
    }
  }
}