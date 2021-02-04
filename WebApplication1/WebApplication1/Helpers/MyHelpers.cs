using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace WebApplication1.Helpers
{
    public static class MyHelpers
    {
        public static HtmlString Select<T, TVal>(this IHtmlHelper html, 
            IEnumerable<T> xs, string valName, string txtName, TVal selected)
        {
            string selectMarkup = "<select class='form-control'>";
            foreach(var item in xs)
            {
                Type itemType = item.GetType();
                Object value = itemType.GetProperty(valName).GetValue(item);
                Object text = itemType.GetProperty(txtName).GetValue(item);
                if(selected.ToString() == value.ToString())
                {
                    selectMarkup += $"<option value='{value}' selected>{text}</option>\n";
                }
                else
                {
                    selectMarkup += $"<option value='{value}'>{text}</option>\n";
                }
            }
            selectMarkup += "</select>";
            return new HtmlString(selectMarkup);
        }
    }
}
