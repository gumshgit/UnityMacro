//Generate by ResourceDefiner.cs
using GmshLib;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.IMGUI.Controls;
#endif
[System.Serializable]
public class CommandType : ExEnumBase
{
	public const int Null = 0;
	public const int Move = 1;
	public const int Attack = 2;
	public static implicit operator CommandType(int n){return new CommandType { Value = n };}
	public static bool operator ==(CommandType l, int r){return l.Value == r;}
	public static bool operator !=(CommandType l, int r){return !(l == r);}
	public override bool Equals(object o){return true;}
	public override int GetHashCode(){return 0;}
#if UNITY_EDITOR
	static readonly string[] m_paths = new string[]
	{
		"CommandType/Null",
		"CommandType/Move",
		"CommandType/Attack",
	}
	;
	protected override string[] Paths{ get{ return m_paths; }}
#endif
}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(CommandType))]
public class CommandTypeDrawer : ExEnumBaseDrawer
{
	protected override ExEnumDropdownBase CreateDropDown(){return new EnumDropdown(new AdvancedDropdownState());}
	public class EnumDropdown : ExEnumDropdownBase
	{
		ExEnumItem root =
		new ExEnumItem("CommandType", 0)
		.SetChild(
		new ExEnumItem("Null", 0)
		)
		.SetChild(
		new ExEnumItem("Move", 1)
		)
		.SetChild(
		new ExEnumItem("Attack", 2)
		)
		;
		public EnumDropdown(AdvancedDropdownState state) : base(state) { }
		protected override AdvancedDropdownItem BuildRoot() { return root; }
	}
}
#endif
