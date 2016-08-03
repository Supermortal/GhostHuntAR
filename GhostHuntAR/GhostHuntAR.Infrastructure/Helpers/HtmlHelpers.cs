using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GhostHuntAR.Infrastructure.Helpers
{
    public static class HtmlHelpers
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(HtmlHelpers));

        public static MvcHtmlString StatePicker(this HtmlHelper html, string statePickerName)
        {
            try
            {
                var div = new TagBuilder("div");
                div.Attributes.Add(new KeyValuePair<string, string>("style", "display: inline;"));

                var stateSelect = new TagBuilder("select");
                stateSelect.Attributes.Add(new KeyValuePair<string, string>("name", statePickerName));
                stateSelect.Attributes.Add(new KeyValuePair<string, string>("style", "display: inline;"));

                var option = new TagBuilder("option");
                option.InnerHtml = "Alabama";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Alaska";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Arizona";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Arkansas";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "California";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Colorado";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Connecticut";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Delaware";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Florida";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Georgia";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Hawaii";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Idaho";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Illinois";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Indiana";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Iowa";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Kansas";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Kentucky";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Louisiana";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Maine";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Maryland";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Massachusetts";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Michigan";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Minnesota";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Mississippi";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Missouri";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Montana";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Nebraska";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Nevada";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "New Hampshire";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "New Jersey";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "New Mexico";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "New York";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "North Carolina";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "North Dakota";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Ohio";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Oklahoma";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Oregon";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Pennsylvania";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Rhode Island";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "South Carolina";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "South Dakota";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Tennessee";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Texas";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Utah";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Vermont";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Virginia";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Washington";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Washington D.C.";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "West Virginia";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Wisconsin";
                stateSelect.InnerHtml += option;

                option = new TagBuilder("option");
                option.InnerHtml = "Wyoming";
                stateSelect.InnerHtml += option;

                div.InnerHtml += stateSelect;

                return new MvcHtmlString(div.ToString());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }
    }
}