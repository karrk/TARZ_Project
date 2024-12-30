using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTester : MonoBehaviour 
{
    [SerializeField] private string sceneName;

    //private void Update() 
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    { 
    //        SceneChanger.LoadScene(sceneName);
    //    } 
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SceneChanger.LoadScene(sceneName);
        }
    }
}
