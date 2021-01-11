using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RickAndMortyCharacters
{
    public class characterOrigin
    {
        String name { set; get; }
        String url { set; get; }
    }

    public class characterLocation
    {
        String name { set; get; }
        String url { set; get; }
    }

    public class character
    {
        Int16 id { set; get; }
        String name { set; get; }
        String status { set; get; }
        String species { set; get; }
        String type { set; get; }
        String gender { set; get; }
        characterOrigin origin { set; get; }
        characterLocation location { set; get; }
        String image { set; get; }
        List<String> episode { set; get; } 
        String url { set; get; } 
        String created { set; get; }
    }
}
