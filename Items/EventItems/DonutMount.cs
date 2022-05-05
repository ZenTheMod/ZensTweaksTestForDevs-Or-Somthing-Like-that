using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items.EventItems
{
    public class DonutMount : ModMountData
    {
        public override void SetDefaults()
        {
            mountData.buff = mod.BuffType("DonutMountBuff");
            mountData.heightBoost = 0;
            mountData.fallDamage = 0f;
            mountData.runSpeed = 4f;
            mountData.dashSpeed = 2f;
            mountData.flightTimeMax = 0;
            mountData.fatigueMax = 0;
            mountData.jumpHeight = 4;
            mountData.acceleration = 0.2f;
            mountData.jumpSpeed = 10f;
            mountData.blockExtraJumps = false;
            mountData.totalFrames = 5;
            mountData.constantJump = true;
            mountData.spawnDust = DustID.PinkSlime;
            int[] array = new int[mountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 19;
            }
            mountData.playerYOffsets = array;
            mountData.xOffset = 0;
            mountData.bodyFrame = 3;
            mountData.yOffset = 4;
            mountData.playerHeadOffset = 0;
            mountData.standingFrameCount = 1;
            mountData.standingFrameDelay = 12;
            mountData.standingFrameStart = 0;
            // mountData.runningFrameCount = 4;
            // mountData.runningFrameDelay = 65;
            // mountData.runningFrameStart = 4;
            // if (Main.player.velocity.X = mountData.runSpeed) {
            // mountData.runningFrameCount = 1;
            // mountData.runningFrameDelay = 12;
            // mountData.runningFrameStart = 8;
            // }
            mountData.flyingFrameCount = 0;
            mountData.flyingFrameDelay = 0;
            mountData.flyingFrameStart = 0;
            mountData.inAirFrameCount = 4;
            mountData.inAirFrameDelay = 12;
            mountData.inAirFrameStart = 1;
            mountData.idleFrameCount = 1;
            mountData.idleFrameDelay = 12;
            mountData.idleFrameStart = 0;
            mountData.runningFrameCount = 4;
            mountData.runningFrameDelay = 12;
            mountData.runningFrameStart = 1;
            mountData.idleFrameLoop = true;
            mountData.swimFrameCount = mountData.inAirFrameCount;
            mountData.swimFrameDelay = mountData.inAirFrameDelay;
            mountData.swimFrameStart = mountData.inAirFrameStart;
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            mountData.textureWidth = mountData.backTexture.Width;
            mountData.textureHeight = mountData.backTexture.Height;
        }
        public override void UpdateEffects(Player player)
        {
            if (player.wet)
            {
                mountData.runSpeed = 5f;
                mountData.acceleration = 0.3f;
                mountData.jumpHeight = 10;
                mountData.jumpSpeed = 2f;
                mountData.flightTimeMax = int.MaxValue - 1;
                mountData.fatigueMax = int.MaxValue - 1;
            }
            else
            {
                mountData.jumpHeight = 20;
                mountData.acceleration = 0.9f;
                mountData.jumpSpeed = 6f;
                mountData.runSpeed = 9f;
                mountData.flightTimeMax = 0;
                mountData.fatigueMax = 0;
                mountData.usesHover = false;
            }
            if (player.direction == -1)
            {
                if (player.velocity.X != 0) 
                {
                    Dust.NewDustDirect(player.position + new Vector2(10, 40), 2, 2, DustID.Fire, 5, 0); 
                }
            }
            else
            {
                if (player.velocity.X != 0)
                {
                    Dust.NewDustDirect(player.position + new Vector2(10, 40), 2, 2, DustID.Fire, -5, 0);
                }
            }
            if (Main.rand.Next(1, 9) == 2 && player.velocity.X != 0)
            {
                Main.PlaySound(SoundID.Item34, player.position);
            }
        }
    }
    public class DonutMountBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Donut Shoes");
            Description.SetDefault("A circular bent version of the Dunkin Donuts logo.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<DonutMount>(), player);
            player.buffTime[buffIndex] = 10;
        }
    }
    public class DonutShoes : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chewed Donut");
            Tooltip.SetDefault("Made By Event \"winer\" Mellyn" +
                "\nWho stole my lumch???");
        }

        public override void SetDefaults()
        {
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 2;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item25;
            item.noMelee = true;
            item.mountType = mod.MountType("DonutMount");
        }
    }
}
