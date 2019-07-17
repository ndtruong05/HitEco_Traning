using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Timers;
using System.Net;

public class DDosAttackModule : IHttpModule
{
    #region CONFIG PROPERTY

    private static int GetInt(string key, int defaultValue)
    {
        try
        {
            int result = 0;
            if (int.TryParse(ConfigurationManager.AppSettings[key], out result))
                return result;
            else
                return defaultValue;
        }
        catch
        {
            return defaultValue;
        }
    }

    private static int BANNED_REQUESTS
    {
        get
        {
            return GetInt("BANNED_REQUESTS", 200);
        }
    }

    private static int REDUCTION_INTERVAL
    {
        get
        {
            return GetInt("REDUCTION_INTERVAL", 1000);
        }
    }

    private static int RELEASE_INTERVAL
    {
        get
        {
            return GetInt("RELEASE_INTERVAL", 300000);
        }
    }

    #endregion

    #region PRIVATE VARIABLE

    private static Dictionary<string, short> _IpAdresses = new Dictionary<string, short>();
    private static Stack<string> _BannedIP = new Stack<string>();
    private static Timer _Timer = CreateTimer();
    private static Timer _BanningTimer = CreateBanningTimer();

    #endregion

    void IHttpModule.Dispose()
    {

    }

    void IHttpModule.Init(HttpApplication context)
    {
        context.BeginRequest += new EventHandler(context_BeginRequest);
    }

    public static string GetVisitorIPAddress(bool GetLan = false)
    {
        string visitorIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (String.IsNullOrEmpty(visitorIPAddress))
            visitorIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

        if (string.IsNullOrEmpty(visitorIPAddress))
            visitorIPAddress = HttpContext.Current.Request.UserHostAddress;

        if (string.IsNullOrEmpty(visitorIPAddress) || visitorIPAddress.Trim() == "::1")
        {
            GetLan = true;
            visitorIPAddress = string.Empty;
        }

        if (GetLan)
        {
            if (string.IsNullOrEmpty(visitorIPAddress))
            {
                //This is for Local(LAN) Connected ID Address
                string stringHostName = Dns.GetHostName();
                //Get Ip Host Entry
                IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);
                //Get Ip Address From The Ip Host Entry Address List
                IPAddress[] arrIpAddress = ipHostEntries.AddressList;

                try
                {
                    visitorIPAddress = arrIpAddress[arrIpAddress.Length - 2].ToString();
                }
                catch
                {
                    try
                    {
                        visitorIPAddress = arrIpAddress[0].ToString();
                    }
                    catch
                    {
                        try
                        {
                            arrIpAddress = Dns.GetHostAddresses(stringHostName);
                            visitorIPAddress = arrIpAddress[0].ToString();
                        }
                        catch
                        {
                            visitorIPAddress = string.Empty;
                        }
                    }
                }
            }
        }
        return visitorIPAddress;
    }

    private bool isStaticFile(string input)
    {
        var externalFile = new string[13] { ".css", ".js", ".jpg", ".jpeg", ".png", ".gif", ".xml", ".svg", "woff2", "woff", "all-script", "DefaultStyle", "ContrastStyle" };

        foreach (var item in externalFile)
        {
            if (input.ToLower().Contains(item))
                return true;
        }

        return false;
    }

    private void context_BeginRequest(object sender, EventArgs e)
    {
        if (isStaticFile(HttpContext.Current.Request.Url.AbsolutePath))
            return;

        try
        {
            string ip = GetVisitorIPAddress();

            if (!string.IsNullOrEmpty(ip))
            {
                if (_BannedIP.Contains(ip))
                {
                    HttpContext.Current.Response.StatusCode = 200;
                    HttpContext.Current.Response.ContentType = "text/html; charset=utf-8";
                    HttpContext.Current.Response.WriteFile("~/ManyRequest.html");
                    HttpContext.Current.Response.Write("<center> Your IP is: " + ip + "</center>");
                    HttpContext.Current.Response.End();
                }

                CheckIPAdress(ip);
            }
        }
        catch
        {
            //do some thing not throw exception
        }
    }

    private static void CheckIPAdress(string token)
    {
        if (!_IpAdresses.ContainsKey(token))
        {
            _IpAdresses[token] = 1;
        }
        else if (_IpAdresses[token] == BANNED_REQUESTS)
        {
            _BannedIP.Push(token);
            _IpAdresses.Remove(token);
        }
        else
        {
            _IpAdresses[token]++;
        }
    }

    private static Timer CreateTimer()
    {
        Timer timer = GetTimer(REDUCTION_INTERVAL);
        timer.Elapsed += new ElapsedEventHandler(TimerElapsed);

        return timer;
    }

    private static void TimerElapsed(object sender, EventArgs e)
    {
        foreach (string key in _IpAdresses.Keys.ToList())
        {
            if (_IpAdresses.Count() == 0)
                break;

            // hung.hoang 20160516: fix unhandle value
            try
            {
                short tmp = 0;
                if (Int16.TryParse(_IpAdresses[key].ToString(), out tmp))
                {
                    _IpAdresses[key]--;
                    if (_IpAdresses[key] == 0)
                    {
                        _IpAdresses.Remove(key);

                        if (_IpAdresses.Count() == 0)
                            break;
                    }
                }
            }
            catch { }
        }
    }

    private static Timer CreateBanningTimer()
    {
        Timer banningTimer = GetTimer(RELEASE_INTERVAL);
        banningTimer.Elapsed += delegate { if (_BannedIP.Count() > 0) { _BannedIP.Pop(); } };

        return banningTimer;
    }

    private static Timer GetTimer(int Interval)
    {
        Timer timer = new Timer();
        timer.Interval = Interval;
        timer.Start();

        return timer;
    }
}