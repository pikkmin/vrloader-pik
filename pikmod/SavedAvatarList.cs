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
		
	}
}
