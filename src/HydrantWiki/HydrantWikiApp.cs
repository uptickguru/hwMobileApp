﻿using System;
using HydrantWiki.Forms;
using HydrantWiki.Interfaces;
using HydrantWiki.Managers;
using HydrantWiki.Objects;
using Xamarin.Forms;

namespace HydrantWiki
{
    public class HydrantWikiApp : Application
    {
        public static string DataFolder;
        public static User User { get; set; }
        public IPlatformManager m_PlatformManager;

        public HydrantWikiApp(
            string _dataFolder,
            IPlatformManager _platformManager)
        {
            DataFolder = _dataFolder;
            m_PlatformManager = _platformManager;

            HWManager manager = HWManager.GetInstance();
            manager.PlatformManager = m_PlatformManager;

            User = manager.SettingManager.GetUser();

            // The root page of your application
            MainPage = new MainForm(this);
        }

        public IPlatformManager PlatformManager
        {
            get
            {
                return m_PlatformManager;
            }
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
