using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ComputerAutomationServer
{
    class ApplicationListManager
    {
        public List<AppData> ListOfApps;

        public ApplicationListManager()
        {
            ListOfApps = new List<AppData>();

            // Load up the apps file and read it's contense into the apps list
            XmlDocument doc = new XmlDocument();
            doc.Load(Util.Paths.AppsFile);

            XmlNodeList appList = doc.FirstChild.ChildNodes;

            foreach (XmlNode node in appList)
            {
                if (node.Name == "app")
                {
                    AppData appData = new AppData();

                    XmlNodeList nodeValues = node.ChildNodes;

                    foreach (XmlNode value in nodeValues)
                    {
                        if (value.Name == "name")
                        {
                            appData.name = value.InnerText;
                        }
                        else if (value.Name == "path")
                        {
                            appData.path = value.InnerText;
                        }
                        else if (value.Name == "args")
                        {
                            appData.arguments = value.InnerText;
                        }
                    }

                    ListOfApps.Add(appData);
                }
            }
        }
    }
}
