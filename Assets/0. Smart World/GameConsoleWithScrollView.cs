using System.Text;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//todo: добавить больше строчек,
public class GameConsoleWithScrollView : MonoBehaviour
{
    public bool show_output = true;
    public bool show_stack = false;
    public static GameConsoleWithScrollView I;


    void Awake()
    {
        I = this;
        strb.AppendLine("CONSOLE:");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)) 
        {
            show = !show;
            Debug.Log("~");
        }
    }

    //error это пользовательский, а exception - это системный "ошипка"?
    int error_count = 0;

    System.Text.StringBuilder strb = new System.Text.StringBuilder();

    void OnEnable()
    {
        Application.RegisterLogCallback(HandleLog);
    }
    void OnDisable()
    {
        Application.RegisterLogCallback(null);
    }
    //чтобы сделать цветные строки нужен массив строк здесь)
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Exception || type == LogType.Error)
        {
            error_count++;
        }

        if (show_output || show_stack)
        {
            //strb.Append("\n");
            if (show_output)
            {
                strb.AppendLine(logString);
            }
            //вписываем стек всегда если есть ошибка
            if (show_stack || type == LogType.Exception || type == LogType.Error)
            {
                strb.AppendLine(stackTrace);
            }
        }
    }


    public Rect pos_rect = new Rect(50, 75, 400, 400);
    public Rect view_rect = new Rect(0, 0, Screen.width - 100f, 60000);
    Vector2 scroll_pos;
    public bool show = false;
    public void OnGUI()
    {
        //GUI.skin = SandboxEditorGUI.I.skin;
        //if (EditorController.I.isDebug)
        //{
            if (GUILayout.Button("Console"))
            {
                show = !show;
            }
            if (GUILayout.Button("Clean console"))
            {
                strb = new StringBuilder();
                strb.AppendLine("/clean");
            }
        //}
        if (show)
        {
            //strb!
            pos_rect = new Rect(50f, 75f, Screen.width - 100f, Screen.height - 150f);
            GUI.Label(new Rect(pos_rect.x, pos_rect.y - 20f, 200f, 50f), "[errors " + error_count + "] length: " + strb.Length, "box");

            scroll_pos = GUI.BeginScrollView(pos_rect, scroll_pos, view_rect);
            GUI.TextArea(new Rect(0, 0, Screen.width - 100f, view_rect.height), strb.ToString());
            GUI.EndScrollView();
        }
    }
}