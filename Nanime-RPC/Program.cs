﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;
using Nanime_RPC.DiscordRpcDemo;
using System.Windows.Automation;
using System.IO;
using System.Diagnostics;

namespace Nanime_RPC
{
    class Program
    {
        static string domain_nanime = "nanime.biz ";
        static string discord_contact = "Zikes GT#2251";
        static string github_profile = "https://github.com/AditFarrel";
        static string title_nanime;
        static string episode_nanime;
        //static long timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds(); # OPTIONAL
        static DiscordRpc.EventHandlers handlers;
        static DiscordRpc.RichPresence presence;
        static void ShowSpinner()
        {
            var counter = 0;
            for (int i = 0; i < 50; i++)
            {
                switch (counter % 4)
                {
                    case 0: Console.Write("/"); break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                counter++;
                Thread.Sleep(100);
            }
        }

        static void Typewrite(string message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                Thread.Sleep(45);
            }

        }

        static void awalan_kata()
        {
            Typewrite("Made by : Adit Farrel\nDiscord : " + discord_contact + "\nGithub  : " + github_profile + "\n\n");
        }

        private static void Main()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            awalan_kata();
            Typewrite("Menghubungkan Koneksi...");
            try
            {
                WebClient a_web = new WebClient();
                string a_string = a_web.DownloadString("https://google.com");
                ShowSpinner();
                Typewrite("\nSukses");
                Thread.Sleep(2000);
                //discordRPC("idling", ""); # Removed at Version 1.1
                Console.Clear();
                pilih_menu();
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.Clear();
                Typewrite("\nTerjadi Kesalahan...");
                Thread.Sleep(2000);
                Console.Clear();
                Main();
            }

        }

        private static void pilih_menu()
        {
            awalan_kata();
            Typewrite("---------------------------");
            Typewrite("\nSilahkan Pilih Browser Anda");
            Typewrite("\n1.Firefox");
            Typewrite("\n2.Google Chrome");
            Typewrite("\n3.Microsoft Edge");
            Typewrite("\n---------------------------\n");
            Typewrite("Perhatian.....\nDiscordRPC ini Berkerja Apabila Domain Nanime\nMasih Menggunakan "+ domain_nanime + "ya:)\n");
            Typewrite("\nNomor : ");
            string input = Console.ReadLine();
            int number;
            int intTemp = Convert.ToInt32(input);

            if (intTemp > 3)
            {
                Console.Clear();
                Typewrite("\nMenu Hanya Tersedia Dari Angka 1-3 Saja!");
                Thread.Sleep(2000);
                Console.Clear();
                Main();
            }
            if (!Int32.TryParse(input, out number))
            {
                Console.Clear();
                Typewrite("\nMohon Pilih Angka Saja!");
                Thread.Sleep(2000);
                Console.Clear();
                Main();

            }

            switch (input)
            {
                case "1":
                    Console.Clear();
                    awalan_kata();
                    Typewrite("Layanan RPC untuk Firefox Berhasil Dijalankan!\nSelamat Menikmati...");
                    mozilla();
                    break;

                case "2":
                    Console.Clear();
                    awalan_kata();
                    Typewrite("Layanan RPC untuk Google Chrome Berhasil Dijalankan!\nSelamat Menikmati...");
                    chrome();
                    break;

                case "3":
                    Console.Clear();
                    awalan_kata();
                    Typewrite("Layanan RPC untuk Microsoft Edge Berhasil Dijalankan!\nSelamat Menikmati...");
                    edge();
                    break;
            }
            Console.ReadKey();
        }

