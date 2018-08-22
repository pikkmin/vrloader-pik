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
    
	}
}
