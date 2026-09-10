using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : KienMonoBehaviour
{
    protected override void Start()
    {
        base.Start();
        SceneManager.LoadScene("Menu");
    }
}
