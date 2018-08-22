using System;
using System.Reflection;
using System.Reflection.Emit;

namespace PikMod
{
	// Token: 0x0200000A RID: 10
	public struct ILInstruction
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002AFC File Offset: 0x00000CFC
		public ILInstruction(OpCode opCode, byte[] ilCode, int index, Module manifest)
		{
			this.OpCode = opCode;
			this.HasArgument = (opCode.OperandType != OperandType.InlineNone);
			this.HasSingleByteArgument = OpCodes.TakesSingleByteArgument(opCode);
			this.Length = opCode.Size + (this.HasArgument ? (this.HasSingleByteArgument ? 1 : 4) : 0);
			if (this.HasArgument)
			{
				if (this.HasSingleByteArgument)
				{
					this.Argument = ilCode[index + opCode.Size];
				}
				else
				{
					this.Argument = BitConverter.ToInt32(ilCode, index + opCode.Size);
				}
				if (this.OpCode == OpCodes.Ldstr)
				{
					this.Argument = manifest.ResolveString((int)this.Argument);
					return;
				}
				if (this.OpCode == OpCodes.Call || this.OpCode == OpCodes.Callvirt)
				{
					this.Argument = manifest.ResolveMethod((int)this.Argument);
					return;
				}
				if (this.OpCode == OpCodes.Box)
				{
					this.Argument = manifest.ResolveType((int)this.Argument);
					return;
				}
				if (this.OpCode == OpCodes.Ldfld || this.OpCode == OpCodes.Ldflda)
				{
					this.Argument = manifest.ResolveField((int)this.Argument);
					return;
				}
			}
			else
			{
				this.Argument = null;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002C70 File Offset: 0x00000E70
		public T GetArgument<T>()
		{
			return (T)((object)this.Argument);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002C80 File Offset: 0x00000E80
		public override string ToString()
		{
			string arg = string.Empty;
			if (this.HasArgument)
			{
				if (this.Argument is int || this.Argument is byte)
				{
					arg = string.Format(" 0x{0:X}", this.Argument);
				}
				else if (this.Argument is string)
				{
					arg = " \"" + this.Argument.ToString() + "\"";
				}
				else
				{
					arg = " " + this.Argument.ToString();
				}
			}
			return string.Format("{0}{1}", this.OpCode.Name, arg);
		}

		// Token: 0x04000018 RID: 24
		public readonly OpCode OpCode;

		// Token: 0x04000019 RID: 25
		public readonly object Argument;

		// Token: 0x0400001A RID: 26
		public readonly bool HasArgument;

		// Token: 0x0400001B RID: 27
		public readonly bool HasSingleByteArgument;

		// Token: 0x0400001C RID: 28
		public readonly int Length;
	}
}
