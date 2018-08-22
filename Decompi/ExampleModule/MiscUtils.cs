using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PikMod
{
	// Token: 0x0200000C RID: 12
	internal static class MiscUtils
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002E78 File Offset: 0x00001078
		static MiscUtils()
		{
			try
			{
				PropertyInfo propertyInfo = typeof(VRCFlowManager).GetProperties().FirstOrDefault((PropertyInfo p) => p.PropertyType == typeof(VRCFlowManager));
				MiscUtils.flowManagerMethod = ((propertyInfo != null) ? propertyInfo.GetGetMethod() : null);
				PropertyInfo propertyInfo2 = typeof(VRCUiManager).GetProperties().FirstOrDefault((PropertyInfo p) => p.PropertyType == typeof(VRCUiManager));
				MiscUtils.uiManagerMethod = ((propertyInfo2 != null) ? propertyInfo2.GetGetMethod() : null);
				PropertyInfo propertyInfo3 = typeof(VRCApplicationSetup).GetProperties().FirstOrDefault((PropertyInfo p) => p.PropertyType == typeof(VRCApplicationSetup));
				MiscUtils.appSetupMethod = ((propertyInfo3 != null) ? propertyInfo3.GetGetMethod() : null);
			}
			catch
			{
				Console.WriteLine("Error loading VRChat information fields.");
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002F48 File Offset: 0x00001148
		public static VRCFlowManager GetVRCFlowManagerInstance()
		{
			return (VRCFlowManager)MiscUtils.flowManagerMethod.Invoke(null, null);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F5B File Offset: 0x0000115B
		public static VRCApplicationSetup GetVRCApplicationSetup()
		{
			return (VRCApplicationSetup)MiscUtils.appSetupMethod.Invoke(null, null);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002F6E File Offset: 0x0000116E
		public static VRCUiManager GetVRCUiManager()
		{
			return (VRCUiManager)MiscUtils.uiManagerMethod.Invoke(null, null);
		}

		// Token: 0x0400001F RID: 31
		private static MethodInfo flowManagerMethod;

		// Token: 0x04000020 RID: 32
		private static MethodInfo uiManagerMethod;

		// Token: 0x04000021 RID: 33
		private static MethodInfo appSetupMethod;

		// Token: 0x02000019 RID: 25
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000072 RID: 114 RVA: 0x00003E24 File Offset: 0x00002024
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000073 RID: 115 RVA: 0x000028E6 File Offset: 0x00000AE6
			public <>c()
			{
			}

			// Token: 0x06000074 RID: 116 RVA: 0x00003E30 File Offset: 0x00002030
			internal bool <.cctor>b__0_0(PropertyInfo p)
			{
				return p.PropertyType == typeof(VRCFlowManager);
			}

			// Token: 0x06000075 RID: 117 RVA: 0x00003E44 File Offset: 0x00002044
			internal bool <.cctor>b__0_1(PropertyInfo p)
			{
				return p.PropertyType == typeof(VRCUiManager);
			}

			// Token: 0x06000076 RID: 118 RVA: 0x00003E58 File Offset: 0x00002058
			internal bool <.cctor>b__0_2(PropertyInfo p)
			{
				return p.PropertyType == typeof(VRCApplicationSetup);
			}

			// Token: 0x04000057 RID: 87
			public static readonly MiscUtils.<>c <>9 = new MiscUtils.<>c();
		}
	}
}
