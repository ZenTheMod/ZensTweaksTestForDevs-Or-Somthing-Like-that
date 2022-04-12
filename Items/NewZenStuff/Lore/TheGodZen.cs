using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Items;
using Microsoft.Xna.Framework;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.NewZenStuff.Lore
{
    public class TheGodZen : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zen");
            Tooltip.SetDefault("Even after the Moon Lords defeat, Zen stayed imprissoned behind 'Celestial Bars'." +
                "\nHis rage only fueled the dreadful infection left behind." +
                "\nHe sent a signal eons ago it bounces around space awaiting a response." +
                "\n-Combine all the Zen related lores for the summon.-");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override string Texture => ModContent.GetInstance<SpriteSettings>().MostClassicSprites ? base.Texture + "OLD" : base.Texture;

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 36;
            item.maxStack = 1;
            item.rare = ItemRarityID.Cyan;
        }

        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange *= 3;
        }

        public override bool GrabStyle(Player player)
        {
            Vector2 vectorItemToPlayer = player.Center - item.Center;
            Vector2 movement = -vectorItemToPlayer.SafeNormalize(default(Vector2)) * 0.1f;
            item.velocity = item.velocity + movement;
            item.velocity = Collision.TileCollision(item.position, item.velocity, item.width, item.height);
            return true;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.WhiteSmoke.ToVector3() * 1.5f * Main.essScale);
        }
    }
}