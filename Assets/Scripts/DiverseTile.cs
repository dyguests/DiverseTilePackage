using UnityEngine;
using UnityEngine.Tilemaps;

namespace Koyou
{
    [CreateAssetMenu(fileName = "DiverseTile", menuName = "2D/Tiles/DiverseTile")]
    public class DiverseTile : RuleTile
    {
        
        
        public override bool RuleMatch(int neighbor, TileBase other)
        {
            return base.RuleMatch(neighbor, other);
        }
    }
}