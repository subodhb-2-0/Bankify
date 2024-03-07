using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static NotificationsLibrary.SMS.SMSCommon;

namespace NotificationsLibrary.SMS
{
	public class YieldPlusSMSProvider : ISMSNotificationInterface
	{
		public bool SendMessage(string MobileNo, string smsType, dynamic parameters)
		{
			StringBuilder _request = new StringBuilder();
			YieldPlusModel _model = new YieldPlusModel();

			StringBuilder _message = new StringBuilder();
			long enumValue = 0;

			string _amount = string.Empty;
			string _distributor = string.Empty;
			string _updatedon = string.Empty;
			string _crn = string.Empty;
			string _transactionid = string.Empty;
			string _otp = string.Empty;
			string _complaintid = string.Empty;
			string _verificationcode = string.Empty;
			string _username = string.Empty;
			string _password = string.Empty;
			string _operator = string.Empty;
			string _bankaccount = string.Empty;
			string _receivername = string.Empty;
			string _bankname = string.Empty;
			string _contactnumber = string.Empty;

			//code added by swapnal to process with validation and exact msg string

			SMStype _smstype = (SMStype)Enum.Parse(typeof(SMStype), smsType);
			switch (_smstype)
			{
				case SMStype.Working_Capital_Limit:
					// You have received the limit transfer of Rs{#var#}from Dist{#var#}on{#var#} - Team SevenPay
					Console.WriteLine("Working_Capital_Limit");
					enumValue = ((long)SMStype.Working_Capital_Limit);

					//parameters
					_amount = parameters.GetType().GetProperty("Amount").GetValue(parameters, null);
					_distributor = parameters.GetType().GetProperty("Distributor").GetValue(parameters, null);
					_updatedon = parameters.GetType().GetProperty("UpdatedOn").GetValue(parameters, null);

					_message.Append("You have received the limit transfer of Rs");
					_message.Append(_amount.ToString());
					_message.Append("from Dist");
					_message.Append(_distributor);
					_message.Append("on");
					_message.Append(_updatedon);
					_message.Append(" - Team SevenPay");
					break;
				case SMStype.Electricty_bill_Payment:
					//	The electricity bill payment of Rs{#var#}is successful for CRN{#var#} Transaction ID{#var#} - Team SevenPay
					Console.WriteLine("Electricty_bill_Payment");
					enumValue = ((long)SMStype.Electricty_bill_Payment);

					//parameters
					_amount = parameters.GetType().GetProperty("Amount").GetValue(parameters, null);
					_crn = parameters.GetType().GetProperty("CRN").GetValue(parameters, null);
					_transactionid = parameters.GetType().GetProperty("TransactionId").GetValue(parameters, null);

					_message.Append("The electricity bill payment of Rs ");
					_message.Append(_amount.ToString());
					_message.Append(" is successful for CRN ");
					_message.Append(_crn);
					_message.Append(" Transaction ID ");
					_message.Append(_transactionid);
					_message.Append(" - Team SevenPay");
					break;
				case SMStype.Addsender1:
					//	Dear Partner, OTP to add a sender on 7Pay is {#var#}
					Console.WriteLine("Addsender1");
					enumValue = ((long)SMStype.Addsender1);

					//parameters
					_otp = parameters.GetType().GetProperty("OTP").GetValue(parameters, null);

					_message.Append("Dear Partner, OTP to add a sender on 7Pay is ");
					_message.Append(_otp);
					break;
				case SMStype.Logiin:
					//	Dear Partner, your 7Pay Login OTP is {#var#}. 
					Console.WriteLine("Logiin");
					enumValue = ((long)SMStype.Logiin);

					//parameters
					_otp = parameters.GetType().GetProperty("OTP").GetValue(parameters, null);

					_message.Append("Dear Partner, your 7Pay Login OTP is ");
					_message.Append(_otp);
					_message.Append(".");
					break;
				case SMStype.Complaint_status:
					//	"Dear SevenPay customer, your complaint{#var#}is still openWe will revert to you shortly Thank you for being with us- Team SevenPay"
					Console.WriteLine("Complaint_status");
					enumValue = ((long)SMStype.Complaint_status);

					//parameters
					_complaintid = parameters.GetType().GetProperty("ComplaintId").GetValue(parameters, null);

					_message.Append("Dear SevenPay customer, your complaint ");
					_message.Append(_complaintid);
					_message.Append(" is still openWe will revert to you shortly Thank you for being with us- Team SevenPay");
					break;
				case SMStype.Deactivate_receiver_OTP:
					//	"Please use this verification code{#var#}to proceed with deactivation -Team  SevenPay"
					Console.WriteLine("Deactivate_receiver_OTP");
					enumValue = ((long)SMStype.Deactivate_receiver_OTP);

					//parameters
					_otp = parameters.GetType().GetProperty("OTP").GetValue(parameters, null);

					_message.Append("Please use this verification code ");
					_message.Append(_otp);
					_message.Append(" to proceed with deactivation -Team  SevenPay");
					break;
				case SMStype.Dist_WCD_transfer:
					//	"	Rs{#var#}has been transferred from distributor{#var#}on{#var#}at{#var#} - Team SevenPay"
					Console.WriteLine("Dist_WCD_transfer");
					enumValue = ((long)SMStype.Dist_WCD_transfer);

					//parameters
					_amount = parameters.GetType().GetProperty("Amount").GetValue(parameters, null);
					_distributor = parameters.GetType().GetProperty("Distributor").GetValue(parameters, null);
					_complaintid = parameters.GetType().GetProperty("OTP").GetValue(parameters, null);
					_complaintid = parameters.GetType().GetProperty("OTP").GetValue(parameters, null);

					_message.Append("Rs ");
					_message.Append(_amount);
					_message.Append(" has been transferred from distributor ");
					_message.Append(_distributor);
					_message.Append(" on ");
					_message.Append(_updatedon);
					_message.Append(" at ");
					_message.Append(_complaintid);
					_message.Append(" - Team SevenPay");
					break;
				case SMStype.Password_reset:
					//	"	Your password reset for SevenPay was successful for any assistance contact SevenPay{#var#}"
					Console.WriteLine("Password_reset");
					enumValue = ((long)SMStype.Password_reset);

					//parameters
					_contactnumber = parameters.GetType().GetProperty("ContactNumber").GetValue(parameters, null);

					_message.Append("Your password reset for SevenPay was successful for any assistance contact SevenPay ");
					_message.Append(_contactnumber);
					break;
				case SMStype.POS_User_login:
					//	"	Dear Channel Partner, use the login details Username{#var#} Password{#var#} for experiencing SevenPay -Team SevenPay"
					Console.WriteLine("POS_User_login");
					enumValue = ((long)SMStype.POS_User_login);

					//parameters
					_username = parameters.GetType().GetProperty("UserName").GetValue(parameters, null);
					_password = parameters.GetType().GetProperty("Password").GetValue(parameters, null);

					_message.Append("Dear Channel Partner, use the login details Username ");
					_message.Append(_username);
					_message.Append(" Password ");
					_message.Append(_password);
					_message.Append(" for experiencing SevenPay -Team SevenPay");
					break;
				case SMStype.Registration01:
					//	"	Dear Channel Partner, use the login details Username{#var#} Password{#var#} for experiencing SevenPay -Team SevenPay"
					Console.WriteLine("Registration01");
					enumValue = ((long)SMStype.Registration01);

					//parameters
					_username = parameters.GetType().GetProperty("UserName").GetValue(parameters, null);
					_password = parameters.GetType().GetProperty("Password").GetValue(parameters, null);

					_message.Append("Dear Channel Partner, use the login details for Username ");
					_message.Append(_username);
					_message.Append(" Password ");
					_message.Append(_password);
					_message.Append(" for experiencing SevenPay -Team 7X Fintech Private Limited.");
					break;
				//case SMStype.POS_User_login:
				//    //	"	Dear Channel Partner, use the login details Username{#var#} Password{#var#} for experiencing SevenPay -Team SevenPay"
				//    Console.WriteLine("POS_User_login");
				//    enumValue = ((long)SMStype.POS_User_login);

				//    //parameters
				//    _username = parameters.GetType().GetProperty("UserName").GetValue(parameters, null);
				//    _password = parameters.GetType().GetProperty("Password").GetValue(parameters, null);

				//    _message.Append("Dear Channel Partner, use the login details Username ");
				//    _message.Append(_username);
				//    _message.Append(" Password ");
				//    _message.Append(_password);
				//    _message.Append(" for experiencing SevenPay -Team SevenPay");
				//    break;
				default:
					Console.WriteLine("Something went wrong !!!");
					break;
			}


			/*
                http://112.196.118.194/api/mt/SendSMS?
                APIKEY=3uzlXFyziUehjBzVJhzlnQ
                &senderid=SEVNPE
                &channel=Trans
                &DCS=0
                &flashsms=0
                &number=919892144614
                &text=TEMPLATE
                &route=2
                &peid=1001670115834591854
             * 
             */
			try
			{
				_request.Append(_model.BaseURL);
				_request.Append("APIKEY=");
				_request.Append(_model.APIKEY);
				_request.Append("&senderid=");
				_request.Append(_model.Senderid);
				_request.Append("&channel=Trans"); //Message channel promotional or transactional
				_request.Append("&DCS=0"); //Data coding value (Default is 0 for normal message, Set 8 for unicode sms)
				_request.Append("&flashsms=0");
				_request.Append("&number=");
				_request.Append(MobileNo);
				_request.Append("&text=");
				_request.Append(_message);
				_request.Append("&route=2");
				//_request.Append(SMSType == 0 ? int.Parse(SMSTypeEnum.Transaction).ToString() : SMSTypeEnum.OTP);

				//_request.Append(SMSType);
				//_request.Append("&Peid=");
				//_request.Append(_model.Peid);
				_request.Append("&DLTTemplateId=");
				_request.Append(enumValue);

				Console.WriteLine(_request.ToString());

				return Process(_request.ToString());
			}
			catch (Exception)
			{

				throw;
			}
		}

		public bool StatusCheck()
		{
			throw new NotImplementedException();
		}


		private string GetMessage()
		{


			return string.Empty;
		}

		private bool Process(string msg)
		{
			string responseFromServer = string.Empty;
			var requestString = new StringBuilder();
			try
			{
				HttpWebRequest webRequest = WebRequest.CreateHttp(msg);
				//webrequest.Method = "GET"; // GET is the default.

				using (var webResponse = webRequest.GetResponse())
				using (var reader = new StreamReader(webResponse.GetResponseStream()))
				{
					responseFromServer = reader.ReadToEnd();
				}

				Console.WriteLine(responseFromServer);
				dynamic getjsondata = JsonSerializer.Deserialize<dynamic>(responseFromServer);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				//End Code to log data in queue
			}
			//return responseFromServer;

			return true;
		}
	}
}
