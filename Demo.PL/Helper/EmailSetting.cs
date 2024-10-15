using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helper
{
	public static class EmailSetting
	{
		public static void SendEmail(Email email)
		{

			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("alaah2122@gmail.com", "aninysavpjiocbeo");
			client.Send("alaah2122@gmail.com", email.To, email.Title, email.Body);
		}
	}
}
