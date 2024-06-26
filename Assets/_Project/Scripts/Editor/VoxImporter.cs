using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace MicroTowerDefence
{
    [ScriptedImporter(1, "vox")]
    public class VoxImporter : ScriptedImporter
    {
        public float m_Scale = 1;
        private int s = 1;
        private string _name;

        public override void OnImportAsset(AssetImportContext ctx)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //var position = JsonUtility.FromJson<Vector3>(File.ReadAllText(ctx.assetPath));
            Vector3 position = Vector3.zero;

            cube.transform.position = position;
            cube.transform.localScale = new Vector3(m_Scale, m_Scale, m_Scale);

            // 'cube' is a GameObject and will be automatically converted into a prefab
            // (Only the 'Main Asset' is eligible to become a Prefab.)
            ctx.AddObjectToAsset("main obj", cube);
            ctx.SetMainObject(cube);

            var material = new Material(Shader.Find("Universal Render Pipeline/Lit"))
            {
                name = "Prefab Material",
                color = Color.gray
            };

            // Assets must be assigned a unique identifier string consistent across imports
            ctx.AddObjectToAsset("Prefab Material", material);

            // Assets that are not passed into the context as import outputs must be destroyed
            var tempMesh = new Mesh();
            DestroyImmediate(tempMesh);
        }
    }
}
