using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using UnityEngine;
using VRC;
using VRC.Core;
using VRCSDK2;

namespace PikMod
{
    // Token: 0x02000010 RID: 16
    public class GlobalCommands : CommandsBase
    {

        
        // Token: 0x06000046 RID: 70 RVA: 0x0000369E File Offset: 0x0000189E
        public override bool Precondition()
        {
            return Event.current.control;
        }

        // Token: 0x06000047 RID: 71 RVA: 0x000036AC File Offset: 0x000018AC
        [Command(KeyCode.G, false)]
        public void TeleportTo()
        {
            Console.Clear();
            Console.WriteLine(string.Join(", ", (from p in PlayerManager.GetAllPlayers()
                                                 select p.GetAPIUser().displayName).ToArray<string>()));
            VRC.Player player = PlayerUtils.FindPlayer(ConsoleUtils.AskInput("Teleport to: "));
            PlayerUtils.TeleportTo(player);
            Console.WriteLine("Teleporting to: {0}", player.GetAPIUser().displayName);
        }
        float rotation = 5f;
        [Command(KeyCode.E, false)]
        public void Rotate()
        {
            rotation += 5f;
            Console.Clear();
            //Console.WriteLine(string.Join(", ", (from p in PlayerManager.GetAllPlayers()
            //select p.GetAPIUser().displayName).ToArray<string>()));
            VRC.Player player = PlayerManager.GetCurrentPlayer();
          //  PlayerUtils.Rotate(player,rotation);
            Console.WriteLine("rotate to: {0}", player.GetAPIUser().displayName);
        }
   
        [Command(KeyCode.Q, false)]
        public void Rotateback()
        {
            rotation -= 5f;
            Console.Clear();
            //Console.WriteLine(string.Join(", ", (from p in PlayerManager.GetAllPlayers()
            //select p.GetAPIUser().displayName).ToArray<string>()));
            VRC.Player player = PlayerManager.GetCurrentPlayer();
           // PlayerUtils.Rotate(player, rx,ry,rz);
            Console.WriteLine("rotate to: {0}", player.GetAPIUser().displayName);
        }
        // Token: 0x06000048 RID: 72 RVA: 0x00003726 File Offset: 0x00001926

        [Command(KeyCode.M, false)]
        public void TeleportObjects()
        {
            new Thread(delegate ()
            {
                foreach (ObjectInternal objectInternal in UnityEngine.Object.FindObjectsOfType<ObjectInternal>())
                {
                    objectInternal.RequestOwnership();
                    Thread.Sleep(200); 
                    if (typeof(ObjectInternal).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault((FieldInfo f) => f.FieldType == typeof(Rigidbody)).GetValue(objectInternal) != null)
                    {
                        objectInternal.transform.position = PlayerManager.GetCurrentPlayer().transform.position;
                    }
                }
            }).Start();
        }
        

        // Token: 0x06000049 RID: 73 RVA: 0x00003751 File Offset: 0x00001951
        [Command(KeyCode.K, false)]
        public void SaveCurrentAvatar()
        {
            AvatarUtils.SaveAvatar(PlayerManager.GetCurrentPlayer().GetApiAvatar(), ConsoleUtils.AskInput("Enter avatar's name: ") ?? "Nameless", "");
        }

        // Token: 0x0600004A RID: 74 RVA: 0x0000377B File Offset: 0x0000197B
        [Command(KeyCode.Delete, false)]
        public void DeleteCurrentAvatar()
        {
            if (AvatarUtils.DeleteAvatar(PlayerManager.GetCurrentPlayer().GetApiAvatar()))
            {
                Console.WriteLine("Current avatar deleted.");
                return;
            }
            Console.WriteLine("No avatars found to delete.");
        }

        // Token: 0x0600004B RID: 75 RVA: 0x000037A4 File Offset: 0x000019A4
        [Command(KeyCode.Mouse2, false)]
        public void SaveAvatarId()
        {
            ApiAvatar apiAvatar = new ApiAvatar();
            apiAvatar.id = ConsoleUtils.AskInput("Enter avatar ID: ");
            apiAvatar.Fetch(delegate (ApiContainer success)
            {
                AvatarUtils.SaveAvatar(success.Model as ApiAvatar, ConsoleUtils.AskInput("Enter avatar's name: "), "");
            }, delegate (ApiContainer error)
            {
                Console.WriteLine("Error saving avatar: {0}", error.Error);
            }, null, false);
        }
        public bool collisiont = false;
        // Token: 0x0600004C RID: 76 RVA: 0x0000380B File Offset: 0x00001A0B
        public GlobalCommands()
        {
        }
        [Command(KeyCode.N, false)]
        public void NoClip()
        {

            VRC.Player cplayer = VRC.PlayerManager.GetCurrentPlayer();
            VRCPlayer vrcPlayer = cplayer.vrcPlayer;
            ApiAvatar apiAvatar = cplayer.GetApiAvatar();
            APIUser apiuser = cplayer.GetAPIUser();
            VRCAvatarManager vrcavatarManager = vrcPlayer.GetVRCAvatarManager();
            Collider col;
            if (collisiont)
            {
                Console.WriteLine("collision off ");

                col = PlayerManager.GetCurrentPlayer().gameObject.GetComponent<Collider>();



                col.gameObject.SetActive(false);

            }
            else
            {
                Console.WriteLine("collision on ");

                 col = PlayerManager.GetCurrentPlayer().gameObject.GetComponent<Collider>();

                
              
                col.gameObject.SetActive(true);
            }
        }


        [Command(KeyCode.U, false)]
        public void WTeleport() { 
        Console.Write("Enter X:");
				float num = Convert.ToSingle(Console.ReadLine());
        Console.Write("Enter Y:");
				float num2 = Convert.ToSingle(Console.ReadLine());
        Console.Write("Enter Z:");
				float num3 = Convert.ToSingle(Console.ReadLine());
        PlayerManager.GetCurrentPlayer().gameObject.transform.position = new Vector3(num, num2, num3);
    }


    // Token: 0x0200001E RID: 30

}

    
}
