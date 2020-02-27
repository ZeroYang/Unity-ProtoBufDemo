using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using Test.App;
using Test.Base;
using cs;

public class TestProtoBuf : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// ObjectToFile();
        // FileToObject();

		CSLoginInfo mLoginInfo = new CSLoginInfo();
		mLoginInfo.UserName = "linshuhe";
        mLoginInfo.Password = "123456";
        CSLoginReq mReq = new CSLoginReq();
        mReq.LoginInfo = mLoginInfo;

		byte[] result = PackCodec.Serialize(mReq);
		CSLoginReq req = PackCodec.Deserialize<CSLoginReq>(result);
		Debug.Log("username="+req.LoginInfo.UserName + ";password="+req.LoginInfo.Password);
	}
	
	private static void ObjectToFile()
	{
		//创建对象
		SuperHero sh = new SuperHero();
		sh.hero = new Hero();
		sh.superSkill = "天狼巽闪";

		sh.hero.people = new People();
		sh.hero.skill = "疾鹰七痕剑";

		sh.hero.people.name = "赛特";
		sh.hero.people.sex = Sex.MALE;
		sh.hero.people.age = 30;

		//序列化
		using(MemoryStream stream = new MemoryStream())
		{
			//获取二进制数据
			Serializer.Serialize<SuperHero>(stream, sh);
			byte[] bytes = stream.ToArray();
			stream.Close();

			//写入数据
			using(FileStream fs = File.Open("D:\\test.bytes", FileMode.OpenOrCreate))
			{
				fs.Write(bytes, 0, bytes.Length);
				fs.Flush();
				fs.Close();
			}
		}

		Debug.Log("文件已经生成！");
	}

	private static void FileToObject()
	{
		//读取数据
		using(FileStream fs = File.Open("D:\\test.bytes", FileMode.Open))
		{
			byte[] bytes = new byte[fs.Length];
			fs.Read(bytes, 0, (int)fs.Length);
			fs.Close();

			//反序列化
			using(MemoryStream stream = new MemoryStream(bytes))
			{
				SuperHero sh = Serializer.Deserialize<SuperHero>(stream);
				stream.Close();

				Debug.Log(String.Format("姓名：{0}，性别：{1}，年龄：{2}，绝技：{3}，终极绝技：{4}", sh.hero.people.name, sh.hero.people.sex, sh.hero.people.age, sh.hero.skill, sh.superSkill));
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}