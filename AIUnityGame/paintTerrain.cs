using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintTerrain : MonoBehaviour {

    [System.Serializable]
    public class SlpatHeights
    {
        public int textureIndex;
        public int startingHeight;
    }

    public SlpatHeights[] splatHeights;
    
	// Use this for initialization
	void Start () {
        TerrainData terrainData = Terrain.activeTerrain.terrainData;
        float[, ,] splatmapData = new float[terrainData.alphamapWidth,terrainData.alphamapHeight, terrainData.alphamapLayers];

        for (int y=0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                float terrainHeight = terrainData.GetHeight(y, x);

                float[] splat = new float[splatHeights.Length];

                for(int i =0; i < splatHeights.Length; i++)
                {
                    if (terrainHeight >= splatHeights[i].startingHeight)
                        splat[i] = 1;
                }

                for(int j =0; j < splatHeights.Length; j++)
                {
                    splatmapData[x, y, j] = splat[j];
                }
            }
        }

        terrainData.SetAlphamaps(0, 0, splatmapData);

	}
	
}
