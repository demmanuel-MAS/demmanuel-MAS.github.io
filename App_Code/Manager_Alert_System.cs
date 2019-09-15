using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Manager_Alet_System;

/// <summary>
/// Summary description for Manager_Alert_System
/// </summary>
///[WebService(Namespace = "http://tempuri.org/")]
[WebService(Namespace = "https://maswebservice20190912120055.azurewebsites.net")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class Manager_Alert_System : System.Web.Services.WebService
{
    //Delerations
    string myIP;
    private static List<TcpClient> tcpClientsList = new List<TcpClient>();
    TcpClient client = new TcpClient();
    TcpListener serverSocket;
    int counter;


    public Manager_Alert_System()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        GetMyIP getIP = new GetMyIP();
        myIP = getIP.getLocalIP();

        //myIP = "https://maswebservice20190912120055.azurewebsites.net";
    }

    [WebMethod]
    public void socket_Transition()
    {
        serverSocket = new TcpListener(IPAddress.Parse(myIP), 8910);//Listen to socket
        serverSocket.Start(); //Start socket
        Console.Out.WriteLine("Server("+myIP+") Message: Server Started....");//Display message on console

        Thread threadAccept = new Thread(clientAcceptance);
        threadAccept.Start();
    }

    private void clientAcceptance()
    {
        while (true)
        {
            if (!client.Connected && tcpClientsList.Count > 0)
            {
                Console.Out.WriteLine("Client removed");
            }
            client = serverSocket.AcceptTcpClient();//connect with client
            tcpClientsList.Add(client);//Add client to list
            counter++;
            if (client.Connected)
            {
                Console.Out.WriteLine("Server Message: Server Accepted client " + counter);//Display message on console
            }
            else
            {
                Console.Out.WriteLine("Server Message: Client could not connect " + counter);//Display message on console
            }

            foreach (TcpClient myClient in tcpClientsList)
            {
                Console.Out.WriteLine(myClient.Client.RemoteEndPoint);
            }
            Thread thread = new Thread(() => serverCOM(client));
            thread.Start();
        }
    }

    private void serverCOM(TcpClient theClient)
    {
        while (true)
        {
            try
            {
                NetworkStream nwStream = theClient.GetStream();
                byte[] buffer = new byte[theClient.ReceiveBufferSize];

                int bytesRead = nwStream.Read(buffer, 0, theClient.ReceiveBufferSize);

                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Server Message:Received ---- " + dataReceived);
                nwStream.Flush();
                Broadcast(dataReceived, theClient);
            }
            catch (Exception exp)
            {
                Console.Out.WriteLine("server Message: Connection lost with client: " + exp);
                break;
            }



        }
    }


    private void Broadcast(string broadCastMessage, TcpClient excludeClient)
    {
        foreach (TcpClient connectedClients in tcpClientsList)
        {
            if (connectedClients != excludeClient)
            {
                StreamWriter sWriter = new StreamWriter(connectedClients.GetStream());
                sWriter.WriteLine("Server Message__Broadcast: " + broadCastMessage);
                sWriter.Flush();
            }
        }
    }

}
