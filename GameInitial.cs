using UnityEngine;
using System.Collections;

public class GameInitial
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(600, 900, false, 60);
    }
	
	
}
