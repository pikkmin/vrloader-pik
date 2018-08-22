using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VRC;
using VRC.Core;
using VRLoader.Attributes;
using VRLoader.Modules;
namespace PikMod
{

    [ModuleInfo("PikMod", "1.0", "Pik")]
    public class PikMod : VRModule
    {
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

        private MethodInfo _getApiUser = typeof(Player).GetProperties().FirstOrDefault(p => p.PropertyType == typeof(APIUser))?.GetGetMethod();
        // Token: 0x04000019 RID: 25 
        private APIUser GetAPIUser(Player player) => (APIUser)_getApiUser.Invoke(player, null);
        public bool flying = false;
        private Vector3 originalGravity;
        private static GameObject self;
        public bool pikgui = false;
        public float hSliderValue = Physics.gravity.y;
        public GlobalCommands com;
        public bool gravityt = false;
        public String Target = "target";
        public GameObject selbutton;
      

        public void Start()
        {
            Console.WriteLine("[PikMod] Started.");
            if (self != null)
            {
                return;
            }
            self = new GameObject();
            self.AddComponent<SavedAvatarList>();
            //self.AddComponent<FlyMode>();
            // this.self.AddComponent<BoneEsp>();
            CommandHandler commandHandler = PikMod.self.AddComponent<CommandHandler>();
            PikUIMods pikui = PikMod.self.AddComponent<PikUIMods>();
            commandHandler.AddBase<SelectedPlayerCommands>();
            commandHandler.AddBase<GlobalCommands>();
            Fps fpss = PikMod.self.AddComponent<Fps>();

            AvatarUtils.LoadAvatars();
        }


        public void Update()
        {
            if (RoomManagerBase.currentRoom != null)
            {



                int num = Input.GetKey(KeyCode.LeftShift) ? 8 : 4;

                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    if (pikgui == true)
                    {
               
                        pikgui = false;
                        
                    }
                    else
                    {
                       
                        pikgui = true;
                       
                    } 
                }

                if (Input.GetKeyDown(KeyCode.I))
                    Console.WriteLine("[PikMod  ] Players in room: {0}", string.Join(", ", PlayerManager.GetAllPlayers().Select(p => GetAPIUser(p).displayName).ToArray()));

                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (flying == true)
                    {
                        flying = false;
                        Physics.gravity = this.originalGravity;
                    }
                    else
                    {
                        flying = true;
                        this.originalGravity = Physics.gravity;
                        Physics.gravity = Vector3.zero;

                    }
                    Console.WriteLine("[PikMod  ] Flying: " + flying);
                }
                if (flying == true)
                {
                    if (Input.GetKey(KeyCode.Q))
                    {
                        PlayerManager.GetCurrentPlayer().transform.position += Vector3.up * Time.deltaTime * num;
                    }
                    if (Input.GetKey(KeyCode.E))
                    {
                        PlayerManager.GetCurrentPlayer().transform.position += Vector3.down * Time.deltaTime * num;
                    }
                }


            }

        }


        // Token: 0x0600004E RID: 78 RVA: 0x000038C0 File Offset: 0x00001AC0








    }

}