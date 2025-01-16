using UnityEngine.SceneManagement;
using Zenject;

public class StageController : IInitializable
{
    [Inject] private PlayerStats playerStats;
    [Inject] private SoundManager soundmanager;
    [Inject] private LobbyData lobby;

    public void Initialize()
    {
        playerStats.SceneChangedFunction();

        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "FixTest1":
                soundmanager.PlayBGM(E_Audio.Stage_1_BGM);
                break;
            case "FixTest2":
                soundmanager.PlayBGM(E_Audio.Stage_1_BGM);
                break;
            case "FixTest3":
                soundmanager.PlayBGM(E_Audio.None);
                break;

        }
        
    }
}
