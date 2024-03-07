using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationsLibrary.SMS
{
	public class YieldPlusModel : SMSBaseModel
	{
		private readonly string _user = "Bankify";
		private readonly string _password = "Kapil@1983";
		private readonly string _senderid = "SVENPY";

		private readonly string _baseurl = "http://112.196.118.194/api/mt/SendSMS?";
		private readonly string _peid = "1001670115834591854";
		private readonly string _apiKey = "3uzlXFyziUehjBzVJhzlnQ";


		public string BaseURL { get { return _baseurl; } }
		public string User { get { return _user; } } // = demo
		public string APIKEY { get { return _apiKey; } }
		public string Password { get { return _password; } } // demo123
		public string Senderid { get { return _senderid; } } // WEBSMS
		public string Channel { get; set; } // Promo
		public string DCS { get; set; } // 0 &
		public string Flashsms { get; set; } // 0
		public string Route { get; set; } //##
		public string Peid { get { return _peid; } } //##
		public string DLTTemplateId { get; set; } //##

	}
}
