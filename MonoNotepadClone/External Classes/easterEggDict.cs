using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoNotepadClone.External_Classes
{
    public class easterEggDict
    {
        public static Dictionary<String, String> bandNames = new Dictionary<String, String>();
        public void initialiseDictionary()
        {
            //Whitechapel
            bandNames.Add("Whitechapel", "somatic");
            bandNames.Add("whitechapel", "somatic");
            //Job for a Cowboy
            bandNames.Add("Job for a Cowboy", "doom");
            bandNames.Add("jfac", "doom");
            bandNames.Add("job for a cowboy", "doom");
            //
            bandNames.Add("Killswitch Engage", "disarm");
            bandNames.Add("kse", "disarm");
            //
            bandNames.Add("August Burns Red", "rescue");
            bandNames.Add("ABR", "rescue");
            bandNames.Add("august burns red", "rescue");
            //
            bandNames.Add("All That Remains", "darkenedHeart");
            bandNames.Add("atr", "darkenedHeart");
            bandNames.Add("all that remains", "darkenedHeart");
            //
            bandNames.Add("At the Gates", "terminal");
            bandNames.Add("atg", "terminal");
            bandNames.Add("at the gates", "terminal");
            //
            bandNames.Add("as i lay dying", "frail");
            bandNames.Add("As I Lay Dying", "frail");
            bandNames.Add("aild", "frail");
        }
    }
}
