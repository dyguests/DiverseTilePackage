using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Koyou
{
    [CreateAssetMenu(fileName = "DiverseTile", menuName = "2D/Tiles/DiverseTile")]
    public class DiverseTile : KoyouRuleTile
    {
        [SerializeField] private MatchType matchType;

        [Tooltip("Binary Digits")] [SerializeField]
        private string matchMaskIn = "00000000000000000000000000000001";

        [Tooltip("Binary Digits")] [SerializeField]
        private string matchMaskOut = "00000000000000000000000000000001";

        private int mMatchMaskIn;
        private int mMatchMaskOut;

        private void OnValidate()
        {
            SetDirtyInternal();
        }

        private void SetDirtyInternal()
        {
            if (!string.IsNullOrEmpty(matchMaskIn))
            {
                matchMaskIn = Regex.Replace(matchMaskIn, @"[^01]", "");
            }

            if (string.IsNullOrEmpty(matchMaskIn))
            {
                matchMaskIn = "00000000000000000000000000000001";
            }

            if (matchType == MatchType.MatchMask)
            {
                matchMaskOut = matchMaskIn;
            }
            else
            {
                if (!string.IsNullOrEmpty(matchMaskOut))
                {
                    matchMaskOut = Regex.Replace(matchMaskOut, @"[^01]", "");
                }

                if (string.IsNullOrEmpty(matchMaskOut))
                {
                    matchMaskOut = "00000000000000000000000000000001";
                }
            }

            mMatchMaskIn = int.Parse(matchMaskIn);
            mMatchMaskOut = int.Parse(matchMaskOut);
        }

        public override bool RuleMatch(int neighbor, TileBase other)
        {
            if (matchType == MatchType.Normal)
            {
                return base.RuleMatch(neighbor, other);
            }

            if (other is RuleOverrideTile) other = (other as RuleOverrideTile).m_InstanceTile;

            int otherMatchMaskOut = 0;
            if (other is DiverseTile diverseTile)
            {
                otherMatchMaskOut = diverseTile.mMatchMaskOut;
            }
            else if (other is RuleTile ruleTile)
            {
                otherMatchMaskOut = 1;
            }

            if (matchType == MatchType.MatchMask || matchType == MatchType.MatchMaskIO)
            {
                switch (neighbor)
                {
                    case TilingRuleOutput.Neighbor.This: return (otherMatchMaskOut & mMatchMaskIn) > 0;
                    case TilingRuleOutput.Neighbor.NotThis: return (otherMatchMaskOut & mMatchMaskIn) == 0;
                }
            }

            return true;
        }

        public override bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, int angle)
        {
            return base.RuleMatches(rule, position, tilemap, angle);
        }

        public override bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, bool mirrorX, bool mirrorY)
        {
            return base.RuleMatches(rule, position, tilemap, mirrorX, mirrorY);
        }

        public enum MatchType
        {
            Normal,
            MatchMask,
            MatchMaskIO,
        }
    }
}