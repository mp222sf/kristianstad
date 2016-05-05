﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.Search;
using EPiServer.Web;
using EPiServer.Web.Hosting;
using EPiServer.Web.Mvc.Html;
using EPiServer.DataAbstraction;
using System;
using System.Text;
using System.Text.RegularExpressions;
using EPiServer.Core.Html;
using EPiServer.DynamicContent;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using EPiServer.Web.Mvc;
using Kristianstad.Models.Pages.Compare;
using Kristianstad.ViewModels.Compare;
using Kristianstad.Business.Compare;
using EPiServer;
using Kristianstad.Business.Models.Blocks.Shared;
using EPiCore.ViewModels.Pages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Kristianstad.Controllers.Compare
{
    public class OrganisationalUnitController : PageController<OrganisationalUnitPage>
    {
        private const string CookieName = "compare";
        private readonly Injected<IContentLoader> _contentLoader;
        private CookieHelper _cookieHelper;

        public OrganisationalUnitController()
        {
            _cookieHelper = new CookieHelper();
        }

        public ActionResult Full(OrganisationalUnitPage currentPage)
        {
            var model = new OrganisationalUnitPageModel(currentPage);
            var editHints = ViewData.GetEditHints<OrganisationalUnitPageModel, OrganisationalUnitPage>();

            return PartialView("Full", model);
        }

        public ActionResult Index(OrganisationalUnitPage currentPage)
        {
            var model = new OrganisationalUnitPageModel(currentPage);

            // Connect the view models logotype property to the start page's to make it editable

            var editHints = ViewData.GetEditHints<OrganisationalUnitPage, OrganisationalUnitPage>();
            editHints.AddConnection(m => m.Category, p => p.Category);
            editHints.AddConnection(m => m.StartPublish, p => p.StartPublish);

            // Checks if the CurrentPage is in the CompareList.
            foreach (int item in _cookieHelper.GetCookie(currentPage.ParentLink.ID))
            {
                if (item == currentPage.ContentLink.ID)
                {
                    ViewData.Add("cookies", currentPage.ContentLink.ID);
                }
            }

            return View(model);
        }

        public ActionResult AddOuToCompare(PageData currentPage, int id, string redirectBackTo = null)
        {
            _cookieHelper.AddCookie(currentPage.ParentLink.ID, id);

            if (!string.IsNullOrWhiteSpace(redirectBackTo))
            {
                return Redirect(redirectBackTo);
            }

            return RedirectToAction("Index");
        }
    }
}
