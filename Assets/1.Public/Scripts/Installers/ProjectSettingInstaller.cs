using UnityEngine;
using Zenject;

//[CreateAssetMenu (menuName = "Installers/ProjectSetter")]
public class ProjectSettingInstaller : ScriptableObjectInstaller<ProjectSettingInstaller>
{
    public ProjectInstaller.NormalPrefab prefab;
    public ProjectInstaller.PooledPrefab pooledPrefabs;
    public ProjectInstaller.GarbagePrefab garbagePrefabs;
    public ProjectInstaller.PlayerSettings playerSetting; 
    public ProjectInstaller.PlayerBaseStats playerBaseStats; 
    public ProjectInstaller.CameraSetting camSetting;
    

    public override void InstallBindings()
    {
        Container.BindInstance(prefab);
        Container.BindInstance(pooledPrefabs);
        Container.BindInstance(garbagePrefabs);
        Container.BindInstance(playerBaseStats);
        Container.BindInstance(playerSetting);
        Container.BindInstance(camSetting);
    }
}
