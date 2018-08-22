using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using UnityEngine;
using VRC.Core;
using VRC.UI;

namespace PikMod
{
	// Token: 0x0200000F RID: 15
	internal class SavedAvatarList : MonoBehaviour
	{
		// Token: 0x0600004A RID: 74 RVA: 0x000035CC File Offset: 0x000017CC
		public void Awake()
		{
			try
			{
				this.avatarListField = typeof(PageAvatar).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).First((FieldInfo f) => f.FieldType == typeof(UiAvatarList[]));
				this.categoryField = typeof(UiAvatarList).GetField("category");
				this.updateAvatarListMethod = typeof(UiAvatarList).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).Where(delegate(MethodInfo m)
				{
					ParameterInfo[] parameters = m.GetParameters();
					return parameters.Length == 1 && parameters.First<ParameterInfo>().ParameterType == typeof(List<ApiAvatar>);
				}).First((MethodInfo m) => m.Parse().Any((ILInstruction i) => i.OpCode == OpCodes.Ldstr && i.GetArgument<string>() == "AvatarsAvailable, page: "));
			}
			catch
			{
				Console.WriteLine("Error reflecting in SavedAvatarList.");
			}
			UnityEngine.Object.DontDestroyOnLoad(this);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000036B4 File Offset: 0x000018B4
		public void Update()
		{
			PageAvatar pageAvatar = MiscUtils.GetVRCUiManager().GetPage("UserInterface/MenuContent/Screens/Avatar") as PageAvatar;
			if (pageAvatar != null && pageAvatar.GetShown())
			{
				UiAvatarList[] array = (UiAvatarList[])this.avatarListField.GetValue(pageAvatar);
				if (array != null)
				{
					foreach (UiAvatarList obj in array)
					{
						if ((int)this.categoryField.GetValue(obj) == 1)
						{
							object[] parameters = new object[]
							{
								AvatarUtils.SavedAvatars
							};
							this.updateAvatarListMethod.Invoke(obj, parameters);
						}
					}
				}
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003746 File Offset: 0x00001946
		public SavedAvatarList()
		{
		}

		// Token: 0x04000037 RID: 55
		private FieldInfo avatarListField;

		// Token: 0x04000038 RID: 56
		private FieldInfo categoryField;

		// Token: 0x04000039 RID: 57
		private MethodInfo updateAvatarListMethod;

		// Token: 0x0200001E RID: 30
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000089 RID: 137 RVA: 0x00003F90 File Offset: 0x00002190
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600008A RID: 138 RVA: 0x000028E6 File Offset: 0x00000AE6
			public <>c()
			{
			}

			// Token: 0x0600008B RID: 139 RVA: 0x00003F9C File Offset: 0x0000219C
			internal bool <Awake>b__0_0(FieldInfo f)
			{
				return f.FieldType == typeof(UiAvatarList[]);
			}

			// Token: 0x0600008C RID: 140 RVA: 0x00003FB0 File Offset: 0x000021B0
			internal bool <Awake>b__0_1(MethodInfo m)
			{
				ParameterInfo[] parameters = m.GetParameters();
				return parameters.Length == 1 && parameters.First<ParameterInfo>().ParameterType == typeof(List<ApiAvatar>);
			}

			// Token: 0x0600008D RID: 141 RVA: 0x00003FE3 File Offset: 0x000021E3
			internal bool <Awake>b__0_2(MethodInfo m)
			{
				return m.Parse().Any((ILInstruction i) => i.OpCode == OpCodes.Ldstr && i.GetArgument<string>() == "AvatarsAvailable, page: ");
			}

			// Token: 0x0600008E RID: 142 RVA: 0x0000400F File Offset: 0x0000220F
			internal bool <Awake>b__0_3(ILInstruction i)
			{
				return i.OpCode == OpCodes.Ldstr && i.GetArgument<string>() == "AvatarsAvailable, page: ";
			}

			// Token: 0x0400005F RID: 95
			public static readonly SavedAvatarList.<>c <>9 = new SavedAvatarList.<>c();

			// Token: 0x04000060 RID: 96
			public static Func<FieldInfo, bool> <>9__0_0;

			// Token: 0x04000061 RID: 97
			public static Func<MethodInfo, bool> <>9__0_1;

			// Token: 0x04000062 RID: 98
			public static Func<ILInstruction, bool> <>9__0_3;

			// Token: 0x04000063 RID: 99
			public static Func<MethodInfo, bool> <>9__0_2;
		}
	}
}
