//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: test1.proto
namespace Test.Base
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"People")]
  public partial class People : global::ProtoBuf.IExtensible
  {
    public People() {}
    
    private string _name;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private Test.Base.Sex _sex;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"sex", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public Test.Base.Sex sex
    {
      get { return _sex; }
      set { _sex = value; }
    }
    private int _age = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"age", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int age
    {
      get { return _age; }
      set { _age = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Hero")]
  public partial class Hero : global::ProtoBuf.IExtensible
  {
    public Hero() {}
    
    private Test.Base.People _people;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"people", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Test.Base.People people
    {
      get { return _people; }
      set { _people = value; }
    }
    private string _skill = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"skill", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string skill
    {
      get { return _skill; }
      set { _skill = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"Sex")]
    public enum Sex
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"MALE", Value=0)]
      MALE = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FEMALE", Value=1)]
      FEMALE = 1
    }
  
}