using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Fastighetsrapport.Util
{
    public static class GeometryParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coords"></param>
        /// <returns></returns>
        public static string ConvertDoubleArrayToString(double[] coords)
        {
            return String.Join(" ", coords);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coords"></param>
        /// <returns></returns>
        public static double[] ConvertStringToDoubleArray(string coords)
        {
            List<string> splitted = coords.Split(' ').ToList();
            List<double> coordinates = new List<double>();
            splitted.ForEach(c => coordinates.Add(Double.Parse(c, CultureInfo.CreateSpecificCulture("en-US"))));
            return coordinates.ToArray();
        }
        
    }
}