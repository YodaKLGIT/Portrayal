using UnityEngine;
using UnityEditor;
using System.IO;

public class TerrainPathMaskBaker : MonoBehaviour
{
    public Terrain terrain;

    [Header("Your Path Layer Index")]
    public int pathLayer = 2;

    [Header("Output")]
    public string fileName = "GrassMask.png";

    [ContextMenu("Bake Path Mask")]
    public void BakeMask()
    {
        if (terrain == null)
        {
            Debug.LogError("No terrain assigned.");
            return;
        }

        TerrainData data = terrain.terrainData;

        int width = data.alphamapWidth;
        int height = data.alphamapHeight;

        float[,,] splat = data.GetAlphamaps(0, 0, width, height);

        Texture2D maskTex = new Texture2D(width, height, TextureFormat.RGBA32, false);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Path strength (0 = no path, 1 = full path)
                float pathValue = splat[y, x, pathLayer];

                // Convert path -> grass mask
                float mask = 1f - pathValue;

                mask = Mathf.Clamp01(mask);

                // Optional: sharpen edges a bit (makes paths cleaner)
                mask = Mathf.Pow(mask, 1.5f);

                maskTex.SetPixel(x, y, new Color(mask, mask, mask, 1));
            }
        }

        maskTex.Apply();

        byte[] png = maskTex.EncodeToPNG();

        string path = Path.Combine(Application.dataPath, fileName);
        File.WriteAllBytes(path, png);

        Debug.Log("Grass mask saved to: " + path);

        AssetDatabase.Refresh();
    }
}