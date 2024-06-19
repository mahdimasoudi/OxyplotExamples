using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using Charts.Droid;
using Java.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(PdfOpener))]
namespace Charts.Droid
{
    internal class PdfOpener : IPdfOpener
    {
        public void OpenPdf(string filePath)
        {
            var context = Android.App.Application.Context;
            var file = new File(filePath);
            var uri = FileProvider.GetUriForFile(context, context.PackageName + ".fileprovider", file);
            var intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(uri, "application/pdf");
            intent.AddFlags(ActivityFlags.GrantReadUriPermission);
            intent.AddFlags(ActivityFlags.NewTask);
            context.StartActivity(intent);
        }
    }
}