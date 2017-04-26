using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCommon.Common.Misc
{
    /// <summary>
    ///  참고 : http://www.geodatasource.com 
    /// </summary>
    public enum DistanceUnit
    {
        METER,
        KILOMETER,
        STATUTEMILE,
        NAUTICALMILE
    }


    public class Geo
    {

        /// <summary>
        /// 두 위/경도 거리 계산
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static double distance(string lat1, string lon1, string lat2, string lon2, DistanceUnit unit)
        {
            double lat1d = System.Convert.ToDouble(lat1);
            double lon1d = System.Convert.ToDouble(lon1);
            double lat2d = System.Convert.ToDouble(lat2);
            double lon2d = System.Convert.ToDouble(lon2);

            return distance(lat1d, lon1d, lat2d, lon2d, unit);
        }


        /// <summary>
        /// 두 위/경도 거리 계산
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static double distance(double lat1, double lon1, double lat2, double lon2, DistanceUnit unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case DistanceUnit.METER:
                    dist = dist * 1.609344 * 1000;
                    break;
                case DistanceUnit.KILOMETER:
                    dist = dist * 1.609344;
                    break;
                case DistanceUnit.NAUTICALMILE:
                    dist = dist * 0.8684;
                    break;
                case DistanceUnit.STATUTEMILE:
                    break;
            }


            return (dist);
        }


        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }



    }
}
