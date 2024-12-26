using UnityEngine;
using UnityEngine.SceneManagement;

public class EmptyScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(1);
    }
}
