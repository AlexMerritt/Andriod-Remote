using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroidRemote.Util
{
    class AppManager
    {
        Dictionary<string, App> apps;
        List<string> appNames;

        public List<string> AppNames
        {
            get
            {
                return appNames;
            }
        }

        public AppManager()
        {
            apps = new Dictionary<string, App>();
        }

        void UpdateAppsNames()
        {
            appNames = new List<string>();

            foreach (KeyValuePair<string, App> value in apps)
            {
                appNames.Add(value.Key);
            }
        }

        public void CreateApp(string data)
        {
            Dictionary<string, string> appToekns = new Dictionary<string, string>();

            int currentIdx;
            int nextIdx;

            currentIdx = data.IndexOf('=');

            while (currentIdx != -1)
            {
                string token;
                string value;

                nextIdx = data.IndexOf('=', currentIdx + 1);

                token = data.Substring(currentIdx - 1, 1);

                if (nextIdx == -1)// We are at the end of the string
                {
                    value = data.Substring(currentIdx + 1);
                }
                else
                {
                    value = data.Substring(currentIdx + 1, nextIdx - currentIdx - 2);
                }

                appToekns.Add(token, value);

                currentIdx = nextIdx;
            }


            if (appToekns.ContainsKey("t") && appToekns.ContainsKey("i"))
            {
                AddApp(Convert.ToInt32(appToekns["i"]), appToekns["t"]);
            }

            UpdateAppsNames();
        }

        public void AddApp(App app)
        {
            apps.Add(app.name, app);

            UpdateAppsNames();
        }

        public void AddApp(int id, string name)
        {
            AddApp(new App(id, name));
        }

        public App FindApp(string name)
        {
            if (apps.ContainsKey(name))
            {
                return apps[name];
            }
            else
            {
                return null;
            }
        }

        public App FindApp(int index)
        {
            return apps[appNames[index]];
        }
    }
}