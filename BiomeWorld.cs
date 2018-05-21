using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace BiomeLibrary
{
    public class BiomeWorld : ModWorld
    {

        public override void Load(TagCompound tag)
        {

            BiomeLibs.world = mod.GetModWorld<BiomeWorld>();
            resetList();
            base.Load(tag);
        }

        public void resetList() {
            BiomeLibs.reset();
        }


        public void addBlock(String biomeName, String[] block)
        {

            for (int i = 0; i < BiomeLibs.name.Count; i++)
            {
                if (BiomeLibs.name[i] == biomeName)
                {
                    BiomeLibs.BiomeList[biomeName].registerTile(block);
                }
            }
        }


        public override void TileCountsAvailable(int[] tileCounts)
        {
            for (int i = 0; i < BiomeLibs.name.Count; i++)
            {
                BiomeLibs.BiomeList[BiomeLibs.name[i]].tileCount(tileCounts);
            }
        }

        public override void ResetNearbyTileEffects()
        {
            for (int i = 0; i < BiomeLibs.name.Count; i++)
            {
                BiomeLibs.BiomeList[BiomeLibs.name[i]].resetTileCount();
            }
        }

        public override void ModifyHardmodeTasks(List<GenPass> list)
        {
            Main.hardMode = true;

            /*if (BiomeLibs.hallowAltList.Count != 0)
            {*/

                list[0] = (new PassLegacy("Hardmode Good", delegate
                {
                    
                    float num = (float)WorldGen.genRand.Next(300, 400) * 0.001f;
                    float num2 = (float)WorldGen.genRand.Next(200, 300) * 0.001f;
                    int num3 = (int)((float)Main.maxTilesX * num);
                    int num4 = (int)((float)Main.maxTilesX * (1f - num));
                    int num5 = 1;
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        num4 = (int)((float)Main.maxTilesX * num);
                        num3 = (int)((float)Main.maxTilesX * (1f - num));
                        num5 = -1;
                    }
                    int num6 = 1;
                    if (WorldGen.dungeonX < Main.maxTilesX / 2)
                    {
                        num6 = -1;
                    }
                    if (num6 < 0)
                    {
                        if (num4 < num3)
                        {
                            num4 = (int)((float)Main.maxTilesX * num2);
                        }
                        else
                        {
                            num3 = (int)((float)Main.maxTilesX * num2);
                        }
                    }
                    else if (num4 > num3)
                    {
                        num4 = (int)((float)Main.maxTilesX * (1f - num2));
                    }
                    else
                    {
                        num3 = (int)((float)Main.maxTilesX * (1f - num2));
                    }

                    int rand = Main.rand.Next(0,BiomeLibs.hallowAltList.Count);
                    rand = 0;
                    if (rand == 0)
                    {
                        Main.NewText("Hallow has generated", Color.Red);
                        WorldGen.GERunner(num3, 0, 3f * (float)3*num5, 5f, true);
                    }
                    else
                    {
                        string message = BiomeLibs.BiomeList[BiomeLibs.name[rand]].getMessage();
                        if (message != null)
                        {
                            Main.NewText(message, Color.Red);
                        }
                        else {
                            Main.NewText(BiomeLibs.name[rand] + "has generated", Color.Red);
                        }
                        

                        BWRunner(num3, 0, blockFinder(BiomeLibs.name[rand]), BiomeLibs.BiomeList[BiomeLibs.name[rand]].mod, (float)(3 * num5), 5f);
                    }
                }));
            //}
        }

        private String[] blockFinder(String biomeName)
        {
            String text = "";
            String[] blockList = { "Grass", "Stone", "Sand", "Dirt", "Ice" };
            text += BiomeLibs.BiomeList[biomeName].mod.Name + "           ";
            for (int i = 0; i < BiomeLibs.BiomeList[biomeName].blockList.Count; i++)
            {
                if (BiomeLibs.BiomeList[biomeName].blockList[i].Contains("dirt") ||
                    BiomeLibs.BiomeList[biomeName].blockList[i].Contains("Dirt"))
                {
                    blockList[3] = BiomeLibs.BiomeList[biomeName].blockList[i];
                }
                else if (BiomeLibs.BiomeList[biomeName].blockList[i].Contains("sand") ||
                         BiomeLibs.BiomeList[biomeName].blockList[i].Contains("Sand"))
                {
                    blockList[2] = BiomeLibs.BiomeList[biomeName].blockList[i];
                }
                else if (BiomeLibs.BiomeList[biomeName].blockList[i].Contains("stone") ||
                           BiomeLibs.BiomeList[biomeName].blockList[i].Contains("Stone"))
                {
                    blockList[1] = BiomeLibs.BiomeList[biomeName].blockList[i];
                }
                else if (BiomeLibs.BiomeList[biomeName].blockList[i].Contains("grass") ||
                    BiomeLibs.BiomeList[biomeName].blockList[i].Contains("Grass"))
                {
                    blockList[0] = BiomeLibs.BiomeList[biomeName].blockList[i];
                }
                else if (BiomeLibs.BiomeList[biomeName].blockList[i].Contains("Ice") ||
                         BiomeLibs.BiomeList[biomeName].blockList[i].Contains("ice"))
                {
                    blockList[4] = BiomeLibs.BiomeList[biomeName].blockList[i];
                }
                
            }
            return blockList;
        }

        private void BWRunner(int i, int j, String[] blockList, Mod mod, float speedX = 0f, float speedY = 0f)
        {
            String text = "";
            text += mod.Name + "           ";
            for (int x = 0; x < blockList.Length; x++)
            {
                text += blockList[x] + "  ";
            }

            int num = WorldGen.genRand.Next(200, 250);
            float num2 = (float)(Main.maxTilesX / 4200);
            num = (int)((float)num * num2);
            double num3 = (double)num;
            Vector2 value;
            value.X = (float)i;
            value.Y = (float)j;
            Vector2 value2;
            value2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            value2.Y = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            if (speedX != 0f || speedY != 0f)
            {
                value2.X = speedX;
                value2.Y = speedY;
            }
            bool flag = true;
            while (flag)
            {
                int num4 = (int)((double)value.X - num3 * 0.5);
                int num5 = (int)((double)value.X + num3 * 0.5);
                int num6 = (int)((double)value.Y - num3 * 0.5);
                int num7 = (int)((double)value.Y + num3 * 0.5);
                if (num4 < 0)
                {
                    num4 = 0;
                }
                if (num5 > Main.maxTilesX)
                {
                    num5 = Main.maxTilesX;
                }
                if (num6 < 0)
                {
                    num6 = 0;
                }
                if (num7 > Main.maxTilesY)
                {
                    num7 = Main.maxTilesY;
                }
                for (int k = num4; k < num5; k++)
                {
                    for (int l = num6; l < num7; l++)
                    {
                        if ((double)(System.Math.Abs((float)k - value.X) + System.Math.Abs((float)l - value.Y)) < (double)num * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015))
                        {
                            /*if (Main.tile[k, l].wall == 63 || Main.tile[k, l].wall == 65 || Main.tile[k, l].wall == 66 || Main.tile[k, l].wall == 68 || Main.tile[k, l].wall == 69 || Main.tile[k, l].wall == 81)
                            {
                                
                            }
                            if (Main.tile[k, l].wall == 3 || Main.tile[k, l].wall == 83)
                            {
                                Main.tile[k, l].wall = 28;
                            }*/
                            if (Main.tile[k, l].type == 0 && blockList[3] != "Dirt" && blockList[3].Contains("Dirt") || blockList[3].Contains("dirt"))
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType(blockList[3]);
                                WorldGen.SquareTileFrame(k, l, true);
                            }
                            if (Main.tile[k, l].type == 1 || Main.tile[k, l].type == 25 || Main.tile[k, l].type == 203 && blockList[1] != "Stone" && blockList[1].Contains("Stone") || blockList[1].Contains("stone"))
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType(blockList[1]);
                                WorldGen.SquareTileFrame(k, l, true);
                            }
                            if (Main.tile[k, l].type == 2 || Main.tile[k, l].type == 23 || Main.tile[k, l].type == 199 && blockList[0] != "Grass" && blockList[0].Contains("Grass") || blockList[0].Contains("grass"))
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType(blockList[0]);
                                WorldGen.SquareTileFrame(k, l, true);
                            }
                            if (Main.tile[k, l].type == 53 || Main.tile[k, l].type == 123 || Main.tile[k, l].type == 112 || Main.tile[k, l].type == 234 && blockList[2] != "Sand" && blockList[2].Contains("Sand") || blockList[2].Contains("sand"))
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType(blockList[2]);
                                WorldGen.SquareTileFrame(k, l, true);
                            }
                            if (Main.tile[k, l].type == 161 || Main.tile[k, l].type == 163 || Main.tile[k, l].type == 200 && blockList[4] != "Ice" && blockList[4].Contains("Ice") || blockList[4].Contains("ice"))
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType(blockList[4]);
                                WorldGen.SquareTileFrame(k, l, true);
                            }
                        }
                    }
                }
                value += value2;
                value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                if (value2.X > speedX + 1f)
                {
                    value2.X = speedX + 1f;
                }
                if (value2.X < speedX - 1f)
                {
                    value2.X = speedX - 1f;
                }
                if (value.X < -(float)num || value.Y < -(float)num || value.X > (float)(Main.maxTilesX + num) || value.Y > (float)(Main.maxTilesX + num))
                {
                    flag = false;
                }
            }

        }
    }
}
