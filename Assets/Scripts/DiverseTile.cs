using UnityEngine;
using UnityEngine.Tilemaps;

namespace Koyou
{
    [CreateAssetMenu(fileName = "DiverseTile", menuName = "2D/Tiles/DiverseTile")]
    public class DiverseTile : RuleTile
    {
        /// <summary>
        /// The RuleTile to override
        /// </summary>
        public RuleTile m_Tile;

        /// <summary>
        /// Returns the Rule Tile for retrieving TileData
        /// </summary>
        [HideInInspector] public RuleTile m_InstanceTile;


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