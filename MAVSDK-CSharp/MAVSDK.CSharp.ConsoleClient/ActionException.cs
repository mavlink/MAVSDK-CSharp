﻿using System;
using Mavsdk.Rpc.Action;

namespace MAVSDK.CSharp.ConsoleClient
{
	public class ActionException : Exception
	{
		public ActionResult.Types.Result Result { get; }
		public string ResultStr { get; }

		public ActionException(ActionResult.Types.Result result, string resultStr)
		{
			Result = result;
			ResultStr = resultStr;
		}
	}
}