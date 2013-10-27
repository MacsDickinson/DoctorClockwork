using System;
using System.Net;
using Clockwork;

namespace DrClockwork.Domain.Logic
{
    public class ClockworkSMS
    {
        public static void Send(string to, string content)
        {
            try
            {
                var api = new API("4832b0bded15eb58c54bb7d3cf01a08029bb41d8");
                var sms = new SMS { To = to, Message = content };
                var result = api.Send(sms);

                if (result.Success)
                {
                    //Console.WriteLine("SMS Sent to {0}, Clockwork ID: {1}", result.SMS.To, result.ID);
                }
                else
                {
                    //Console.WriteLine("SMS to {0} failed, Clockwork Error: {1} {2}", result.SMS.To, result.ErrorCode, result.ErrorMessage);
                }
            }
            catch (APIException ex)
            {
                // You'll get an API exception for errors
                // such as wrong username or password
                //Console.WriteLine("API Exception: " + ex.Message);
            }
            catch (WebException ex)
            {
                // Web exceptions mean you couldn't reach the Clockwork server
                //Console.WriteLine("Web Exception: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                // Argument exceptions are thrown for missing parameters,
                // such as forgetting to set the username
                //Console.WriteLine("Argument Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Something else went wrong, the error message should help
                //Console.WriteLine("Unknown Exception: " + ex.Message);
            }
        }
    }
}
