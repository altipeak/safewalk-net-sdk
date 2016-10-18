using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace safewalk.helper
{
	public class ConnectivityException : Exception
	{
		public ConnectivityException(Exception cause)
			: base(cause.Message, cause)
		{
		}
	}
}