        static string GetTitle(string file)
        {
            Match match = Regex.Match(file, @"<title>\s*(.+?)\s*</title>");
            if (match.Success)
            {               
                if (Regex.IsMatch(match.Value, "Nonton Anime - Nonton Streaming Anime Sub Indo"))
                {
                    return title_nanime = "Home Page";
                }

                if (Regex.IsMatch(match.Value, @"You searched for \s*(.+?)\s* - Nonton Anime"))
                {
                    Match mt = Regex.Match(match.Value, @"You searched for \s*(.+?)\s* - Nonton Anime");
                    return title_nanime = "Searching : " + mt.Groups[1].Value;
                }

                if (Regex.IsMatch(match.Value, @"Nonton Anime \s*(.+?)\s* Episode [0-9]{1,4} Sub Indo - Nonton Anime"))
                {
                    Match mt = Regex.Match(match.Value, @"Nonton Anime \s*(.+?)\s* Episode [0-9]{1,4} Sub Indo - Nonton Anime");
                    return title_nanime = mt.Groups[1].Value;
                }

                if (Regex.IsMatch(match.Value, @"Nonton Anime \s*(.+?)\s* Sub Indo - Nonton Anime"))
                {
                    Match mt = Regex.Match(match.Value, @"Nonton Anime \s*(.+?)\s* Sub Indo - Nonton Anime");
                    return title_nanime = "Looking : " + mt.Groups[1].Value;
                }
                return title_nanime;
            }
            else
            {
                //return ""; # Removed at Version 1.1
                return title_nanime; // Added at Version 1.1
            }
        }

        static string GetEpisode(string file)
        {
            Match match = Regex.Match(file, @"<title>\s*(.+?)\s*</title>");
            if (match.Success)
            {
                Match yep = Regex.Match(match.Groups[1].Value, @"Episode\s* [0-9]{1,4}");
                episode_nanime = Regex.Replace(yep.Value, "Episode", "Episode :");
                return episode_nanime;
            }
            else
            {
                //return ""; # Removed at Version 1.1
                return episode_nanime; // # Added at Version 1.1
            }
        }

