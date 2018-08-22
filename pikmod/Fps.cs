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
public class Fps : MonoBehaviour
{

    public void Start()
    {
    }

    // Token: 0x06000002 RID: 2 RVA: 0x00002052 File Offset: 0x00000252
    public void Update()
    {
        this.deltaTime += (Time.unscaledDeltaTime - this.deltaTime) * 0.1f;
        if (Input.GetKeyDown(KeyCode.I))
        {
            showfps = !showfps;
        }
    
        }

    // Token: 0x06000003 RID: 3 RVA: 0x00002073 File Offset: 0x00000273
  

    // Token: 0x06000004 RID: 4 RVA: 0x0000207C File Offset: 0x0000027C
    public void OnGUI()
    {
        if (showfps) { 
        int width = Screen.width;
        int height = Screen.height;
        GUIStyle guistyle = new GUIStyle();
        Rect rect = new Rect(width - 200, 0f, (float)width, (float)(height * 2 / 100));
        guistyle.alignment = 0;
        guistyle.fontSize = height * 2 / 100;
        guistyle.normal.textColor = new Color(0f, 1f, 0f, 1f);
        float num = this.deltaTime * 1000f;
        float num2 = 1f / this.deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", num, num2);
        GUI.Label(rect, text, guistyle);
        }
    }

    // Token: 0x04000001 RID: 1
    private float deltaTime;
    private bool showfps = true;
}


