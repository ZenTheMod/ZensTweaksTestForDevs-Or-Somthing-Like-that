using Terraria;
using Terraria.ModLoader;
using System.Linq;
using Terraria.ID;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot
{
    public class Zen_Peeve_Essence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flaring Essence");
        }
        public override string Texture => ModContent.GetInstance<SpriteSettings>().MostClassicSprites ? base.Texture + "OLD" : base.Texture;
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 38;
            item.rare = ItemRarityID.Yellow;
            item.value = Item.sellPrice(silver: 10);
            item.maxStack = 999;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.Red.ToVector3() * 0.75f * Main.essScale);
        }
    }
}
