using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guess
{
    public class GameType
    {
        public string Name { get; set; }
        public string ImageLocation { get; set; }
        public string Description { get; set; }
        public List<string> Cards { get; set; }
        public int SortOrder { get; set; }
    }
}
