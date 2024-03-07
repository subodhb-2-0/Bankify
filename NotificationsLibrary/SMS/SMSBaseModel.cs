using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationsLibrary.SMS
{
	public class SMSBaseModel
	{
		public string MobileNo { get; set; }
		public string Message { get; set; }
		public string MessageType { get; set; }
	}
}
