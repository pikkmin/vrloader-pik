using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using VRC;
using VRC.Core;
using VRLoader.Attributes;
using VRLoader.Modules;

namespace PikMod
{
	// Token: 0x0200000D RID: 13
	[ModuleInfo("PikMod", "1.0", "Pik")]
	public class PikMod : VRModule
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002F81 File Offset: 0x00001181
		private APIUser GetAPIUser(Player player)
		{
			return (APIUser)this._getApiUser.Invoke(player, null);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002F98 File Offset: 0x00001198
		public void Start()
		{
			Console.WriteLine("[PikMod] Started.");
			if (PikMod.self != null)
			{
				return;
			}
			PikMod.self = new GameObject();
			PikMod.self.AddComponent<SavedAvatarList>();
			CommandHandler commandHandler = PikMod.self.AddComponent<CommandHandler>();
			PikMod.self.AddComponent<PikUIMods>();
			commandHandler.AddBase<SelectedPlayerCommands>();
			commandHandler.AddBase<GlobalCommands>();
			AvatarUtils.LoadAvatars();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002FFC File Offset: 0x000011FC
		public void Update()
		{
			if (RoomManagerBase.currentRoom != null)
			{
				int num = Input.GetKey(KeyCode.LeftShift) ? 8 : 4;
				if (Input.GetKeyDown(KeyCode.Tab))
				{
					if (this.pikgui)
					{
						this.pikgui = false;
					}
					else
					{
						this.pikgui = true;
					}
				}
				if (Input.GetKeyDown(KeyCode.I))
				{
					Console.WriteLine("[PikMod  ] Players in room: {0}", string.Join(", ", (from p in PlayerManager.GetAllPlayers()
					select this.GetAPIUser(p).displayName).ToArray<string>()));
				}
				if (Input.GetKeyDown(KeyCode.F))
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
				if (this.flying)
				{
					if (Input.GetKey(KeyCode.Q))
					{
						PlayerManager.GetCurrentPlayer().transform.position += Vector3.up * Time.deltaTime * (float)num;
					}
					if (Input.GetKey(KeyCode.E))
					{
						PlayerManager.GetCurrentPlayer().transform.position += Vector3.down * Time.deltaTime * (float)num;
					}
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003154 File Offset: 0x00001354
		public PikMod()
		{
			PropertyInfo propertyInfo = typeof(Player).GetProperties().FirstOrDefault((PropertyInfo p) => p.PropertyType == typeof(APIUser));
			this._getApiUser = ((propertyInfo != null) ? propertyInfo.GetGetMethod() : null);
			this.hSliderValue = Physics.gravity.y;
			this.Target = "target";
			base..ctor();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000031C7 File Offset: 0x000013C7
		[CompilerGenerated]
		private string <Update>b__14_0(Player p)
		{
			return this.GetAPIUser(p).displayName;
		}

		// Token: 0x04000022 RID: 34
		private MethodInfo _getApiUser;

		// Token: 0x04000023 RID: 35
		public bool flying;

		// Token: 0x04000024 RID: 36
		private Vector3 originalGravity;

		// Token: 0x04000025 RID: 37
		private static GameObject self;

		// Token: 0x04000026 RID: 38
		public bool pikgui;

		// Token: 0x04000027 RID: 39
		public float hSliderValue;

		// Token: 0x04000028 RID: 40
		public GlobalCommands com;

		// Token: 0x04000029 RID: 41
		public bool gravityt;

		// Token: 0x0400002A RID: 42
		public string Target;

		// Token: 0x0400002B RID: 43
		public GameObject selbutton;

		// Token: 0x0400002C RID: 44
		private CursorLockMode wantedMode;

		// Token: 0x0200001A RID: 26
		[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
		public class CommandAttribute : Attribute
		{
			// Token: 0x06000077 RID: 119 RVA: 0x00003E6C File Offset: 0x0000206C
			public CommandAttribute(KeyCode KeyCode, bool Hold = false)
			{
				this.KeyCode = KeyCode;
				this.Hold = Hold;
			}

			// Token: 0x04000058 RID: 88
			public readonly KeyCode KeyCode;

			// Token: 0x04000059 RID: 89
			public readonly bool Hold;
		}

		// Token: 0x0200001B RID: 27
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000078 RID: 120 RVA: 0x00003E82 File Offset: 0x00002082
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000079 RID: 121 RVA: 0x000028E6 File Offset: 0x00000AE6
			public <>c()
			{
			}

			// Token: 0x0600007A RID: 122 RVA: 0x00003A07 File Offset: 0x00001C07
			internal bool <.ctor>b__15_0(PropertyInfo p)
			{
				return p.PropertyType == typeof(APIUser);
			}

			// Token: 0x0400005A RID: 90
			public static readonly PikMod.<>c <>9 = new PikMod.<>c();

			// Token: 0x0400005B RID: 91
			public static Func<PropertyInfo, bool> <>9__15_0;
		}
	}
}
