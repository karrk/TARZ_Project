using UnityEngine;
using Zenject;

public class CharacterSpawner : MonoBehaviour
{
    [Inject]
    private void Init(ProjectPlayer player)
    {
        player.transform.position = this.transform.position;
    }
}
