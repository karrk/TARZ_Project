using UnityEngine;
using Zenject;

public class SceneLoadTest : MonoBehaviour 
{
    [SerializeField] private string sceneName;
    [Inject] private SignalBus signal;

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
            signal.Fire<StageEndSignal>();

            SceneChanger.LoadScene(sceneName);
        }
    }
}
