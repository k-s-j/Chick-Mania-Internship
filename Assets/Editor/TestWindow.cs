using UnityEngine;
using UnityEditor;

public class TestWindow : EditorWindow
{


[MenuItem("Tools/WindowTest")]
public static void Open()
    {
            TestWindow wnd = GetWindow<TestWindow>();

    }
}
