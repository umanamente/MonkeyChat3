﻿using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MonkeyChat3.Helpers;
using Xamarin.Forms.Platform.Android;

namespace MonkeyChat3.Droid
{
    [Activity(Label = "@string/ApplicationName",
              Icon = "@mipmap/ic_launcher",
              Theme = "@style/MyTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App(new AndroidInitializer(Application)));
        }
    }
}