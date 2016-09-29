﻿using System;
using System.Collections.Generic;
using System.IO;
using Foundation;
using HydrantWiki.iOS.Managers;
using UIKit;
using XLabs.Forms;

namespace HydrantWiki.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : XFormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init();

            string rootAppFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string dataFolder = Path.Combine(rootAppFolder, "Library", "HWMobile");

            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            LoadApplication(new HydrantWikiApp(dataFolder, new PlatformManager()));

            return base.FinishedLaunching(app, options);
        }
    }
}
