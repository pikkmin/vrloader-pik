using System;
using System.Reflection;
using UnityEngine;

namespace PikMod
{
	// Token: 0x02000004 RID: 4
	public class Command
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000026F9 File Offset: 0x000008F9
		public Command(CommandsBase commandsBase, MethodInfo method, KeyCode keyCode, bool hold = false)
		{
			this.Base = commandsBase;
			this.KeyCode = keyCode;
			this.Hold = hold;
			this._method = method;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000271E File Offset: 0x0000091E
		public void Execute()
		{
			this._method.Invoke(this.Base, null);
		}

		// Token: 0x0400000F RID: 15
		public readonly CommandsBase Base;

		// Token: 0x04000010 RID: 16
		public readonly KeyCode KeyCode;

		// Token: 0x04000011 RID: 17
		public readonly bool Hold;

		// Token: 0x04000012 RID: 18
		private readonly MethodInfo _method;
	}
}
