using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private SceneRouter _sceneRouter;
    
    private void Start()
    {
        _sceneRouter = GameManager.Instance.GetSceneRouter();
    }

    public void ReturnToMenu()
    {
        _sceneRouter.ToMainMenu();
    }
}
