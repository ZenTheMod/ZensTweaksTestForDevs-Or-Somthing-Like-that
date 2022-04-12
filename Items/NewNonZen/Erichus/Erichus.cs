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
            Tooltip.SetDefault(@"He was always different hiding in the corner and such...
His experiments never worked even when following a guide...
Until the fated day, he found it! just what he needed...
A energy source capable of fixing everything he failed at...
Until his life started going downhill...
Bursting and lashing out at us...
So naturally he wanted to fix his life...
He was not at work the next day...
So I went to check on him...
I reluctantly opened the stained door...
All I could see was a open window...
" + "\"He was gone.\"");
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
