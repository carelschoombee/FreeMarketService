﻿using FreeMarket.Models;
using System.Web.Mvc;

namespace FreeMarket
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new FreeMarketErrorHandler());
        }
    }
}
