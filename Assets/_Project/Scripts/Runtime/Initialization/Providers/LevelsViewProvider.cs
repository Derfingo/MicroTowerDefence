using System.Threading.Tasks;

namespace MicroTowerDefence
{
    public class LevelsViewProvider : LocalAssetLoaderBase
    {
        public Task<LevelsView> Load()
        {
            return LoadInternal<LevelsView>("Levels View");
        }

        public void Unload()
        {
            UnloadInternal();
        }
    }
}
