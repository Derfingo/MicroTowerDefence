using System.Collections.Generic;
using UnityEngine;

public class AppStartup : MonoBehaviour
{
    private void Start()
    {
        var loadingOperations = new Queue<ILoadingOperation>();
        loadingOperations.Enqueue(new MenuLoadingOperation());
        GetComponent<LoadingScreen>().Load(loadingOperations);
        Debug.Log("boot");
    }
}
