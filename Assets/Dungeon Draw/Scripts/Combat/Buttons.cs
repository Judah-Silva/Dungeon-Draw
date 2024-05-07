using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    private SceneRouter _sceneRouter;

    private void Start()
    {
        _sceneRouter = GameManager.Instance.GetSceneRouter();
    }

    public void Continue()
    {
        _sceneRouter.ToMap();
    }
}
