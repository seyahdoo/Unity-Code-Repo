///Author: seyahdoo(Seyyid Ahmed Doğan) 
///github: github.com/seyahdoo
///email : contact@seyahdoo.com
///
///see my other reusable unity codes at
///https://github.com/seyahdoo/Unity-Code-Repo

using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDP : MonoBehaviour {

	private const int LISTENPORT = 34456;
	private const int SENDPORT = 34457;
    
    private static Sender sender = new Sender();
    private static Receiver receiver;

	public static void SendData(string IP,int Port = SENDPORT, string Message = "HELLO"){

		sender.sendString (IP, Port, Message);

	}

	public delegate void ReceiveDataDelegate(IPAddress Adress,int Port,string Message);
	private static event ReceiveDataDelegate ReceiveDataEvent;

    public static void SubscribeReceiveData(ReceiveDataDelegate Function)
    {
        //print("Subsciber++");
        //if there is no receiver, who do i listen to???
        if(receiver == null)
        {
            receiver = new Receiver(LISTENPORT);
        }

        ReceiveDataEvent += Function;
    }

    protected static void OnApplicationQuit()
    {
        sender.Dispose();
        receiver.Dispose();
    }

    protected static void InvokeReceiveData(IPAddress Adress,int Port, string Message) {
        
        //if something is subscribed?
        if (ReceiveDataEvent != null)
        {
            ReceiveDataEvent(Adress,Port,Message);
        }

    }


    private class Receiver
    {

        // receiving Thread
        Thread receiveThread;

        private UdpClient client;
        private int port; // define > init

        public Receiver(int Port)
        {
            port = Port;

            receiveThread = new Thread(
                new ThreadStart(ReceiveData));
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        // receive thread
        private void ReceiveData()
        {
            //Debug.Log("Receiver Starting");
            client = new UdpClient(port);
            while (true)
            {

                try
                {

                    IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);
                    byte[] data = client.Receive(ref ip);

                    string text = Encoding.UTF8.GetString(data);

                    //Debug.Log("Received: " + text + " from " + ip.Address + ":" + ip.Port);
                    UDP.InvokeReceiveData(ip.Address, ip.Port, text);

                }
                catch (Exception err)
                {
                    Debug.Log(err.ToString());
                }
            }
        }

        public void Dispose()
        {
            //print("Receiver Disposed");
            receiveThread.Abort();
            client.Close();
        }

    }
    private class Sender
    {

        public Sender()
        {
            client = new UdpClient();
        }

        UdpClient client;

        public void sendString(string IP, int Port, string Message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(Message);
                client.Send(data, data.Length, IP, Port);
            }
            catch (Exception err)
            {
                Debug.Log(err.ToString());
            }
        }

        public void Dispose()
        {
            //print("Sender Disposed");
            client.Close();

        }


    }

}
