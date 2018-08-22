using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using PikMod;
using UnityEngine;
using VRC;
using VRC.Core;

// Token: 0x02000002 RID: 2
public class PikUIMods : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private APIUser GetAPIUser(Player player)
	{
		return (APIUser)this._getApiUser.Invoke(player, null);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002064 File Offset: 0x00000264
	public void OnGUI()
	{
		int i = 0;
		PlayerManager.GetAllPlayers().ToList<Player>().ForEach(delegate(Player p)
		{
			float x = 1f;
			int i = i;
			i++;
			GUI.Label(new Rect(x, (float)(i * 15), 300f, 20f), string.Format("{0}", this.GetAPIUser(p).displayName));
		});
		if (this.pikgui)
		{
			float uix = (float)Screen.width * 0.1f;
			float num = (float)Screen.height * 0.1f;
			GUI.Box(new Rect(uix, num, (float)Screen.width * 0.8f, (float)Screen.height * 0.8f), "Pik hack");
			if (GUI.Button(new Rect(uix, num, 100f, 30f), "Fly"))
			{
				if (this.flying)
				{
					this.flying = false;
					Physics.gravity = this.originalGravity;
				}
				else
				{
					this.flying = true;
					this.originalGravity = Physics.gravity;
					Physics.gravity = Vector3.zero;
				}
				Console.WriteLine("[PikMod  ] Flying: " + this.flying.ToString());
			}
			if (GUI.Button(new Rect(uix, num + 100f, 200f, 30f), "Toggle Custom Gravity"))
			{
				this.gravityt = !this.gravityt;
				if (!this.gravityt)
				{
					this.originalGravity = Physics.gravity;
					Physics.gravity = new Vector3(0f, this.hSliderValue, 0f);
				}
				else
				{
					Physics.gravity = this.originalGravity;
				}
			}
			this.hSliderValue = GUI.HorizontalSlider(new Rect(uix, num + 50f, 300f, 100f), this.hSliderValue, -10f, 10f);
			GUI.Label(new Rect(uix, num + 150f, 200f, 50f), "Current Gravity: " + Physics.gravity.y.ToString());
			if (GUI.Button(new Rect(uix, num + 250f, 100f, 30f), "Teleport"))
			{
				Player player = PlayerUtils.FindPlayer(this.Target);
				PlayerUtils.TeleportTo(player);
				Console.WriteLine("Teleporting to: {0}", player.GetAPIUser().displayName);
			}
			int l = 0;
			PlayerManager.GetAllPlayers().ToList<Player>().ForEach(delegate(Player p)
			{
				float x = uix + 200f;
				int l = l;
				l++;
				GUI.Button(new Rect(x, (float)(l * 15), 300f, 20f), string.Format("{0}", this.GetAPIUser(p).displayName));
			});
			PlayerManager.GetAllPlayers().ToArray<Player>();
			if (GUI.Button(new Rect(uix, num + 300f, 200f, 30f), "Jump"))
			{
				if (VRCPlayer.Instance.gameObject.GetComponent<PlayerModComponentJump>())
				{
					Console.WriteLine("World already has jump enabled.");
					return;
				}
				VRCPlayer.Instance.gameObject.AddComponent<PlayerModComponentJump>();
				Console.WriteLine("Enabled jumping in current world.");
			}
		}
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002340 File Offset: 0x00000540
	private void Start()
	{
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002342 File Offset: 0x00000542
	private void Update()
	{
		if (RoomManagerBase.currentRoom != null && Input.GetKeyDown(KeyCode.Tab))
		{
			if (this.pikgui)
			{
				this.pikgui = false;
				return;
			}
			this.pikgui = true;
		}
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000236C File Offset: 0x0000056C
	public PikUIMods()
	{
		PropertyInfo propertyInfo = typeof(Player).GetProperties().FirstOrDefault((PropertyInfo p) => p.PropertyType == typeof(APIUser));
		this._getApiUser = ((propertyInfo != null) ? propertyInfo.GetGetMethod() : null);
		this.self = new GameObject();
		this.hSliderValue = Physics.gravity.y;
		this.Target = "target";
		base..ctor();
	}

	// Token: 0x04000001 RID: 1
	private MethodInfo _getApiUser;

	// Token: 0x04000002 RID: 2
	public bool flying;

	// Token: 0x04000003 RID: 3
	private Vector3 originalGravity;

	// Token: 0x04000004 RID: 4
	private GameObject self;

	// Token: 0x04000005 RID: 5
	public bool pikgui;

	// Token: 0x04000006 RID: 6
	public float hSliderValue;

	// Token: 0x04000007 RID: 7
	public GlobalCommands com;

	// Token: 0x04000008 RID: 8
	public bool gravityt;

	// Token: 0x04000009 RID: 9
	public string Target;

	// Token: 0x0400000A RID: 10
	public GameObject selbutton;

	// Token: 0x0400000B RID: 11
	public bool jump;

	// Token: 0x02000011 RID: 17
	[CompilerGenerated]
	private sealed class <>c__DisplayClass12_0
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000028E6 File Offset: 0x00000AE6
		public <>c__DisplayClass12_0()
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003940 File Offset: 0x00001B40
		internal void <OnGUI>b__0(Player p)
		{
			float x = 1f;
			int num = this.i;
			this.i = num + 1;
			GUI.Label(new Rect(x, (float)(num * 15), 300f, 20f), string.Format("{0}", this.<>4__this.GetAPIUser(p).displayName));
		}

		// Token: 0x0400003F RID: 63
		public int i;

		// Token: 0x04000040 RID: 64
		public PikUIMods <>4__this;
	}

	// Token: 0x02000012 RID: 18
	[CompilerGenerated]
	private sealed class <>c__DisplayClass12_1
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000028E6 File Offset: 0x00000AE6
		public <>c__DisplayClass12_1()
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003998 File Offset: 0x00001B98
		internal void <OnGUI>b__1(Player p)
		{
			float x = this.uix + 200f;
			int num = this.l;
			this.l = num + 1;
			GUI.Button(new Rect(x, (float)(num * 15), 300f, 20f), string.Format("{0}", this.CS$<>8__locals1.<>4__this.GetAPIUser(p).displayName));
		}

		// Token: 0x04000041 RID: 65
		public float uix;

		// Token: 0x04000042 RID: 66
		public int l;

		// Token: 0x04000043 RID: 67
		public PikUIMods.<>c__DisplayClass12_0 CS$<>8__locals1;
	}

	// Token: 0x02000013 RID: 19
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06000057 RID: 87 RVA: 0x000039FB File Offset: 0x00001BFB
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000028E6 File Offset: 0x00000AE6
		public <>c()
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003A07 File Offset: 0x00001C07
		internal bool <.ctor>b__15_0(PropertyInfo p)
		{
			return p.PropertyType == typeof(APIUser);
		}

		// Token: 0x04000044 RID: 68
		public static readonly PikUIMods.<>c <>9 = new PikUIMods.<>c();

		// Token: 0x04000045 RID: 69
		public static Func<PropertyInfo, bool> <>9__15_0;
	}
}
