using Zenject;

//[CreateAssetMenu (menuName = "Installers/ProjectSetter")]
public class ProjectSettingInstaller : ScriptableObjectInstaller<ProjectSettingInstaller>
{
    public ProjectInstaller.Prefabs Prefabs;

    public override void InstallBindings()
    {
        Container.BindInstance(Prefabs);
    }
}