        private static void mozilla()
        {
            while (true)
            {
                try
                {
                    AutomationElement root = AutomationElement.RootElement.FindFirst(TreeScope.Children,
                    new PropertyCondition(AutomationElement.ClassNameProperty, "MozillaWindowClass"));

                    Condition toolBar = new AndCondition(
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ToolBar),
                    new PropertyCondition(AutomationElement.NameProperty, "Browser tabs"));
                    var tool = root.FindFirst(TreeScope.Children, toolBar);

                    var tool2 = TreeWalker.ControlViewWalker.GetNextSibling(tool);

                    var children = tool2.FindAll(TreeScope.Children, Condition.TrueCondition);

                    foreach (AutomationElement item in children)
                    {
                        foreach (AutomationElement i in item.FindAll(TreeScope.Children, Condition.TrueCondition))
                        {
                            foreach (AutomationElement ii in i.FindAll(TreeScope.Element, Condition.TrueCondition))
                            {
                                if (ii.Current.LocalizedControlType == "edit")
                                {
                                    if (!ii.Current.BoundingRectangle.X.ToString().Contains("empty"))
                                    {
                                        ValuePattern activeTab = ii.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;

                                        var activeUrl = activeTab.Current.Value;
                                        WebClient web = new WebClient();
                                        var snya = web.DownloadString(activeUrl);;
                                        Match match = Regex.Match(snya, @"<title>\s*(.+?)\s*</title>");
                                        if (match.Success)
                                        {
                                            if (Regex.IsMatch(match.Value, "Nonton Anime - Nonton Streaming Anime Sub Indo") || Regex.IsMatch(match.Value, @"You searched for \s*(.+?)\s* - Nonton Anime") || Regex.IsMatch(match.Value, @"Nonton Anime \s*(.+?)\s* Episode [0-9]{1,4} Sub Indo - Nonton Anime") || Regex.IsMatch(match.Value, @"Nonton Anime \s*(.+?)\s* Sub Indo - Nonton Anime"))
                                            {
                                                discordRPC(GetTitle(snya), GetEpisode(snya));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception e)
                {

                }

            }
            Thread.Sleep(1000);
        }

        private static void chrome()
        {
            while (true)
            {
                try
                {
                    Process[] procsChrome = Process.GetProcessesByName("chrome");
                    foreach (Process chrome in procsChrome)
                    {
                        // the chrome process must have a window
                        if (chrome.MainWindowHandle == IntPtr.Zero)
                        {
                            continue;
                        }

                        // find the automation element
                        AutomationElement elm = AutomationElement.FromHandle(chrome.MainWindowHandle);
                        AutomationElement elmUrlBar = elm.FindFirst(TreeScope.Descendants,
                          new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));

                        // if it can be found, get the value from the URL bar
                        if (elmUrlBar != null)
                        {
                            AutomationPattern[] patterns = elmUrlBar.GetSupportedPatterns();
                            if (patterns.Length > 0)
                            {
                                ValuePattern val = (ValuePattern)elmUrlBar.GetCurrentPattern(patterns[0]);
                                //Console.WriteLine("Chrome URL found: " + val.Current.Value);
                                WebClient web = new WebClient();
                                var snya = web.DownloadString("https://" + val.Current.Value);
                                Match match = Regex.Match(snya, @"<title>\s*(.+?)\s*</title>");
                                if (match.Success)
                                {
                                    if (Regex.IsMatch(match.Value, "Nonton Anime - Nonton Streaming Anime Sub Indo") || Regex.IsMatch(match.Value, @"You searched for \s*(.+?)\s* - Nonton Anime") || Regex.IsMatch(match.Value, @"Nonton Anime \s*(.+?)\s* Episode [0-9]{1,4} Sub Indo - Nonton Anime") || Regex.IsMatch(match.Value, @"Nonton Anime \s*(.+?)\s* Sub Indo - Nonton Anime"))
                                    {
                                        discordRPC(GetTitle(snya), GetEpisode(snya));
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                }

            }
            Thread.Sleep(1000);
        }

        private static void edge()
        {
            while (true)
            {
                try
                {
                    Process[] procsChrome = Process.GetProcessesByName("msedge");
                    foreach (Process chrome in procsChrome)
                    {
                        // the chrome process must have a window
                        if (chrome.MainWindowHandle == IntPtr.Zero)
                        {
                            continue;
                        }

                        // find the automation element
                        AutomationElement elm = AutomationElement.FromHandle(chrome.MainWindowHandle);
                        AutomationElement elmUrlBar = elm.FindFirst(TreeScope.Descendants,
                          new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));

                        // if it can be found, get the value from the URL bar
                        if (elmUrlBar != null)
                        {
                            AutomationPattern[] patterns = elmUrlBar.GetSupportedPatterns();
                            if (patterns.Length > 0)
                            {
                                ValuePattern val = (ValuePattern)elmUrlBar.GetCurrentPattern(patterns[0]);
                                //Console.WriteLine("Edge URL found: " + val.Current.Value);
                                WebClient web = new WebClient();
                                var snya = web.DownloadString(val.Current.Value);
                                Match match = Regex.Match(snya, @"<title>\s*(.+?)\s*</title>");
                                if (match.Success)
                                {
                                    if (Regex.IsMatch(match.Value, "Nonton Anime - Nonton Streaming Anime Sub Indo") || Regex.IsMatch(match.Value, @"You searched for \s*(.+?)\s* - Nonton Anime") || Regex.IsMatch(match.Value, @"Nonton Anime \s*(.+?)\s* Episode [0-9]{1,4} Sub Indo - Nonton Anime") || Regex.IsMatch(match.Value, @"Nonton Anime \s*(.+?)\s* Sub Indo - Nonton Anime"))
                                    {
                                        discordRPC(GetTitle(snya), GetEpisode(snya));
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                }

            }
            Thread.Sleep(1000);
        }

        private static void discordRPC(string details, string state)
        {
            handlers = default(DiscordRpc.EventHandlers);
            DiscordRpc.Initialize("815646242041757706", ref handlers, true, null);
            handlers = default(DiscordRpc.EventHandlers);
            DiscordRpc.Initialize("815646242041757706", ref handlers, true, null);
            presence.details = details;
            presence.state = state;
            presence.largeImageKey = "logo";
            presence.smallImageKey = "";
            presence.largeImageText = title_nanime;
            //presence.startTimestamp = timestamp; # OPTIONAL
            DiscordRpc.UpdatePresence(ref presence);
        }
    }
}
