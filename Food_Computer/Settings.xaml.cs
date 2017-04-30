using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Email;
using LightBuzz.SMTP;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Food_Computer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
            var subject = "test message";

            var body = "this is a test";

            var recipient = "lschuelk@mymail.mines.edu";

            var emailManager = new MessagingManager.EMailManager();

            emailManager.SendEmail(subject, body, recipient);

        }

       


    }
}

           