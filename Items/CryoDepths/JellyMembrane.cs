using Microsoft.Xna.Framework;
using QwertysRandomContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
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
                Main.NewText("The sky runs AMONGUS with terror.", 255, 1, 0, false);
                Vector2 IceSpawn = new Vector2(ZenWorld.IceX * 16, (WorldGen.lavaLine - 150) * 16);
                NPC.NewNPC((int)IceSpawn.X, (int)IceSpawn.Y, ModContent.NPCType<Enfyshing.Enfyshing>());
                player.CameraShake(23, 50);
                int numberofproj = 15;
                for (int i = 0; i < numberofproj; i++)
                {
                    Vector2 spawnPos = IceSpawn;
                    Vector2 VelPos = new Vector2((float)Math.Cos((float)2 * i * MathHelper.Pi / numberofproj), (float)Math.Sin((float)2 * i * MathHelper.Pi / numberofproj));
                    VelPos.Normalize();
                    Projectile.NewProjectile(spawnPos, VelPos * 8f, ModContent.ProjectileType<AquaSoulProj>(), item.damage, 5f, Main.myPlayer);
                    Projectile.NewProjectile(spawnPos, VelPos * 4f, ModContent.ProjectileType<AquaSoulProj>(), item.damage, 5f, Main.myPlayer);
                    Projectile.NewProjectile(spawnPos, VelPos * 2f, ModContent.ProjectileType<AquaSoulProj>(), item.damage, 5f, Main.myPlayer);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
