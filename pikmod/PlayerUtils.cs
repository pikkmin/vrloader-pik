﻿using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using VRC;
using VRC.Core;
using VRCSDK2;
namespace PikMod
{
	// Token: 0x0200000E RID: 14
	internal static class PlayerUtils
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000031D8 File Offset: 0x000013D8
		static PlayerUtils()
		{
			try
			{
				PropertyInfo propertyInfo = typeof(VRCPlayer).GetProperties().First((PropertyInfo p) => p.GetGetMethod().Name == "get_player");
				PlayerUtils.getPlayerMethod = ((propertyInfo != null) ? propertyInfo.GetGetMethod() : null);
				PlayerUtils.photonPlayerType = typeof(Player).GetProperties().First((PropertyInfo p) => p.GetGetMethod().Name == "get_PhotonPlayer").PropertyType;
				PropertyInfo propertyInfo2 = PlayerUtils.photonPlayerType.GetProperties().First((PropertyInfo p) => p.GetCustomAttributes(typeof(ObsoleteAttribute), false).Any((object a) => ((ObsoleteAttribute)a).Message.Contains("CustomProperties")));
				PlayerUtils.customPropertiesMethod = ((propertyInfo2 != null) ? propertyInfo2.GetGetMethod() : null);
				PropertyInfo propertyInfo3 = typeof(Player).GetProperties().First((PropertyInfo p) => p.GetGetMethod().Name == "get_isModerator");
				PlayerUtils.isModeratorMethod = ((propertyInfo3 != null) ? propertyInfo3.GetGetMethod() : null);
				PlayerUtils.cSharp = typeof(VRCApplicationSetup).Assembly;
				PlayerUtils.UserType = PlayerUtils.cSharp.GetTypes().First((Type t) => t.BaseType == typeof(APIUser));
				PropertyInfo propertyInfo4 = PlayerUtils.UserType.GetProperties().First((PropertyInfo p) => p.PropertyType == PlayerUtils.UserType);
				PlayerUtils.currentUserMethod = ((propertyInfo4 != null) ? propertyInfo4.GetGetMethod() : null);
				PropertyInfo propertyInfo5 = typeof(Player).GetProperties().First((PropertyInfo p) => p.PropertyType == typeof(APIUser));
				PlayerUtils.getApiUserMethod = ((propertyInfo5 != null) ? propertyInfo5.GetGetMethod() : null);
				PropertyInfo propertyInfo6 = typeof(Player).GetProperties().First((PropertyInfo p) => p.PropertyType == PlayerUtils.photonPlayerType);
				PlayerUtils.getPhotonPlayerMethod = ((propertyInfo6 != null) ? propertyInfo6.GetGetMethod() : null);
				PlayerUtils.getAvatarManagerMethod = typeof(VRCPlayer).GetProperties().First((PropertyInfo p) => p.PropertyType == typeof(VRCAvatarManager)).GetGetMethod();
			}
			catch
			{
				Console.WriteLine("Error loading player types, fields, and methods.");
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000033E4 File Offset: 0x000015E4
		private static Hashtable GetCustomProperties(this object photonPlayer)
		{
			return (Hashtable)PlayerUtils.customPropertiesMethod.Invoke(photonPlayer, null);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000033F7 File Offset: 0x000015F7
		private static object GetPhotonPlayer(this Player player)
		{
			return PlayerUtils.getPhotonPlayerMethod.Invoke(player, null);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003405 File Offset: 0x00001605
		public static object GetCurrentUser()
		{
			return PlayerUtils.currentUserMethod.Invoke(null, null);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003413 File Offset: 0x00001613
		public static VRCAvatarManager GetVRCAvatarManager(this VRCPlayer player)
		{
			return PlayerUtils.getAvatarManagerMethod.Invoke(player, null) as VRCAvatarManager;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003426 File Offset: 0x00001626
		public static ApiAvatar GetApiAvatar(this VRCPlayer player)
		{

            return ((Player)PlayerUtils.getPlayerMethod.Invoke(player, null)).GetApiAvatar();
		}


       /*ublic static void Yoink(VRCPlayer targetPlayer)
        {
            targetPlayer.GetApiAvatar().AssignToThisUser().GetCustomProperties()["avatarBlueprint"];
        }*/

        // Token: 0x06000040 RID: 64 RVA: 0x0000343E File Offset: 0x0000163E
        public static ApiAvatar GetApiAvatar(this Player player)
		{
    
			return (ApiAvatar)player.GetPhotonPlayer().GetCustomProperties()["avatarBlueprint"];
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000345A File Offset: 0x0000165A
		public static APIUser GetAPIUser(this Player player)
		{
			return (APIUser)PlayerUtils.getApiUserMethod.Invoke(player, null);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000346D File Offset: 0x0000166D
		public static bool IsModerator(this Player player)
		{
			return (bool)PlayerUtils.isModeratorMethod.Invoke(player, null);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003480 File Offset: 0x00001680
		public static void TeleportTo(Player targetPlayer)
		{
			PlayerUtils.TeleportTo(targetPlayer.vrcPlayer);
		}

        public static void Rotate(Player targetPlayer, float rx, float ry, float rz)
        {
            //PlayerUtils.Rotate(targetPlayer.vrcPlayer);

            targetPlayer.transform.eulerAngles = new Vector3(rx, ry, rz);
            
            PlayerUtils.TeleportTransform(targetPlayer.transform, targetPlayer.transform);
        }

        public static void portal(Player targetPlayer, float rx, float ry, float rz)
        {
            //PlayerUtils.Rotate(targetPlayer.vrcPlayer);

        }

        // Token: 0x06000044 RID: 68 RVA: 0x0000348D File Offset: 0x0000168D
        public static void TeleportTo(VRCPlayer targetPlayer)
		{
			PlayerUtils.Teleport(PlayerManager.GetCurrentPlayer().vrcPlayer, targetPlayer);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000349F File Offset: 0x0000169F
		public static void Teleport(Player player, Player targetPlayer)
		{
			PlayerUtils.Teleport(player.vrcPlayer, player.vrcPlayer);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000034B2 File Offset: 0x000016B2
		public static void Teleport(VRCPlayer player, VRCPlayer targetPlayer)
		{
			PlayerUtils.TeleportTransform(player.transform, targetPlayer.transform);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000034C8 File Offset: 0x000016C8
		public static Player FindPlayer(string search)
		{
			search = search.ToLower();
			return PlayerManager.GetAllPlayers().FirstOrDefault((Player p) => p.GetAPIUser().displayName.ToLower().Contains(search));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000350C File Offset: 0x0000170C
		public static void Follow(APIUser user)
		{
			if (!(user.location != "offline"))
			{
				Console.WriteLine("User is offline!");
				return;
			}
			if (!(user.location != "private"))
			{
				Console.WriteLine("User is in a private room!");
				return;
			}
			if (user.location.Split(new char[]
			{
				':'
			}).Length > 1)
			{
				string text = user.location.Split(new char[]
				{
					':'
				})[0];
				string text2 = user.location.Split(new char[]
				{
					':'
				})[1];
				return;
			}
			Console.WriteLine("Could not parse user location \"{0}\"", user.location);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000035B1 File Offset: 0x000017B1
		private static void TeleportTransform(Transform from, Transform to)
		{
			from.position = to.position;
			from.rotation = to.rotation;
		}

		// Token: 0x0400002D RID: 45
		public static Type UserType;

		// Token: 0x0400002E RID: 46
		private static Assembly cSharp;

		// Token: 0x0400002F RID: 47
		private static Type photonPlayerType;

		// Token: 0x04000030 RID: 48
		private static MethodInfo getPlayerMethod;

		// Token: 0x04000031 RID: 49
		private static MethodInfo customPropertiesMethod;

		// Token: 0x04000032 RID: 50
		private static MethodInfo isModeratorMethod;

		// Token: 0x04000033 RID: 51
		private static MethodInfo currentUserMethod;

		// Token: 0x04000034 RID: 52
		private static MethodInfo getApiUserMethod;

		// Token: 0x04000035 RID: 53
		private static MethodInfo getPhotonPlayerMethod;

		// Token: 0x04000036 RID: 54
		private static MethodInfo getAvatarManagerMethod;

		// Token: 0x0200001C RID: 28
	
	}
}
