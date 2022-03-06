using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ProjectMVC.Helpers {
    public class CustomHelpers {
        public static HtmlString NavLink(string target, string name) {
            return new HtmlString(String.Format(
                "<li class='nav-item '>" +
                "   <a class='nav-link text-white' href='{0}'>{1}</a>" +
                "</li>"
                , target, name));
        }

        public static HtmlString Create(string target) {
            return new HtmlString(String.Format(
                "<p>" +
                "   <a class=\"btn-sm btn\" style=\"background-color:#8F0097;color:white\" href='/{0}/Create'>ADD</a>" +
                "</p>"
                , target));
        }
       
        public static HtmlString Details(string target, int id) {
            return new HtmlString(String.Format(
                "<a class=\" btn-sm btn\" style=\"background-color:#8F0097;color:white\" href='/{0}/Details/{1}'>SHOW</a> &nbsp;"
                , target, id));
        }

        public static HtmlString Edit(string target, int id) {
            return new HtmlString(String.Format(
                "<a class=\"btn-sm btn\" style=\"background-color:#8F0097;color:white\"  href='/{0}/Edit/{1}'>EDIT</a> &nbsp;"
                , target, id));
        }

        public static HtmlString Control(string target, int id) {
            return new HtmlString(String.Format(
                "<a class=\"btn-sm btn\" style=\"background-color:#8F0097;color:white\" href='/{0}/Edit/{1}'>EDIT</a> &nbsp;" +
                "<a class=\" btn-sm btn\" style=\"background-color:#8F0097;color:white\" href='/{0}/Delete/{1}'>DELETE</a>"
                , target, id));
        }
    }
}
