using UnityEngine;

public class StartGame : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.StartGame();
        }
    }
}
