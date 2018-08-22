using System;

namespace PikMod
{
	// Token: 0x02000007 RID: 7
	public class CommandsBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000028CA File Offset: 0x00000ACA
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000028D2 File Offset: 0x00000AD2
		public Command[] Commands
		{
			get
			{
				return this._commands;
			}
			set
			{
				if (this._commands == null)
				{
					this._commands = value;
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000028E3 File Offset: 0x00000AE3
		public virtual bool Precondition()
		{
			return true;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000028E6 File Offset: 0x00000AE6
		public CommandsBase()
		{
		}

		// Token: 0x04000017 RID: 23
		private Command[] _commands;
	}
}
