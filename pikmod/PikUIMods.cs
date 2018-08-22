using PikMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VRC;
using VRC.Core;
using VRLoader.Attributes;
using VRLoader.Modules;
public class PikUIMods : MonoBehaviour
{
    private MethodInfo _getApiUser = typeof(Player).GetProperties().FirstOrDefault(p => p.PropertyType == typeof(APIUser))?.GetGetMethod();
    // Token: 0x04000019 RID: 25 
    private APIUser GetAPIUser(Player player) => (APIUser)_getApiUser.Invoke(player, null);
    public bool flying = false;
    private Vector3 originalGravity;
    private GameObject self = new GameObject();
    public bool pikgui = false;
    public float hSliderValue = Physics.gravity.y;
    public float xrSliderValue = 0f;
    public float yrSliderValue = 0f;
    public float zrSliderValue = 0f;
    public GlobalCommands com;
    public bool gravityt = false;
    public String Target = "target";
    public GameObject selbutton;
    public bool jump = false;       
    VRC.Player player = PlayerManager.GetCurrentPlayer();


    public void OnGUI()
    {
        if (RoomManagerBase.currentRoom != null)
        {
            int i = 0;
            PlayerManager.GetAllPlayers().ToList().ForEach(p => GUI.Label(new Rect(1, i++ * 15, 300, 20), string.Format("{0}", GetAPIUser(p).displayName)));

            if (pikgui == true)
            {
                float uix = Screen.width * 0.1f;
                float uiy = Screen.height * 0.1f;
                GUI.Box(new Rect(uix, uiy, Screen.width * 0.8f, Screen.height * 0.8f), "Pik hack");

                if (GUI.Button(new Rect(uix, uiy, 100, 30), "Fly"))
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
                if (GUI.Button(new Rect(uix, uiy + 100, 200, 30), "Toggle Custom Gravity"))
                {
                    gravityt = !gravityt;
                    if (gravityt == false)
                    {
                        this.originalGravity = Physics.gravity;
                        Physics.gravity = new Vector3(0, hSliderValue, 0);
                    }
                    else
                    {
                        Physics.gravity = this.originalGravity;
                    }
                }

                hSliderValue = GUI.HorizontalSlider(new Rect(uix, uiy + 50, 300, 100), hSliderValue, -10F, 10.0F);


                xrSliderValue = GUI.HorizontalSlider(new Rect(uix, uiy + 350, 300, 100), xrSliderValue, 0F, 360F);
                yrSliderValue = GUI.HorizontalSlider(new Rect(uix, uiy + 400, 300, 100), yrSliderValue, 0F, 360F);
                zrSliderValue = GUI.HorizontalSlider(new Rect(uix, uiy + 450, 300, 100), zrSliderValue, 0F, 360F);

                PlayerUtils.Rotate(player, xrSliderValue, yrSliderValue, zrSliderValue);


                GUI.Label(new Rect(uix, uiy + 150, 200, 50), "Current Gravity: " + Physics.gravity.y.ToString());



                if (GUI.Button(new Rect(uix, uiy + 250, 100, 30), "Teleport"))
                {

                    VRC.Player player = PlayerUtils.FindPlayer(Target);
                    PlayerUtils.TeleportTo(player);
                    Console.WriteLine("Teleporting to: {0}", player.GetAPIUser().displayName);
                }
                // int l = 0;


                // int m = 0;
                if (GUI.Button(new Rect(uix, uiy + 300, 200, 30), "Jump"))
                {
                    if (VRCPlayer.Instance.gameObject.GetComponent<PlayerModComponentJump>())
                    {
                        Console.WriteLine("World already has jump enabled.");
                        return;
                    }
                    else
                    {
                        VRCPlayer.Instance.gameObject.AddComponent<PlayerModComponentJump>();
                        Console.WriteLine("Enabled jumping in current world.");
                    }
                }

            }
            GUI.Label(new Rect(0f, (float)Screen.height - 100f, 1000f, 1000f), string.Concat(new object[]
            {
                "<b><color=#08ff00>Current Position:</color> <color=#ff0000>X:",
                Math.Round(PlayerManager.GetCurrentPlayer().gameObject.transform.position.x),
                "/Y:",
                Math.Round(PlayerManager.GetCurrentPlayer().gameObject.transform.position.y),
                "/Z:",
               Math.Round( PlayerManager.GetCurrentPlayer().gameObject.transform.position.z) + "</color></b>"
            }));

        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (RoomManagerBase.currentRoom != null)
        {



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
        }
        
}

}
