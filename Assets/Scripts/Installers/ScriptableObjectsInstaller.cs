using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ScriptableObjectsInstaller", menuName = "Installers/ScriptableObjectsInstaller")]
public class ScriptableObjectsInstaller : ScriptableObjectInstaller<ScriptableObjectsInstaller>
{
    public GameSettingsScriptableObject gameSettings;
    
    public override void InstallBindings()
    {
        Container.Bind<GameSettingsScriptableObject>().FromInstance(gameSettings).AsSingle();
    }
}
