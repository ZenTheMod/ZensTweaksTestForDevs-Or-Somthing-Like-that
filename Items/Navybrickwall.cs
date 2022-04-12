using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System.Text;
using System.Threading.Tasks;

namespace ZensTweakstest.Items
{
    public class Navybrickwall : ModWall
    {

        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            drop = drop = mod.ItemType("nbw_Item");
            AddMapEntry(new Color(41, 145, 135));
            dustType = DustID.Water_Snow;
        }
    }
}
