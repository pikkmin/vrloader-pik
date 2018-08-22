using System;
using UnityEngine;

namespace PikMod
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class CommandAttribute : Attribute
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002733 File Offset: 0x00000933
		public CommandAttribute(KeyCode KeyCode, bool Hold = false)
		{
			this.KeyCode = KeyCode;
			this.Hold = Hold;
		}

		// Token: 0x04000013 RID: 19
		public readonly KeyCode KeyCode;

		// Token: 0x04000014 RID: 20
		public readonly bool Hold;
	}
}
