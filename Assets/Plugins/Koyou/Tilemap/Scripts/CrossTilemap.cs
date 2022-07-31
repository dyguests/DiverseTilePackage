using UnityEngine;
using UnityEngine.Tilemaps;

namespace Koyou
{
    [RequireComponent(typeof(Tilemap))]
    public class CrossTilemap : MonoBehaviour
    {
        [SerializeField] private CrossTilemap[] relatedTilemaps;

        public CrossTilemap[] RelatedTilemaps => relatedTilemaps;

        private Tilemap mTilemap;

        public Tilemap Tilemap
        {
            get
            {
                if (mTilemap == null) mTilemap = GetComponent<Tilemap>();
                return mTilemap;
            }
        }
    }
}