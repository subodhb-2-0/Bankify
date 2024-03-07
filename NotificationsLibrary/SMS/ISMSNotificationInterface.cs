using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationsLibrary.SMS
{
	public interface ISMSNotificationInterface
	{
		bool SendMessage(string MobileNo, string smsType, dynamic parameters);
		bool StatusCheck();
	}
}
