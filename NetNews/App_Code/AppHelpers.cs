using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AppHelpers.App_Code
{
    public static class ListHelper
    {
        public static HtmlString CreateList(this IHtmlHelper html, string[] items)
        {
            string result = "<ul>";
            foreach (string item in items)
            {
                result = $"{result}<li>{item}</li>";
            }
            result = $"{result}</ul>";
            return new HtmlString(result);
        }
    }

    public static class TextHelper
    {
        //removes html tags from text
        public static string StripHTML(string text)
        {
            text = Regex.Replace(text, "<.*?>", "");
            return text;
        }


        //trims text to the desired lenght passed in the parameter
        public static string FormatLongText(string text, int max_length)
        {
            if (text != null && text.Length > max_length)
            {
                int iNextSpace = text.LastIndexOf(" ", max_length, StringComparison.Ordinal);
                //text = string.Format("{0}...", text.Substring(0, (iNextSpace > 0) ? iNextSpace : max_length).Trim());
                text = $"{(text.Substring(0, (iNextSpace > 0) ? iNextSpace : max_length).Trim())}...";
            }
            return text;
        }

        //convert text case
        public static string ConvertCase(string text, string convert_to)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            switch (convert_to)
            {
                case "Upper":
                    // convert to upper case
                    return textInfo.ToUpper(text);
                case "Lower":
                    // convert to lower case
                    return textInfo.ToLower(text);
                case "Title":
                    // convert to title case
                    return textInfo.ToTitleCase(text);
                case "SplitUpper":
                    //split text by capital case
                    return Regex.Replace(text, "([A-Z])", " $1").Trim();
                default:
                    return text;
            }

        }
    }





}
