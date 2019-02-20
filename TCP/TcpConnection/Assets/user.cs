using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;
public class user : MonoBehaviour
{
    public GameObject aaa;
    public Transform bbb;
    public Text IP;
    bool ccc = false;
    public Text tec;
    Socket Sserver;
    IPAddress ip;
    IPEndPoint ed;
    string rec;
    string post;
    byte[] recD = new byte[1024];
    byte[] postD = new byte[1024];
    int recL;
    Thread CT;
	public void init ()
    {
        if (IP.text != null)
        {
            ip = IPAddress.Parse(IP.text);
            ed = new IPEndPoint(ip, 5566);
            CT = new Thread(new ThreadStart(SocketRecieve));
            CT.Start();
        }
	}
    void SocketConnet()
    {
        if (Sserver != null)
        {
            Sserver.Close();
        }
        Sserver = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        print("可以连接");
        Sserver.Connect(ed);

        recL = Sserver.Receive(recD);
        rec = Encoding.ASCII.GetString(recD, 0, recL);
        print(rec);
    }
    void SocketSend(string poster)
    {
        postD = new byte[1024];
        postD = Encoding.ASCII.GetBytes(poster);
        Sserver.Send(postD, postD.Length, SocketFlags.None);
    }
    void SocketRecieve()
    {
        SocketConnet();
        while (true)
        {
            recD = new byte[1024];
            recL = Sserver.Receive(recD);
            if (recL == 0)
            {
                SocketConnet();
                continue;
            }
            rec = Encoding.ASCII.GetString(recD, 0, recL);
            ccc = true;
        }
    }
    void SocketQuit()
    {
        //关闭线程
        if (CT != null)
        {
            CT.Interrupt();
            CT.Abort();
        }
        //最后关闭服务器
        if (Sserver != null)
        {
            Sserver.Close();
        }
        print("diconnect");
    }
    void Update()
    {
        if (ccc == true)
        {
            ccc = false;
            aaa.transform.GetChild(0).GetComponent<Text>().text = rec;
            Instantiate(aaa, bbb);
        }
    }
    public void Send()
    {
        SocketSend(tec.text);
    }
    void OnApplicationQuit()
    {
        SocketQuit();
    }
}
