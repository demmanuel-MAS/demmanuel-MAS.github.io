using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MASService1._0
{
    /// <summary>
    /// Uses all resources and data to create the emergency event
    /// </summary>
    public class Activate_Emergency
    {
        List<string> search = new List<string>();
        string result;

        public string startEmergency(string filePath, string SOSName)
        {
            RandomGenerator rndGen = new RandomGenerator();
            int rnd = rndGen.generateRandomNumber();//Generate random # 

            readXMLFile rXML = new readXMLFile();
            search = rXML.readMyXMLFile(filePath, SOSName, rnd.ToString());//Returns null or email/code value

            if (!search.Contains(SOSName) && !search.Contains(rnd.ToString()))//If email and code not found
            {
                writeXMLFile wXML = new writeXMLFile();
                wXML.WriteMyXMLFile(filePath, SOSName, "Case" + "_" + rnd);//Write to XML file

                result = SOSName;
            }

            

            return result;
        }
    }
}