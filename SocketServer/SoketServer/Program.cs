
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using Net;
using System.IO;
using cs;


namespace SoketServer
{
    class Program
    {
        private static byte[] result = new byte[1024];
        private const int port = 8088;
        private static string IpStr = "127.0.0.1";
        private static Socket serverSocket;

        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse(IpStr);
            IPEndPoint ip_endPoint = new IPEndPoint(ip, port);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ip_endPoint);
            serverSocket.Listen(10);
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());

            //在新线程中监听客户端的连接
            Thread thread = new Thread(ClientConnectListen);
            thread.Start();
            Console.ReadLine();

        }

        private static void ClientConnectListen()
        {
            while (true)
            {
                //为新的客户端连接创建一个Socket对象
                Socket clientSocket = serverSocket.Accept();
                Console.WriteLine("客户端{0}成功连接", clientSocket.RemoteEndPoint.ToString());
                //向连接的客户端发送连接成功的数据
                ByteBuffer buffer = new ByteBuffer();
                buffer.WriteString("Connected Server");
                clientSocket.Send(WriteMessage(buffer.ToBytes()));
                //每个客户端连接创建一个线程来接受该客户端发送的消息
                Thread thread = new Thread(RecieveMessage);
                thread.Start(clientSocket);
            }
        }

         private static byte[] WriteMessage(byte[] message)
        {
            MemoryStream ms = null;
            using (ms = new MemoryStream())
            {
                ms.Position = 0;
                BinaryWriter writer = new BinaryWriter(ms);
                ushort msglen = (ushort)message.Length;
                writer.Write(msglen);
                writer.Write(message);
                writer.Flush();
                return ms.ToArray();
            }
        }

        private static void RecieveMessage(object clientSocket)
        {
            Socket mClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    int receiveNumber = mClientSocket.Receive(result);
                   /*
                    Console.WriteLine("接收客户端{0}消息， 长度为{1}", mClientSocket.RemoteEndPoint.ToString(), receiveNumber);
                    ByteBuffer buff = new ByteBuffer(result);
                    //数据长度
                    int len = buff.ReadShort();
                    //数据内容
                    string data = buff.ReadString();
                    Console.WriteLine("数据内容：{0}", data);
                    */
                    ByteBuffer buff = new ByteBuffer(result);
                    int datalength = buff.ReadShort();
                    int typeId = buff.ReadInt();
                    byte[] pbdata = buff.ReadBytes();
                    //通过协议号判断选择的解析类
                    if (typeId == (int)EnmCmdID.CS_LOGIN_REQ)
                    {
                        CSLoginReq clientReq = PackCodec.Deserialize<CSLoginReq>(pbdata);
                        string user_name = clientReq.LoginInfo.UserName;
                        string pass_word = clientReq.LoginInfo.Password;
                        Console.WriteLine("数据内容：UserName={0},Password={1}", user_name, pass_word);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    mClientSocket.Shutdown(SocketShutdown.Both);
                    mClientSocket.Close();
                    break;
                }

            }
        }
    }
}
