using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using VRC;
using VRC.Core;

namespace PikMod
{
	// Token: 0x02000009 RID: 9
	public class GlobalCommands : CommandsBase
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002985 File Offset: 0x00000B85
		public override bool Precondition()
		{
			return Event.current.control;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002994 File Offset: 0x00000B94
		[Command(KeyCode.G, false)]
		public void TeleportTo()
		{
			Console.Clear();
			Console.WriteLine(string.Join(", ", (from p in PlayerManager.GetAllPlayers()
			select p.GetAPIUser().displayName).ToArray<string>()));
			Player player = PlayerUtils.FindPlayer(ConsoleUtils.AskInput("Teleport to: "));
			PlayerUtils.TeleportTo(player);
			Console.WriteLine("Teleporting to: {0}", player.GetAPIUser().displayName);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A0E File Offset: 0x00000C0E
		[Command(KeyCode.M, false)]
		public void TeleportObjects()
		{
			new Thread(delegate()
			{
				foreach (ObjectInternal objectInternal in UnityEngine.Object.FindObjectsOfType<ObjectInternal>())
				{
					objectInternal.RequestOwnership();
					Thread.Sleep(200);
					if (typeof(ObjectInternal).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault((FieldInfo f) => f.FieldType == typeof(Rigidbody)).GetValue(objectInternal) != null)
					{
						objectInternal.transform.position = PlayerManager.GetCurrentPlayer().transform.position;
					}
				}
			}).Start();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002A39 File Offset: 0x00000C39
		[Command(KeyCode.K, false)]
		public void SaveCurrentAvatar()
		{
			AvatarUtils.SaveAvatar(PlayerManager.GetCurrentPlayer().GetApiAvatar(), ConsoleUtils.AskInput("Enter avatar's name: ") ?? "Nameless", "");
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002A63 File Offset: 0x00000C63
		[Command(KeyCode.Delete, false)]
		public void DeleteCurrentAvatar()
		{
			if (AvatarUtils.DeleteAvatar(PlayerManager.GetCurrentPlayer().GetApiAvatar()))
			{
				Console.WriteLine("Current avatar deleted.");
				return;
			}
			Console.WriteLine("No avatars found to delete.");
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A8C File Offset: 0x00000C8C
		[Command(KeyCode.Mouse2, false)]
		public void SaveAvatarId()
		{
			ApiAvatar apiAvatar = new ApiAvatar();
			apiAvatar.id = ConsoleUtils.AskInput("Enter avatar ID: ");
			apiAvatar.Fetch(delegate(ApiContainer success)
			{
				AvatarUtils.SaveAvatar(success.Model as ApiAvatar, ConsoleUtils.AskInput("Enter avatar's name: "), "");
			}, delegate(ApiContainer error)
			{
				Console.WriteLine("Error saving avatar: {0}", error.Error);
			}, null, false);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002AF3 File Offset: 0x00000CF3
		public GlobalCommands()
		{
		}

		// Token: 0x02000018 RID: 24
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600006B RID: 107 RVA: 0x00003D37 File Offset: 0x00001F37
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600006C RID: 108 RVA: 0x000028E6 File Offset: 0x00000AE6
			public <>c()
			{
			}

			// Token: 0x0600006D RID: 109 RVA: 0x00003D43 File Offset: 0x00001F43
			internal string <TeleportTo>b__1_0(Player p)
			{
				return p.GetAPIUser().displayName;
			}

			// Token: 0x0600006E RID: 110 RVA: 0x00003D50 File Offset: 0x00001F50
			internal void <TeleportObjects>b__2_0()
			{
				foreach (ObjectInternal objectInternal in UnityEngine.Object.FindObjectsOfType<ObjectInternal>())
				{
					objectInternal.RequestOwnership();
					Thread.Sleep(200);
					if (typeof(ObjectInternal).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault((FieldInfo f) => f.FieldType == typeof(Rigidbody)).GetValue(objectInternal) != null)
					{
						objectInternal.transform.position = PlayerManager.GetCurrentPlayer().transform.position;
					}
				}
			}

			// Token: 0x0600006F RID: 111 RVA: 0x00003DDC File Offset: 0x00001FDC
			internal bool <TeleportObjects>b__2_1(FieldInfo f)
			{
				return f.FieldType == typeof(Rigidbody);
			}

			// Token: 0x06000070 RID: 112 RVA: 0x00003DF0 File Offset: 0x00001FF0
			internal void <SaveAvatarId>b__5_0(ApiContainer success)
			{
				AvatarUtils.SaveAvatar(success.Model as ApiAvatar, ConsoleUtils.AskInput("Enter avatar's name: "), "");
			}

			// Token: 0x06000071 RID: 113 RVA: 0x00003E12 File Offset: 0x00002012
			internal void <SaveAvatarId>b__5_1(ApiContainer error)
			{
				Console.WriteLine("Error saving avatar: {0}", error.Error);
			}

			// Token: 0x04000051 RID: 81
			public static readonly GlobalCommands.<>c <>9 = new GlobalCommands.<>c();

			// Token: 0x04000052 RID: 82
			public static Func<Player, string> <>9__1_0;

			// Token: 0x04000053 RID: 83
			public static Func<FieldInfo, bool> <>9__2_1;

			// Token: 0x04000054 RID: 84
			public static ThreadStart <>9__2_0;

			// Token: 0x04000055 RID: 85
			public static Action<ApiContainer> <>9__5_0;

			// Token: 0x04000056 RID: 86
			public static Action<ApiContainer> <>9__5_1;
		}
	}
}
