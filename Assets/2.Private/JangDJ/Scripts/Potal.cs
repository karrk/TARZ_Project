using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Potal : MonoBehaviour
{
    [SerializeField] private E_Scenes NextStage;
    [Inject] private SignalBus signal;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ProjectPlayer>(out ProjectPlayer _))
        {
            signal.Fire<StageEndSignal>();
            SceneManager.LoadScene(NextStage.ToString());
        }
    }
}
