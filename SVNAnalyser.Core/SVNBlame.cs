using System;

namespace SVNAnalyser.Core
{
    public class SVNBlame : IEquatable<SVNBlame>
    {
        public SVNAuthor author { get; set; }
        public int linesChanged { get; set; }
        public string path { get; set; }
        public double ratioForThisPath { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            SVNBlame objAsBlame = obj as SVNBlame;
            if (objAsBlame == null) return false;
            else return Equals(objAsBlame);
        }
        public bool Equals(SVNBlame other)
        {
            return (this.author.Equals(other.author) && this.path.Equals(other.path));
        }
        public static bool operator ==(SVNBlame b1, SVNBlame b2)
        {
            return b1.Equals(b2);
        }
        public static bool operator !=(SVNBlame b1, SVNBlame b2)
        {
            return (!b1.Equals(b2));
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() ^ author.GetHashCode() ^ path.GetHashCode() ^ linesChanged;
        }

        public void calculateAndSetRatio(double totalLinesChanged, double ratioRemaining)
        {
            ratioForThisPath = Math.Round(linesChanged / totalLinesChanged, 2);
            double tempRatioRemaining = ratioRemaining - ratioForThisPath;

            if (tempRatioRemaining < 0)
            {
                ratioForThisPath = 1 - ratioRemaining;
                ratioRemaining = 0;
            }
        }
        
        public bool isValid()
        {
           if (author== null)
                return false;
            else if ((path == null) || (path == String.Empty))
                return false;

            return true;
        }
    }
}
