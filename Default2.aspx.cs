using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        NotificationService service = new NotificationService(true, "c:\\aps_developer_identity.p12", "password!123", 1);



        service.SendRetries = 5;

        service.ReconnectDelay = 5000;



        service.Error += new NotificationService.OnError(service_Error);

        service.NotificationTooLong += new NotificationService.OnNotificationTooLong(service_NotificationTooLong);



        service.BadDeviceToken += new NotificationService.OnBadDeviceToken(service_BadDeviceToken);

        service.NotificationFailed += new NotificationService.OnNotificationFailed(service_NotificationFailed);

        service.NotificationSuccess += new NotificationService.OnNotificationSuccess(service_NotificationSuccess);

        service.Connecting += new NotificationService.OnConnecting(service_Connecting);

        service.Connected += new NotificationService.OnConnected(service_Connected);

        service.Disconnected += new NotificationService.OnDisconnected(service_Disconnected);



        Notification alertNotification = new Notification("b0a2f562eeb993d98961834abecad051f7804132b5b058509eaab396d546414a");



        alertNotification.Payload.Alert.Body = string.Format("Testing...");

        alertNotification.Payload.Sound = "default";

        alertNotification.Payload.Badge = 1;



        if (service.QueueNotification(alertNotification))

            Console.WriteLine("Notification Queued!");

        else

            Console.WriteLine("Notification Failed to be Queued!");



        Console.WriteLine("Cleaning Up...");



        service.Close();

        service.Dispose();



        Console.WriteLine("Done!");

        Console.ReadLine();
    }


     static void service_BadDeviceToken(object sender, BadDeviceTokenException ex)
            {

                Console.WriteLine("Bad Device Token: {0}", ex.Message);

            }

     

            static void service_Disconnected(object sender)

            {

                Console.WriteLine("Disconnected...");

            }

     static void service_Connected(object sender)

        {

           Console.WriteLine("Connected...");

         }

 

        static void service_Connecting(object sender)

         {

            Console.WriteLine("Connecting...");

       }

         static void service_NotificationTooLong(object sender, NotificationLengthException ex)

       {

            Console.WriteLine(string.Format("Notification Too Long: {0}", ex.Notification.ToString()));

         }



      static void service_NotificationSuccess(object sender, Notification notification)

        {

            Console.WriteLine(string.Format("Notification Success: {0}", notification.ToString()));

       }



      static void service_NotificationFailed(object sender, Notification notification)

       {

            Console.WriteLine(string.Format("Notification Failed: {0}", notification.ToString()));

        }

    static void service_Error(object sender, Exception ex)
         {

            Console.WriteLine(string.Format("Error: {0}", ex.Message));

           }
}