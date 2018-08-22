using System;
using UnityEngine;

namespace PikMod
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class CommandAttribute : Attribute
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000034E4 File Offset: 0x000016E4
		public CommandAttribute(KeyCode KeyCode, bool Hold = false)
		{
			this.KeyCode = KeyCode;
			this.Hold = Hold;
		}

		// Token: 0x0400002A RID: 42
		public readonly KeyCode KeyCode;

		// Token: 0x0400002B RID: 43
		public readonly bool Hold;
	}
}
