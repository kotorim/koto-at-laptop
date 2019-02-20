using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;
public class server : MonoBehaviour {
    private Socket Sserver;
    private Socket Cuser;
    private IPEndPoint ed;
    string rec;
    string post;
    byte[] recD = new byte[1024];
    byte[] postD = new byte[1024];
    int recL;
    Thread CT;
    void init()
    {
        ed = new IPEndPoint(IPAddress.Any, 5566);
        Sserver = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Sserver.Bind(ed);
        Sserver.Listen(20);

        CT = new Thread(new ThreadStart(SocketReceive));
    }
    void SocketConnet()
    {
        if (Cuser != null)
        {
            Cuser.Close();
        }
        print("等待接入");
        Cuser = Sserver.Accept();
        IPEndPoint ipendcilent = (IPEndPoint)Cuser.RemoteEndPoint;
        print(ipendcilent.Address.ToString() + "连接了");
        post = "欢迎进入";
        SocketSend(post);
    }
    void SocketSend(string postr)
    {
        postD = new byte[1024];
        postD = Encoding.ASCII.GetBytes(post);
        Cuser.Send(postD, postD.Length, SocketFlags.None);
    }
    void SocketReceive()
    {
        SocketConnet();
        while (true)
        {
            recD = new byte[1024];
            if (recL == 0)
            {
                SocketConnet();
                continue;
            }
            rec = Encoding.ASCII.GetString(recD, 0, recL);
            print(rec);
            post = "from server : " + rec;
            SocketSend(post);
        }
    }
    void SocketQuit()
    {
        if (Cuser != null)
        {
            Cuser.Close();
        }
        if (CT != null)
        {
            CT.Interrupt();
            CT.Abort();
        }
        Sserver.Close();
        print("关了");
    }
    void Start ()
    {
        init();
	}
    void OnApplicationQuit()
    {
        SocketQuit();
    }
}
