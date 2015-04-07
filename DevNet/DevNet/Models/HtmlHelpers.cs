using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;
using System.Text;


namespace DevNet.Models
{
    public static class HtmlHelpers
    {
        #region Public

        /// <summary>
        /// Function returns an IMG Html encode tag
        /// </summary>
        public static MvcHtmlString Image(this HtmlHelper helper, string id, string url, string alternateText)
        {
            return Image(helper, id, url, alternateText, null);
        }

        /// <summary>
        /// Function returns an IMG Html encode tag with htmlAttributes
        /// </summary>
        public static MvcHtmlString Image(this HtmlHelper helper, string id, string url, string alternateText, object htmlAttributes)
        {
            // Instantiate a UrlHelper 
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            // Create tag builder
            var builder = new TagBuilder("img");

            // Create valid id
            builder.GenerateId(id);

            // Add attributes
            builder.MergeAttribute("src", urlHelper.Content(url));
            builder.MergeAttribute("alt", alternateText);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            // Render tag
            var ret = new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));

            return ret;
        }

        /// <summary>
        /// HTML Helper returns a percent bar.
        /// NOTE:  Max percentage value (regarless of what is passed in) will be 100%
        /// </summary>
        public static MvcHtmlString PercentBarHorizontal(this HtmlHelper helper, int pbGood, int pbTotal, bool pbInverseBar, string pbAlignment, string pbWidth)
        {
            double pbPercent = 0;
            StringBuilder sb = new StringBuilder();

            try
            {
                if (pbTotal > 0)
                {
                    pbPercent = pbGood / ((pbTotal) * 1.0);

                    if (pbInverseBar)
                        pbPercent = (1.0 - pbPercent) * 100;
                    else
                        pbPercent = pbPercent * 100;

                    pbPercent = pbPercent > 100 ? 100 : pbPercent;
                }

                sb.Append("<div align=\"" + pbAlignment + "\">");
                sb.Append("<div class=\"percentBar\" style=\"width: " + pbWidth + "%;\">");

                if (pbTotal > 0)
                {
                    sb.Append("<div class=\"percentBarBad\">&nbsp;</div>");
                    sb.Append("<div class=\"percentBarGood\" style=\"width: " + (int)pbPercent + "%;\">&nbsp;</div>");
                }

                sb.Append("<div class=\"percentBarLabel\">");
                sb.Append(pbPercent.ToString("##0.0") + "%");
                sb.Append("</div>");

                sb.Append("</div>");
                sb.Append("</div>");

                pbInverseBar = false;
                pbGood = 0;
                pbTotal = 0;
            }
            catch (Exception) { return new MvcHtmlString(string.Empty); }

            return new MvcHtmlString(sb.ToString());
        }

        /// <summary>
        /// Checks for empty string, if empty or null returns DefaultIfEmpty string value.
        /// </summary>
        public static string GetCommonStringFormat(this string helper, string DefaultIfEmpty = "-")
        {
            return !string.IsNullOrEmpty(helper) ? helper : DefaultIfEmpty;
        }

        /// <summary>
        /// Removes any unsafe characters from the string and encodes using AntiXSS.  If empty string, returns DefaultValue.
        /// </summary>
        public static string SafeString(this string helper, string sDefaultValue = "...")
        {
            return Microsoft.Security.Application.Encoder.HtmlEncode(!string.IsNullOrEmpty(helper) ? Regex.Replace(helper, @"<[^>]+>|&nbsp;", "").Trim().Replace(@"\", "") : sDefaultValue, false);
        }


        #endregion
    }
}