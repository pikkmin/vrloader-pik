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
	
	}
}
