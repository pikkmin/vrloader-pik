using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;

namespace PikMod
{
	// Token: 0x0200000B RID: 11
	public static class ILParser
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002D24 File Offset: 0x00000F24
		static ILParser()
		{
			FieldInfo[] fields = typeof(OpCodes).GetFields();
			for (int i = 0; i < fields.Length; i++)
			{
				OpCode opCode = (OpCode)fields[i].GetValue(null);
				if (opCode.Size == 1)
				{
					ILParser.OpCodes[(int)opCode.Value] = opCode;
				}
				else
				{
					ILParser.MultiOpCodes[(int)(opCode.Value & 255)] = opCode;
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002DAF File Offset: 0x00000FAF
		public static ILInstruction[] Parse(this MethodInfo method)
		{
			return ILParser.Parse(method.GetMethodBody().GetILAsByteArray(), method.DeclaringType.Assembly.ManifestModule);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002DD1 File Offset: 0x00000FD1
		public static ILInstruction[] Parse(this MethodBase methodBase)
		{
			return ILParser.Parse(methodBase.GetMethodBody().GetILAsByteArray(), methodBase.Module);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002DE9 File Offset: 0x00000FE9
		public static ILInstruction[] Parse(this MethodBody methodBody, Module manifest)
		{
			return ILParser.Parse(methodBody.GetILAsByteArray(), manifest);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002DF8 File Offset: 0x00000FF8
		private static ILInstruction[] Parse(byte[] ilCode, Module manifest)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < ilCode.Length; i++)
			{
				ILInstruction ilinstruction = new ILInstruction((ilCode[i] == 254) ? ILParser.MultiOpCodes[(int)ilCode[i + 1]] : ILParser.OpCodes[(int)ilCode[i]], ilCode, i, manifest);
				arrayList.Add(ilinstruction);
				i += ilinstruction.Length - 1;
			}
			return (ILInstruction[])arrayList.ToArray(typeof(ILInstruction));
		}

		// Token: 0x0400001D RID: 29
		private static readonly OpCode[] OpCodes = new OpCode[256];

		// Token: 0x0400001E RID: 30
		private static readonly OpCode[] MultiOpCodes = new OpCode[31];
	}
}
