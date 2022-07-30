using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Koyou
{
    [CreateAssetMenu(fileName = "DiverseTile", menuName = "2D/Tiles/DiverseTile")]
    public class DiverseTile : RuleTile
    {
        [SerializeField] private MatchType matchType;
        [SerializeField] private string matchMaskIn = "1";
        [SerializeField] private string matchMaskOut = "1";

        private int mMatchMaskIn;
        private int mMatchMaskOut;

        private void OnValidate()
        {
            SetDirtyInternal();
        }

        private void SetDirtyInternal()
        {
            matchMaskIn = matchMaskIn != null ? Regex.Replace(matchMaskIn, @"[^01]", "") : "0";
            if (matchType == MatchType.MatchMask)
            {
                matchMaskOut = matchMaskIn;
            }
            else
            {
                matchMaskOut = matchMaskOut != null ? Regex.Replace(matchMaskOut, @"[^01]", "") : "0";
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

            int otherMatchMaskOut = 1;
            if (other is DiverseTile diverseTile)
            {
                otherMatchMaskOut = diverseTile.mMatchMaskIn;
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

        public enum MatchType
        {
            Normal,
            MatchMask,
            MatchMaskIO,
        }
    }
}