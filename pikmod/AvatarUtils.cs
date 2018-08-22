using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;
using VRC.Core;

namespace PikMod
{
	// Token: 0x02000003 RID: 3
	internal static class AvatarUtils
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000023EC File Offset: 0x000005EC
		static AvatarUtils()
		{
			AvatarUtils.setCurrentAvatarMethod = PlayerUtils.UserType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).Where(delegate(MethodInfo m)
			{
				ParameterInfo[] parameters = m.GetParameters();
				return parameters.Length == 1 && parameters[0].ParameterType == typeof(ApiAvatar);
			}).FirstOrDefault(delegate(MethodInfo m)
			{
				ILInstruction ilinstruction = m.Parse().LastOrDefault((ILInstruction i) => i.OpCode == OpCodes.Call);
				if (ilinstruction.Equals(default(ILInstruction)))
				{
					return false;
				}
				bool result;
				try
				{
					result = ilinstruction.GetArgument<MethodBase>().Parse().Any((ILInstruction i) => i.OpCode == OpCodes.Ldstr && i.GetArgument<string>() == "modTag");
				}
				catch
				{
					result = false;
				}
				return result;
			});
			if (AvatarUtils.setCurrentAvatarMethod == null)
			{
				Console.WriteLine("Error parsing SetCurrentAvatar IL.");
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000244F File Offset: 0x0000064F
		public static string GenerateAvatarId()
		{
			return "avtr_" + Guid.NewGuid();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002465 File Offset: 0x00000665
		public static void LoadAvatars()
		{
			new Thread(delegate()
			{
				while (PlayerUtils.GetCurrentUser() == null)
				{
					Thread.Sleep(1000);
				}
				try
				{
					if (AvatarUtils.SavedAvatars.Count == 0 && File.Exists("VRChat_Data\\Managed\\VRLoader\\Modules\\Avatars.txt"))
					{
						string[] array = File.ReadAllLines("VRChat_Data\\Managed\\VRLoader\\Modules\\Avatars.txt");
						for (int i = 0; i < array.Length; i++)
						{
							string[] array2 = array[i].Split(new char[]
							{
								'|'
							});
							if (array2.Length >= 3)
							{
								ApiAvatar apiAvatar = new ApiAvatar();
								apiAvatar.Init(array2[1], APIUser.CurrentUser, array2[0], "", array2[2], array2[0], "public", null, null);
								AvatarUtils.SavedAvatars.Add(apiAvatar);
							}
						}
					}
					if (AvatarUtils.SavedAvatars.Count > 0)
					{
						AvatarUtils.SavedAvatars = (from a in AvatarUtils.SavedAvatars
						group a by a.assetUrl into a
						select a.First<ApiAvatar>()).ToList<ApiAvatar>();
						Console.WriteLine("Loaded {0} {1}!", AvatarUtils.SavedAvatars.Count, (AvatarUtils.SavedAvatars.Count > 1) ? "avatars" : "avatar");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error loading avatars: {0}", ex.Message);
				}
			}).Start();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002490 File Offset: 0x00000690
		public static void SetCurrentAvatar(ApiAvatar avatar)
		{
			AvatarUtils.setCurrentAvatarMethod.Invoke(PlayerUtils.GetCurrentUser(), new object[]
			{
				avatar
			});
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000024AC File Offset: 0x000006AC
		public static void CopyAvatar1(VRCPlayer vrcPlayer)
		{
			Console.WriteLine(vrcPlayer.GetApiAvatar() + "gay");
			Console.WriteLine(vrcPlayer.GetApiAvatar().assetUrl);
            Console.WriteLine(vrcPlayer.GetApiAvatar().id);
            Console.WriteLine(vrcPlayer.GetApiAvatar().ToString());
            ApiAvatar avatar = vrcPlayer.GetApiAvatar();

            Console.WriteLine("copy part 2");
            avatar.authorId = APIUser.CurrentUser.id;
            avatar.authorName = APIUser.CurrentUser.displayName;
            avatar.releaseStatus = "public";
            avatar.id = AvatarUtils.GenerateAvatarId();
            AvatarUtils.SetCurrentAvatar(avatar);
            //AvatarUtils.CopyAvatar2(vrcPlayer.GetApiAvatar());
        }

		// Token: 0x0600000B RID: 11 RVA: 0x000024E0 File Offset: 0x000006E0
		public static void CopyAvatar2(ApiAvatar avatar)
		{
			Console.WriteLine("copy part 2");
			avatar.authorId = APIUser.CurrentUser.id;
			avatar.authorName = APIUser.CurrentUser.displayName;
			avatar.releaseStatus = "public";
			avatar.id = AvatarUtils.GenerateAvatarId();
			AvatarUtils.SetCurrentAvatar(avatar);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002534 File Offset: 0x00000734
		public static string GetInfo(this ApiAvatar avatar)
		{
			return string.Join("\n", (from p in typeof(ApiAvatar).GetProperties()
			select string.Format("{0}: {1}", p.Name, p.GetValue(avatar, null))).ToArray<string>());
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002580 File Offset: 0x00000780
		public static void Init(this ApiAvatar avatar, string id, APIUser user, string name, string imageUrl, string assetUrl, string description, string releaseStatus, List<string> tags, string packageUrl = null)
		{
			avatar.id = id;
			avatar.authorName = user.displayName;
			avatar.authorId = user.id;
			avatar.name = name;
			avatar.assetUrl = assetUrl;
			avatar.imageUrl = imageUrl;
			avatar.description = description;
			avatar.releaseStatus = releaseStatus;
			avatar.tags = tags;
			avatar.unityPackageUrl = packageUrl;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000025E4 File Offset: 0x000007E4
		public static bool DeleteAvatar(ApiAvatar avatar)
		{
			bool flag = AvatarUtils.SavedAvatars.RemoveAll((ApiAvatar a) => a.assetUrl == avatar.assetUrl) > 0;
			if (flag)
			{
				File.WriteAllLines("VRChat_Data\\Managed\\VRLoader\\Modules\\Avatars.txt", (from a in AvatarUtils.SavedAvatars
				select string.Format("{0}|{1}|{2}{3}", new object[]
				{
					a.name,
					a.id,
					a.assetUrl,
					Environment.NewLine
				})).ToArray<string>());
			}
			return flag;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002652 File Offset: 0x00000852
		public static ApiAvatar SaveAvatar(VRCPlayer vrcPlayer, string name, string imageUrl = "")
		{
			return AvatarUtils.SaveAvatar(vrcPlayer.GetApiAvatar(), name, imageUrl);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002664 File Offset: 0x00000864
		public static ApiAvatar SaveAvatar(ApiAvatar avatar, string name, string imageUrl = "")
		{
			ApiAvatar apiAvatar = new ApiAvatar();
			apiAvatar.Init(AvatarUtils.GenerateAvatarId(), APIUser.CurrentUser, name, avatar.imageUrl, avatar.assetUrl, avatar.description, "public", avatar.tags, avatar.unityPackageUrl);
			File.AppendAllText("VRChat_Data\\Managed\\VRLoader\\Modules\\Avatars.txt", string.Format("{0}|{1}|{2}{3}", new object[]
			{
				name,
				avatar.id,
				avatar.assetUrl,
				Environment.NewLine
			}));
			AvatarUtils.SavedAvatars.Add(apiAvatar);
			Console.WriteLine("Avatar saved!");
			return avatar;
		}

		// Token: 0x0400000C RID: 12
		private const string AVATARS_PATH = "VRChat_Data\\Managed\\VRLoader\\Modules\\Avatars.txt";

		// Token: 0x0400000D RID: 13
		private static MethodInfo setCurrentAvatarMethod;

		// Token: 0x0400000E RID: 14
		public static List<ApiAvatar> SavedAvatars = new List<ApiAvatar>();

		// Token: 0x02000014 RID: 20
		
	}
}
