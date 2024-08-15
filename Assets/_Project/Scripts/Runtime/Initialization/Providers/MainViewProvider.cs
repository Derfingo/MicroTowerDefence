using System.Threading.Tasks;

namespace MicroTowerDefence
{
    public class MainViewProvider : LocalAssetLoaderBase
    {
        public Task<MainView> Load()
        {
            return LoadInternal<MainView>("Main View");
        }

        public void Unload()
        {
            UnloadInternal();
        }
    }
}