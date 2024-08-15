using System.Threading.Tasks;

namespace MicroTowerDefence
{
    public class SettingsViewProvider : LocalAssetLoaderBase
    {
        public Task<SettingsView> Load()
        {
            return LoadInternal<SettingsView>("Settings View");
        }

        public void Unload()
        {
            UnloadInternal();
        }
    }
}
