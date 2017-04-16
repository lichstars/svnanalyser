using System;

namespace SVNAnalyser.Core
{
    public class SVNAuthor : IEquatable<SVNAuthor>
    {
        public string svnName;
        public string realName;

        public SVNAuthor(string svnName, string realName)
        {
            this.svnName = svnName;
            this.realName = realName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            SVNAuthor objAsSVNAuthor = obj as SVNAuthor;
            if (objAsSVNAuthor == null) return false;
            else return Equals(objAsSVNAuthor);
        }
        public bool Equals(SVNAuthor other)
        {
            if ((other as object) == null)
                return false;
            else
                return (this.svnName.Equals(other.svnName) && this.realName.Equals(other.realName));
        }
        public static bool operator ==(SVNAuthor b1, SVNAuthor b2)
        {            
            return b1.Equals(b2);
        }
        public static bool operator !=(SVNAuthor b1, SVNAuthor b2)
        {
            return (!b1.Equals(b2));
        }
        public override int GetHashCode()
        {
            return svnName.GetHashCode() ^ realName.GetHashCode();
        }
    }
}
