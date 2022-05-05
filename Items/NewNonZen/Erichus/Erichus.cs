using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace ZensTweakstest.Items.NewNonZen.Erichus
{
    public class Erichus : ModItem
    {
        public override void SetStaticDefaults()
        {
            DateTime dateTime = DateTime.Now;
            Tooltip.SetDefault(@"A creation so unholy, a forgetable monster...
Left to rot away in containment...
Forgeting about it the containment was to be used for somthing else...
Opening the containment broke it free...
Following the hallways to an exit as if it had worked here...
We tried everything to stop it...
Our efforts failed...
Out to wander the world...
7889" + dateTime.Year);
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 36;
            item.maxStack = 1;
            item.rare = ItemRarityID.Pink;
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
            Lighting.AddLight(item.Center, Color.Green.ToVector3() * 1.5f * Main.essScale);
        }
    }
}
