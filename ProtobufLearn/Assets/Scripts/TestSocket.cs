using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Net;
using ProtoBuf;
using cs;

public class TestSocket : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// ClientSocket mSocket = new ClientSocket();
        // mSocket.ConnectServer("127.0.0.1", 8088);
        // mSocket.SendMessage("服务器傻逼！");

		CSLoginInfo mLoginInfo = new CSLoginInfo();
        mLoginInfo.UserName = "linshuhe";
        mLoginInfo.Password = "123456";
        CSLoginReq mReq = new CSLoginReq();
        mReq.LoginInfo = mLoginInfo;
 
        byte[] data = CreateData((int)EnmCmdID.CS_LOGIN_REQ, mReq);
        ClientSocket mSocket = new ClientSocket();
        mSocket.ConnectServer("127.0.0.1", 8088);
        mSocket.SendMessage(data);
	}
	
	// Update is called once per frame
	void Update () {
		
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


	private byte[] CreateData(int typeId,IExtensible pbuf)
    {
        byte[] pbdata = PackCodec.Serialize(pbuf);
        ByteBuffer buff = new ByteBuffer();
        buff.WriteInt(typeId);
        buff.WriteBytes(pbdata);
        return buff.ToBytes();
    }

}
