using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Vupa
{
    [Serializable]
    public  class Highscore : IComparable<Highscore>
    {
       public int Score { get; set; }
        public string Name { get; set; }


        public List<Highscore> highScores = new List<Highscore>();
        /// <summary>
        /// sorts the higscore list (highest to lowest)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Highscore other)
        {
            if (other.Score > Score)
            {
                return 1;
            }
            else if (other.Score < Score)
            {
                return -1;
            }
            else
            {
                return Name.CompareTo(other.Name);
            }
        }
    }
}
