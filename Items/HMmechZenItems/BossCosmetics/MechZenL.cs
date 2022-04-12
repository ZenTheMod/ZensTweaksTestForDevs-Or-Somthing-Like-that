using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ZensTweakstest.Items.HMmechZenItems.BossCosmetics
{
    public class MechZenL : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mech Zen");
            Tooltip.SetDefault("Banished from his realm forced to scavenge the void for anything." +
                "\nLeft alone to suffer. But not for long as he soon mechanicalized himself with the reasources he had found." +
                "\nIf You didnt come to slow him down he would become a god," +
                "\na destructive power too soon and would kill everything. But you must make upgrades fast before he gains more power than you." +
                "\nYou dont have to long.");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 36;
            item.maxStack = 1;
            item.rare = 4;
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
            Lighting.AddLight(item.Center, Color.Red.ToVector3() * 1.5f * Main.essScale);
        }
    }
}
