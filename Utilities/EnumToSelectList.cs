using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace GetOnIt.Utilities
{
    /// <summary>
    /// Creating a converter to not have to deal with pesky enumerations and code each invidual one, 1000 times.
    /// https://terrybrown.me/posts/old/creating-a-drop-down-list-from-an-enum-in-asp-net-mvc/
    /// Code retrieved from this kind sir!
    /// </summary>
    public static class EnumToSelectList
    {
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
        {
            var values = (from TEnum e in Enum.GetValues(typeof(TEnum))
                          select new { ID = e, Name = (e as Enum).GetDescriptionString() }).ToList();

            return new SelectList(values, "Id", "Name", enumObj);
        }

        public static string PascalCaseToPrettyString(this string s)
        {
            return Regex.Replace(s, @"(\B[A-Z]|[0-9]+)", " $1"); //ChangesThisTo Changes This To 
        }

        public static string GetDescriptionString(this Enum val)
        {
            try
            {
                var attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

                return attributes.Length > 0 ? attributes[0].Description : val.ToString().PascalCaseToPrettyString();
            }
            catch (Exception)
            {
                return val.ToString().PascalCaseToPrettyString();
            }
        }
    }
}
