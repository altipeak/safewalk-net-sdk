using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safewalk.helper
{
	public interface IServerConnectivityHelper
	{
		Response post(String path, Dictionary<string,string> parameters, Dictionary<string,string> headers) ;
//throw ConnectivityException;
		Response put(String path, Dictionary<string,string> parameters, Dictionary<string,string> headers) ;
//throws ConnectivityException;
		Response get(String path, Dictionary<string,string> parameters, Dictionary<string,string> headers) ;
//throws ConnectivityException;
		Response delete(String path, Dictionary<string,string> parameters, Dictionary<string,string> headers) ;
//throws ConnectivityException;

	}
}
