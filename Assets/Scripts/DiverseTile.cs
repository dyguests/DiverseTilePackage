using UnityEngine;
using UnityEngine.Tilemaps;

namespace Koyou
{
    [CreateAssetMenu(fileName = "DiverseTile", menuName = "2D/Tiles/DiverseTile")]
    public class DiverseTile : RuleTile
    {
        public MatchType matchType;
        public int matchMaskIn;
        public int matchMaskOut;

        public override bool RuleMatch(int neighbor, TileBase other)
        {
            return base.RuleMatch(neighbor, other);
        }

        public enum MatchType
        {
            Normal,
            MatchMask,
            MatchMaskIO,
        }
    }
}