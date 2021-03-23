using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Vupa
{
    [Serializable]
    public  class Highscore
    {
       public int Score { get; set; }
        public string Name { get; set; }


        public List<Highscore> highScores = new List<Highscore>();

       
        
    }
}
