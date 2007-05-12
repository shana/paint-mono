/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.Actions;
using PaintDotNet.SystemLayer;
using PaintDotNet.Updates;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PaintDotNet.Menus
{
    public sealed class HelpMenu
        : PdnMenuItem
    {
        private PdnMenuItem menuHelpHelpTopics;
        private ToolStripSeparator menuHelpSeparator1;
        private PdnMenuItem menuHelpPdnWebsite;
        private PdnMenuItem menuHelpDonate;
        private PdnMenuItem menuHelpForum;
        private PdnMenuItem menuHelpTutorials;
        private PdnMenuItem menuHelpPlugins;
        private PdnMenuItem menuHelpSendFeedback;
        private ToolStripSeparator menuHelpSeparator2;
        private PdnMenuItem menuHelpLanguage;
        private PdnMenuItem menuHelpLanguageSentinel;
        private CheckForUpdatesMenuItem menuHelpCheckForUpdates;
        private ToolStripSeparator menuHelpSeparator3;
        private PdnMenuItem menuHelpAbout;

        public void CheckForUpdates()
        {
            this.menuHelpCheckForUpdates.PerformClick();
        }

        public HelpMenu()
        {
            InitializeComponent();
        }

        protected override void OnAppWorkspaceChanged()
        {
            this.menuHelpCheckForUpdates.AppWorkspace = this.AppWorkspace;
            base.OnAppWorkspaceChanged();
        }

        private void InitializeComponent()
        {
            this.menuHelpHelpTopics = new PdnMenuItem();
            this.menuHelpSeparator1 = new ToolStripSeparator();
            this.menuHelpPdnWebsite = new PdnMenuItem();
            this.menuHelpDonate = new PdnMenuItem();
            this.menuHelpForum = new PdnMenuItem();
            this.menuHelpTutorials = new PdnMenuItem();
            this.menuHelpPlugins = new PdnMenuItem();
            this.menuHelpSendFeedback = new PdnMenuItem();
            this.menuHelpSeparator2 = new ToolStripSeparator();
            this.menuHelpLanguage = new PdnMenuItem();
            this.menuHelpLanguageSentinel = new PdnMenuItem();
            this.menuHelpCheckForUpdates = new CheckForUpdatesMenuItem();
            this.menuHelpSeparator3 = new ToolStripSeparator();
            this.menuHelpAbout = new PdnMenuItem();
            //
            // HelpMenu
            //
            this.DropDownItems.AddRange(
                new ToolStripItem[]
                {
                    this.menuHelpHelpTopics,
                    this.menuHelpSeparator1,
                    this.menuHelpPdnWebsite,
                    this.menuHelpDonate,
                    this.menuHelpForum,
                    this.menuHelpTutorials,
                    this.menuHelpPlugins,
                    this.menuHelpSendFeedback,
                    this.menuHelpSeparator2,
                    this.menuHelpLanguage,
                    this.menuHelpCheckForUpdates,
                    this.menuHelpSeparator3,
                    this.menuHelpAbout
                });
            this.Name = "Menu.Help";
            this.Text = PdnResources.GetString("Menu.Help.Text");
            // 
            // menuHelpHelpTopics
            // 
            this.menuHelpHelpTopics.Name = "HelpTopics";
            this.menuHelpHelpTopics.ShortcutKeys = Keys.F1;
            this.menuHelpHelpTopics.Click += new System.EventHandler(this.MenuHelpHelpTopics_Click);
            //
            // menuHelpPdnWebsite
            //
            this.menuHelpPdnWebsite.Name = "PdnWebsite";
            this.menuHelpPdnWebsite.Click += new EventHandler(MenuHelpPdnWebsite_Click);
            //
            // menuHelpDonate
            //
            this.menuHelpDonate.Name = "Donate";
            this.menuHelpDonate.Click += new EventHandler(MenuHelpDonate_Click);
            this.menuHelpDonate.Font = Utility.CreateFont(this.menuHelpDonate.Font.Name, this.menuHelpDonate.Font.Size, this.menuHelpDonate.Font.Style | FontStyle.Italic);
            //
            // menuHelpForum
            //
            this.menuHelpForum.Name = "Forum";
            this.menuHelpForum.Click += new EventHandler(MenuHelpForum_Click);
            //
            // menuHelpTutorials
            //
            this.menuHelpTutorials.Name = "Tutorials";
            this.menuHelpTutorials.Click += new EventHandler(MenuHelpTutorials_Click);
            //
            // menuHelpPlugins
            //
            this.menuHelpPlugins.Name = "Plugins";
            this.menuHelpPlugins.Click += new EventHandler(MenuHelpPlugins_Click);
            //
            // menuHelpSendFeedback
            //
            this.menuHelpSendFeedback.Name = "SendFeedback";
            this.menuHelpSendFeedback.Click += new EventHandler(MenuHelpSendFeedback_Click);
            //
            // menuHelpLanguage
            //
            this.menuHelpLanguage.Name = "Language";
            this.menuHelpLanguage.DropDownItems.AddRange(
                new ToolStripItem[] 
                {
                    this.menuHelpLanguageSentinel
                });
            this.menuHelpLanguage.DropDownOpening += new EventHandler(MenuHelpLanguage_DropDownOpening);
            // 
            // menuHelpLanguageSentinel
            //
            this.menuHelpLanguageSentinel.Text = "(sentinel)";
            //
            // menuHelpCheckForUpdates
            //
            // (left blank on purpose)

            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Name = "About";
            this.menuHelpAbout.Click += new System.EventHandler(this.MenuHelpAbout_Click);
        }

        private void MenuHelpDonate_Click(object sender, EventArgs e)
        {
            PdnInfo.LaunchWebSite(AppWorkspace, InvariantStrings.DonatePageHelpMenu);
        }

        private void MenuHelpPdnWebsite_Click(object sender, EventArgs e)
        {
            PdnInfo.LaunchWebSite(AppWorkspace, InvariantStrings.WebsitePageHelpMenu);
        }

        private void MenuHelpForum_Click(object sender, EventArgs e)
        {
            PdnInfo.LaunchWebSite(AppWorkspace, InvariantStrings.ForumPageHelpPage);
        }

        private void MenuHelpTutorials_Click(object sender, EventArgs e)
        {
            PdnInfo.LaunchWebSite(AppWorkspace, InvariantStrings.TutorialsPageHelpPage);
        }

        private void MenuHelpPlugins_Click(object sender, EventArgs e)
        {
            PdnInfo.LaunchWebSite(AppWorkspace, InvariantStrings.PluginsPageHelpPage);
        }

        private void MenuHelpAbout_Click(object sender, System.EventArgs e)
        {
            using (AboutDialog af = new AboutDialog())
            {
                af.ShowDialog(AppWorkspace);
            }
        }

        private void MenuHelpHelpTopics_Click(object sender, System.EventArgs e)
        {
            Utility.ShowHelp(AppWorkspace);
        }

        private class MenuTitleAndLocale
        {
            public string title;
            public string locale;

            public MenuTitleAndLocale(string title, string locale)
            {
                this.title = title;
                this.locale = locale;
            }
        }

        private string GetCultureInfoName(CultureInfo ci)
        {
            CultureInfo en_US = new CultureInfo("en-US");

            // For "English (United States)" we'd rather just display "English"
            if (ci.Equals(en_US))
            {
                return GetCultureInfoName(ci.Parent);
            }
            else
            {
                return ci.NativeName;
            }
        }

        private void MenuHelpLanguage_DropDownOpening(object sender, EventArgs e)
        {
            this.menuHelpLanguage.DropDownItems.Clear();

            string[] locales = PdnResources.GetInstalledLocales();

            MenuTitleAndLocale[] mtals = new MenuTitleAndLocale[locales.Length];

            for (int i = 0; i < locales.Length; ++i)
            {
                string locale = locales[i];
                CultureInfo ci = new CultureInfo(locale);
                mtals[i] = new MenuTitleAndLocale(ci.DisplayName, locale);
            }

            Array.Sort(
                mtals,
                delegate(MenuTitleAndLocale x, MenuTitleAndLocale y)
                {
                    return string.Compare(x.title, y.title, StringComparison.InvariantCultureIgnoreCase);
                });

            foreach (MenuTitleAndLocale mtal in mtals)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem();
                menuItem.Text = GetCultureInfoName(new CultureInfo(mtal.locale));
                menuItem.Tag = mtal.locale;
                menuItem.Click += new EventHandler(LanguageMenuItem_Click);

                if (0 == string.Compare(mtal.locale, CultureInfo.CurrentUICulture.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    menuItem.Checked = true;
                }

                this.menuHelpLanguage.DropDownItems.Add(menuItem);
            }
        }

        private void LanguageMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem miwt = (ToolStripMenuItem)sender;
            string newLocaleName = (string)miwt.Tag;
            PdnResources.SetNewCulture(AppWorkspace, newLocaleName);
        }

        private void MenuHelpSendFeedback_Click(object sender, EventArgs e)
        {
            AppWorkspace.PerformAction(new SendFeedbackAction());
        }
    }
}
