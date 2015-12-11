using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace PhoneBook.Src.Framework {


    class Util {

        public static void sendToastNotification(string notificationText) {

   
            var template = ToastTemplateType.ToastText01;
            var xml = ToastNotificationManager.GetTemplateContent(template);
            xml.DocumentElement.SetAttribute("launch", "Args");

      
            var text = xml.CreateTextNode(notificationText);
            var elements = xml.GetElementsByTagName("text");
            elements[0].AppendChild(text);
            
            var toast = new ToastNotification(xml);
            var notifier = ToastNotificationManager.CreateToastNotifier();
            notifier.Show(toast);
        }

        public static void refreshNumericBadge(int number) {

            XmlDocument badgeXml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);
            XmlElement badgeElement = (XmlElement)badgeXml.SelectSingleNode("/badge");
            badgeElement.SetAttribute("value", number.ToString());
            
            BadgeNotification badge = new BadgeNotification(badgeXml);
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);
        }

        public static void refreshTile(string name) {

            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150ImageAndText01);
          

            XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");
            tileTextAttributes[0].InnerText = "Hello World! My very own tile notification";

            XmlDocument squareTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text04);
            XmlNodeList squareTileTextAttributes = squareTileXml.GetElementsByTagName("text");
            squareTileTextAttributes[0].AppendChild(squareTileXml.CreateTextNode(name));
            IXmlNode node = tileXml.ImportNode(squareTileXml.GetElementsByTagName("binding").Item(0), true);
            tileXml.GetElementsByTagName("visual").Item(0).AppendChild(node);

            TileNotification tileNotification = new TileNotification(tileXml);

            tileNotification.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(10);

            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
           // TileUpdateManager.CreateTileUpdaterForApplication().StartPeriodicUpdate(new Uri(tileXml.ToString()),PeriodicUpdateRecurrence.HalfHour);


        }



        public static bool isValidEmail(string inputEmail) {

            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex re = new Regex(strRegex);

            if (re.IsMatch(inputEmail))
                return true;
            else
                return false;
        }

        public static string phoneNumberMask(string phoneNumber) {

            string phone = phoneNumber;

            if (phone.Length == 1)
                phone = "(" + phone;
            else if (phone.Length == 3)
                phone = phone + ")";

            return phone;

        }

        public static MessageDialog getDialog(string title, string message) {

            MessageDialog dialog = new MessageDialog(message, title);
            return dialog;

        }

        public static DependencyObject FindChildByName(DependencyObject from, string name) {

            int count = 0;

            if(from != null) 
               count = VisualTreeHelper.GetChildrenCount(from);

            


            for (int i = 0; i < count; i++) {

                var child = VisualTreeHelper.GetChild(from, i);
                if (child is FrameworkElement && ((FrameworkElement)child).Name == name)
                    return child;

                var result = FindChildByName(child, name);
                if (result != null)
                    return result;
            }

            return null;
        }

    }
}
