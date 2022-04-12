using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Items;
using Microsoft.Xna.Framework;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.NewZenStuff.Lore
{
    public class SG_LORE : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Spark Guardian");
            Tooltip.SetDefault("A collection of powerful Zen-Stone Peeves given a vesel to possess." +
                "\nEven peeves, a uncontrolable type of spirit, can be controled." +
                "\nThis powerful collection dubbed -The Spark Guardian- lives to protect." +
                "\nEven when theirs nothing to protect.");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override string Texture => ModContent.GetInstance<SpriteSettings>().MostClassicSprites ? base.Texture + "OLD" : base.Texture;

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 36;
            item.maxStack = 1;
            item.rare = ItemRarityID.Yellow;
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
            Lighting.AddLight(item.Center, Color.SlateGray.ToVector3() * 1.9f * Main.essScale);
        }
    }
}
