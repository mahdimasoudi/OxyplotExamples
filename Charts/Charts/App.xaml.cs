using OxyPlot.Xamarin.Forms;
using OxyPlot;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Charts
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
          
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
