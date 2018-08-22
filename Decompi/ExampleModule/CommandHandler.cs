using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PikMod
{
	// Token: 0x02000006 RID: 6
	public class CommandHandler : MonoBehaviour
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000274C File Offset: 0x0000094C
		public T AddBase<T>() where T : CommandsBase, new()
		{
			CommandsBase commandsBase = Activator.CreateInstance<T>();
			List<Command> list = new List<Command>();
			foreach (MethodInfo methodInfo in commandsBase.GetType().GetMethods())
			{
				CommandAttribute commandAttribute = methodInfo.GetCustomAttributes(false).FirstOrDefault((object a) => a is CommandAttribute) as CommandAttribute;
				if (commandAttribute != null)
				{
					list.Add(new Command(commandsBase, methodInfo, commandAttribute.KeyCode, commandAttribute.Hold));
				}
			}
			commandsBase.Commands = list.ToArray();
			this.bases.Add(commandsBase);
			return commandsBase as T;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000027FF File Offset: 0x000009FF
		public void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(this);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002808 File Offset: 0x00000A08
		public void Update()
		{
			foreach (CommandsBase commandsBase in this.bases)
			{
				if (commandsBase.Precondition())
				{
					foreach (Command command in commandsBase.Commands)
					{
						if ((command.Hold && Input.GetKey(command.KeyCode)) || (!command.Hold && Input.GetKeyDown(command.KeyCode)))
						{
							command.Execute();
						}
					}
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000028AC File Offset: 0x00000AAC
		public CommandHandler()
		{
		}

		// Token: 0x04000015 RID: 21
		private GameObject self = new GameObject();

		// Token: 0x04000016 RID: 22
		private List<CommandsBase> bases = new List<CommandsBase>();

		// Token: 0x02000017 RID: 23
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__0<T> where T : CommandsBase, new()
		{
			// Token: 0x06000068 RID: 104 RVA: 0x00003D20 File Offset: 0x00001F20
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__0()
			{
			}

			// Token: 0x06000069 RID: 105 RVA: 0x000028E6 File Offset: 0x00000AE6
			public <>c__0()
			{
			}

			// Token: 0x0600006A RID: 106 RVA: 0x00003D2C File Offset: 0x00001F2C
			internal bool <AddBase>b__0_0(object a)
			{
				return a is CommandAttribute;
			}

			// Token: 0x0400004F RID: 79
			public static readonly CommandHandler.<>c__0<T> <>9 = new CommandHandler.<>c__0<T>();

			// Token: 0x04000050 RID: 80
			public static Func<object, bool> <>9__0_0;
		}
	}
}
