using UnityEngine;
using Zenject;

//[CreateAssetMenu (menuName = "Installers/ProjectSetter")]
public class ProjectSettingInstaller : ScriptableObjectInstaller<ProjectSettingInstaller>
{
    public ProjectInstaller.NormalPrefab prefab;
    public ProjectInstaller.PooledPrefab pooledPrefabs;
    public ProjectInstaller.CameraSetting camSetting;
    

    public override void InstallBindings()
    {
        Container.BindInstance(prefab);
        Container.BindInstance(pooledPrefabs);
        Container.BindInstance(camSetting);
    }
}
