﻿using AzureAPI.ActionFilters;
using System.Web;
using System.Web.Mvc;

namespace AzureAPI
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
