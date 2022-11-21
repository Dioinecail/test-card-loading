using UnityEngine;

namespace Project.Cards
{
    public struct InfoObject
    {
        public string Name;
        public string Description;
    }

    public class CardData
    {
        // stats
        public int HP;
        public int MP;
        public int ATK;

        // id to get info from a manager or database
        public int ID;
        // this should go into a manager/database too
        // but kept here for simplicity
        public Sprite Sprite;



        public InfoObject GetInfo()
        {
            // get info from some other manager
            // or database
            return new InfoObject()
            {
                Name = "Name",
                Description = "Uses fire to burn it's foes!"
            };
        }
    }
}