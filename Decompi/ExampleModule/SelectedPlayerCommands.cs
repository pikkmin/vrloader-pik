using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using VRC;
using VRC.Core;

namespace PikMod
{
	// Token: 0x02000010 RID: 16
	public class SelectedPlayerCommands : CommandsBase
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00003750 File Offset: 0x00001950
		public SelectedPlayerCommands()
		{
			try
			{
				this.quickMenuInstanceMethod = typeof(QuickMenu).GetProperties().First((PropertyInfo p) => p.PropertyType == typeof(QuickMenu)).GetGetMethod();
				this.selectedUserField = typeof(QuickMenu).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).First((FieldInfo f) => f.FieldType == typeof(APIUser));
				Console.WriteLine(this.selectedUserField);
			}
			catch
			{
				Console.WriteLine("Error reflecting in SelectedPlayerCommands.");
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003808 File Offset: 0x00001A08
		public override bool Precondition()
		{
			this.menu = (QuickMenu)this.quickMenuInstanceMethod.Invoke(null, null);
			this.selectedUser = (APIUser)this.selectedUserField.GetValue(this.menu);
			if (this.selectedUser != null)
			{
				this.selectedPlayer = PlayerManager.GetPlayer(this.selectedUser.id).vrcPlayer;
				return true;
			}
			return false;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000386F File Offset: 0x00001A6F
		[Command(KeyCode.Mouse1, false)]
		public void CopyAvatar()
		{
			Console.WriteLine(this.selectedUser);
			Console.WriteLine(this.selectedUser.displayName);
			AvatarUtils.CopyAvatar1(this.selectedPlayer);
			Console.WriteLine("Copied {0}'s avatar!", this.selectedUser.displayName);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000038AC File Offset: 0x00001AAC
		[Command(KeyCode.Mouse2, false)]
		public void SaveAvatar()
		{
			AvatarUtils.SaveAvatar(this.selectedPlayer, ConsoleUtils.AskInput("Enter avatar's name: "), "");
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000038C9 File Offset: 0x00001AC9
		[Command(KeyCode.T, false)]
		public void TeleportToPlayer()
		{
			PlayerUtils.TeleportTo(this.selectedPlayer);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000038D8 File Offset: 0x00001AD8
		[Command(KeyCode.M, false)]
		public void SendMessage()
		{
			ApiNotification.SendNotification(this.selectedUser.id, ApiNotification.NotificationType.All, ConsoleUtils.AskInput("Enter message: "), null, delegate(ApiNotification notification)
			{
				Console.WriteLine("Message sent!");
			}, delegate(string error)
			{
				Console.WriteLine("Error sending message");
			});
		}

		// Token: 0x0400003A RID: 58
		private MethodInfo quickMenuInstanceMethod;

		// Token: 0x0400003B RID: 59
		private FieldInfo selectedUserField;

		// Token: 0x0400003C RID: 60
		private QuickMenu menu;

		// Token: 0x0400003D RID: 61
		private APIUser selectedUser;

		// Token: 0x0400003E RID: 62
		private VRCPlayer selectedPlayer;

		// Token: 0x0200001F RID: 31
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600008F RID: 143 RVA: 0x00004036 File Offset: 0x00002236
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000090 RID: 144 RVA: 0x000028E6 File Offset: 0x00000AE6
			public <>c()
			{
			}

			// Token: 0x06000091 RID: 145 RVA: 0x00004042 File Offset: 0x00002242
			internal bool <.ctor>b__0_0(PropertyInfo p)
			{
				return p.PropertyType == typeof(QuickMenu);
			}

			// Token: 0x06000092 RID: 146 RVA: 0x00004056 File Offset: 0x00002256
			internal bool <.ctor>b__0_1(FieldInfo f)
			{
				return f.FieldType == typeof(APIUser);
			}

			// Token: 0x06000093 RID: 147 RVA: 0x0000406A File Offset: 0x0000226A
			internal void <SendMessage>b__5_0(ApiNotification notification)
			{
				Console.WriteLine("Message sent!");
			}

			// Token: 0x06000094 RID: 148 RVA: 0x00004076 File Offset: 0x00002276
			internal void <SendMessage>b__5_1(string error)
			{
				Console.WriteLine("Error sending message");
			}

			// Token: 0x04000064 RID: 100
			public static readonly SelectedPlayerCommands.<>c <>9 = new SelectedPlayerCommands.<>c();

			// Token: 0x04000065 RID: 101
			public static Func<PropertyInfo, bool> <>9__0_0;

			// Token: 0x04000066 RID: 102
			public static Func<FieldInfo, bool> <>9__0_1;

			// Token: 0x04000067 RID: 103
			public static Action<ApiNotification> <>9__5_0;

			// Token: 0x04000068 RID: 104
			public static Action<string> <>9__5_1;
		}
	}
}
