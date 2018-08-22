using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using VRC;
using VRC.Core;
using Photon;

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

                //this.selectedPlayer = PlayerManager.GetPlayer(this.selectedUser.id).vrcPlayer;
                this.selectedPlayer = PlayerManager.GetPlayer(this.selectedUser.id);
                return true;
			}
			return false;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000386F File Offset: 0x00001A6F
		[Command(KeyCode.Mouse1, false)]
		public void CopyAvatar()
		{

            //.WriteLine(this.selectedUser);
            //Console.WriteLine(this.selectedUser.displayName);
            // Console.WriteLine(this.selectedUser.currentAvatarBlueprint);
            // Console.WriteLine(this.selectedUser.currentAsvatarBlueprint.Values);
            if (this.selectedPlayer.INHALJIAKAB.ONEMEJCBNLC.ContainsKey("avatarBlueprint") && this.selectedPlayer.INHALJIAKAB.ONEMEJCBNLC["avatarBlueprint"] != null)
            {
                var obj = selectedPlayer.INHALJIAKAB.ONEMEJCBNLC;
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                ApiAvatar apiAvatar = new ApiAvatar();
                  
                //apiAvatar = this.selectedPlayer.INHALJIAKAB.ONEMEJCBNLC["avatarBlueprint"];

                Console.WriteLine(this.selectedPlayer.INHALJIAKAB.ONEMEJCBNLC);
                Console.WriteLine(this.selectedPlayer.INHALJIAKAB.ONEMEJCBNLC["avatarBlueprint"]);
                    Console.WriteLine(this.selectedPlayer.INHALJIAKAB.ONEMEJCBNLC["avatarBlueprint"].ToString());
                var pout = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(jsonString);
                Console.WriteLine(pout);
                //AvatarUtils.SaveAvatar(success.Model as ApiAvatar, this.selectedPlayer.INHALJIAKAB.ONEMEJCBNLC["avatarBlueprint"].ToString(), "");

                //Console.WriteLine("Copied {0}'s avatar!", this.selectedUser.displayName);
            }
        }

        // Token: 0x06000050 RID: 80 RVA: 0x000038AC File Offset: 0x00001AAC
        [Command(KeyCode.Mouse2, false)]
		public void SaveAvatar()
		{
			//AvatarUtils.SaveAvatar(this.selectedPlayer, ConsoleUtils.AskInput("Enter avatar's name: "), "");
		}

       /* [Command(KeyCode.Mouse2, false)]
        public void SaveAvatar()
        {
            playerUtils.Yoink(this.selectedPlayer);
        }*/


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
		private VRC.Player selectedPlayer;

		// Token: 0x0200001F RID: 31
	
	}
}
