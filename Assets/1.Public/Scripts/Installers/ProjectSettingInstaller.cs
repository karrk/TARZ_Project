using UnityEngine;
using Zenject;

//[CreateAssetMenu (menuName = "Installers/ProjectSetter")]
public class ProjectSettingInstaller : ScriptableObjectInstaller<ProjectSettingInstaller>
{
    public ProjectInstaller.NormalPrefab prefab;
    public ProjectInstaller.PooledPrefab pooledPrefabs;
    //public ProjectInstaller.GarbagePrefab garbagePrefabs;
    public ProjectInstaller.PlayerBaseStats playerBaseStats; 
    public ProjectInstaller.CameraSetting camSetting;
    public ProjectInstaller.LockOnSetting lockOnSetting;

    public ProjectInstaller.InventorySetting inventorySettings;
    public ProjectInstaller.PlayerSettings playerSettings;

    public ProjectInstaller.MonsterStats monsterStatSetting;

    public ProjectInstaller.SoundSetting soundSetting;

    public override void InstallBindings()
    {
        Container.BindInstance(prefab);
        Container.BindInstance(pooledPrefabs);
        //Container.BindInstance(garbagePrefabs);
        Container.BindInstance(playerBaseStats);
        Container.BindInstance(camSetting);
        Container.BindInstance(playerSettings);
        Container.BindInstance(inventorySettings);
        Container.BindInstance(lockOnSetting);
        Container.BindInstance(soundSetting);
        Container.BindInstance(monsterStatSetting);
    }
}
