using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;
using ZensTweakstest.Items;
using System;

namespace ZensTweakstest.Helper
{
    public class ZCamera : ModPlayer
    {
        public int camerashake;
		public int camerashakeforced;
		public int camerashakeforcedtimer;
		public override void ModifyScreenPosition() {
			Vector2 centerScreen = new Vector2(Main.screenWidth/2,Main.screenHeight/2);
            if (camerashake > 0)
            {
                Main.screenPosition += new Vector2(Main.rand.Next(-camerashake, camerashake + 1), Main.rand.Next(-camerashake, camerashake + 1));
                camerashake -= 1;
            }
            if (camerashakeforcedtimer > 0)
            {
                Main.screenPosition += new Vector2(Main.rand.Next(-camerashakeforced, camerashakeforced + 1), Main.rand.Next(-camerashakeforced, camerashakeforced + 1));
                camerashakeforcedtimer -= 1;
            }
            else if (camerashakeforced > 0) {
                camerashake += camerashakeforced;
                camerashakeforced = 0;
            }
		}
    }
    public static class Helplul
    {
        //Player
        public static void CameraShake(this Player player, int a, int b = 0) {
            player.GetModPlayer<ZCamera>().camerashakeforcedtimer = b;
            player.GetModPlayer<ZCamera>().camerashakeforced = a;
        }
        //NPC
        public static void AddLoot(this NPC npc ,int id, int npcid = -1, int ran = 1, int stack = 1, bool a = true) {
			if (Main.rand.Next(ran) == 0 && a) {
                if (npcid == -1) {Item.NewItem(npc.getRect(), id, stack);}
				else if (npc.type == npcid) {Item.NewItem(npc.getRect(), id, stack);}
			}
		}
        public static Vector2 DirectionTo(this Vector2 a, Vector2 b) {
            NPC e = new NPC();
            e.position = a;
            return e.DirectionTo(b);
        }
        public static void AddShop(this Chest shop, ref int nextSlot, int item, int gold = -1){
            shop.item[nextSlot].SetDefaults(item);
            if (gold > -1) {shop.item[nextSlot].shopCustomPrice = gold;}
            nextSlot++;
        }
        public static bool AnyBoss(int type = -1) {
			bool flag1 = false;
			for (int i = 0; i < Main.maxNPCs; i++){
				NPC n = Main.npc[i];
                if (type > -1 && n.active && n.boss && n.type == type) {flag1 = true;}
				else if (type == -1 && n.active && (n.boss || n.type == NPCID.EaterofWorldsHead || n.type == NPCID.EaterofWorldsBody ||n.type == NPCID.EaterofWorldsTail)) {flag1 = true;}
			}
			return flag1;
		}
        //Sprite batch
        public static bool ApplyShaderOnTooltip(this Item item,DrawableTooltipLine line,int id,string ifmod = "Terraria", string ifname = "ItemName") {
            if (line.mod == ifmod && line.Name == ifname)
            {
                Main.spriteBatch.End(); //end and begin main.spritebatch to apply a shader
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(id), item, null); //use living rainbow dye shader
                Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1); //draw the tooltip manually
                Main.spriteBatch.End(); //then end and begin again to make remaining tooltip lines draw in the default way
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }
        public static void DrawChain(this SpriteBatch spriteBatch, Color lightColor,Vector2 from,Vector2 tothis, string Texture, bool nolight = false,float lengths = 25f, int spacing = 12) {
			Texture2D chainTexture = ModContent.GetTexture(Texture);
			var remainingVectorToPlayer = from - tothis;
			float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;
			// tothis while loop draws the chain texture from the projectile to the player, looping to draw the chain texture along the path
			while (true) {
				float length = remainingVectorToPlayer.Length();
				// Once the remaining length is small enough, we terminate the loop
				if (length < lengths || float.IsNaN(length))
					break;
				// tothis is advanced along the vector back to the player by 12 pixels
				// 12 comes from the height of ExampleFlailProjectileChain.png and the spacing that we desired between links
				tothis += remainingVectorToPlayer * spacing / length;
				remainingVectorToPlayer = from - tothis;
				// Finally, we draw the texture at the coordinates using the lighting information of the tile coordinates of the chain section
				Color color = Lighting.GetColor((int)tothis.X / 16, (int)(tothis.Y / 16f));
                if (nolight) {color = lightColor;}
				spriteBatch.Draw(chainTexture, tothis - Main.screenPosition, null, color, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
			}
        }
    }
}
