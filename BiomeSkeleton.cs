using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace BiomeLibrary
{
    public class BiomeSkeleton
    {
        public Collection<String> blockList;
        public int biomeTileCount = 0;
        public byte biomeID;
        private int minTileRequired;
        private bool valid;
        private bool isSpreading;

        private Func<bool> condition;

        public Mod mod;

        public BiomeSkeleton(int minTileRequired, Mod mod)
        {
            this.minTileRequired = minTileRequired;
            blockList = new Collection<string>();
            this.mod = mod;
        }

        public void resetTileCount()
        {
            biomeTileCount = 0;
        }

        public int tileCount(int[] tileCounts)
        {
            biomeTileCount = 0;
            Vector2 plr_pos = Main.player[Main.myPlayer].Center / 16;
            Vector2 sc_size = new Vector2(Main.screenWidth, Main.screenHeight) / 32;
            int light_size = 20;

            for (int x = 0; x < blockList.Count; x++)
            {
                for (int i = (int)(plr_pos.X - sc_size.X) - light_size; i < (plr_pos.X + sc_size.X) + light_size; i++)
                {
                    for (int j = (int)(plr_pos.Y - sc_size.Y) - light_size; j < (plr_pos.Y + sc_size.Y) + light_size; j++)
                    {
                        //check for tiles based on Main.tile[i, j].type
						
                        if (checkXCoord(i) && checkYCoord(j) && Main.tile[i, j].type == (ushort)mod.TileType(blockList[x]))
                        {
                            biomeTileCount++;
                        }
                    }
                }
            }
            return biomeTileCount;
        }

        public void registerTile(String[] blockName)
        {
            String text = "";
            for (int i = 0; i < blockName.Length; i++)
            {
                blockList.Add(blockName[i]);
                text += blockList[i] + " ";
            }
        }

        public bool isValid()
        {
            bool c = true;
            if (condition != null) {
                c = condition.Invoke();
            }
            return biomeTileCount > minTileRequired && c;
        }
		
		public bool checkYCoord(int j) {
            return j > 0 && j < Main.ActiveWorldFileData.WorldSizeY;
		}

        public bool checkXCoord(int i)
        {
            return i > 0 && i < Main.ActiveWorldFileData.WorldSizeX;
        }

        public void SetCondition(Func<bool> flag)
        {
             condition = flag;
        }

        public void SetMinTile(int minTile) {
            minTileRequired = minTile;
        } 
    }
}
