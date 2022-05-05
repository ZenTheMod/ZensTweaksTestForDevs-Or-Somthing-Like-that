using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QwertysRandomContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using ZensTweakstest.Helper;
using ZensTweakstest.Items.CryoDepths.Enfyshing;

namespace ZensTweakstest.Items.CryoDepths
{
    public class JellyMembrane : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("JellyFish Membrane");
            Tooltip.SetDefault("Do jelly fish even have brains... wait its brane." +
                "\nSummons Enfyshing");//SUS SUS AMONGUS
        }
        public override void SetDefaults()
        {
            item.width = 38;//change
            item.height = 38;//change
            item.value = Item.sellPrice(0, 15, 5, 5);
            item.rare = 4;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTurn = true;
            item.useAnimation = 30;
            item.maxStack = 30;
            item.useTime = 30;
            item.autoReuse = true;
            item.consumable = true;
        }
        public override bool UseItem(Player player)
        {
            if (player.GetModPlayer<Charred_Life>().CryoSpace && NPC.downedBoss1 && !NPC.AnyNPCs(ModContent.NPCType<Enfyshing.Enfyshing>()))
            {
                for (int x = 0; x < Main.maxTilesX; x++)
                {
                    for (int y = 0; y < Main.maxTilesY; y++)
                    {
                        if (Framing.GetTileSafely(x, y).active() && (Framing.GetTileSafely(x, y).type == ModContent.TileType<EndothermicAlter>()) && (Framing.GetTileSafely(x, y).frameX == 18) && (Framing.GetTileSafely(x, y).frameY == 0) )
                        {
                            Vector2 Spawn = new Vector2(x * 16 + 8, y * 16 - 16);
                            if (Vector2.Distance(player.position, Spawn) <= 200)
                            {
                                Main.NewText("Something...", 255, 1, 0, false);
                                NPC.NewNPC((int)Spawn.X, (int)Spawn.Y, ModContent.NPCType<Enfyshing.Enfyshing>());
                                player.CameraShake(23, 50); 
                            }
                            return true;
                        }
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.mod == "Terraria" && line.Name == "ItemName")
            {
                // we calculate the rectangle 
                Vector2 messageSize = Helplul.MeasureString(line.text);
                Rectangle rec = new Rectangle(line.X - 40, line.Y - 2, (int)messageSize.X + 88, (int)messageSize.Y);
                // we end and begin a Immediate spriteBatch for shader
                Main.spriteBatch.BeginImmediate(true, true);
                GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
                Main.spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Helper/NotMyBalls"), rec, Color.Black);
                Main.spriteBatch.BeginImmediate(true, true, true);
                GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
                Color color = Helplul.CycleColor(Color.LightBlue, Color.Blue);
                Main.spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Helper/NotMyBalls"), rec, color);
                // we end and begin a deffered spriteBatch so it doesnt break everything
                Main.spriteBatch.BeginImmediate(true, true);
                // return false so vanilla doesnt draw it
                Main.spriteBatch.End(); //end and begin main.spritebatch to apply a shader
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(ItemID.BlueAcidDye), item, null); //use living rainbow dye shader
                Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1); //draw the tooltip manually
                Main.spriteBatch.End(); //then end and begin again to make remaining tooltip lines draw in the default way
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }
    }
}
