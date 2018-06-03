using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.ModLoader;

namespace BiomeLibrary
{
    public class BiomeLibs : Mod
	{
	    public static BiomePlayer player;
	    public static Collection<String> name;
	    public static BiomeWorld world;
	    public static Mod instance;
	    public static Dictionary<String, bool> biomeRegistery;
	    public static Dictionary<String, BiomeSkeleton> BiomeList;
	    public static Collection<String> hallowAltList;


        public BiomeLibs()
		{
            
		    world = this.GetModWorld<BiomeWorld>();
		    instance = this;
		    Properties = new ModProperties()
		    {
		        Autoload = true,
		        AutoloadGores = true,
		        AutoloadSounds = true
		    };
            
        }

	    public override void Unload()
	    {
	        name = null;
            biomeRegistery = null;
            BiomeList = null;
	        instance = null;
	        hallowAltList = null;
	    }

	    public override void Load()
	    {
	        instance = this;
            reset();
        }

        internal static void reset() {
            name = new Collection<string>();
            biomeRegistery = new Dictionary<string, bool>();
            BiomeList = new Dictionary<string, BiomeSkeleton>();
            hallowAltList = new Collection<string>();
        }

	    public static void RegisterNewBiome(String biomeName, int minTileRequired, Mod mod)
	    {
	        biomeRegistery.Remove(biomeName);
	        BiomeList.Remove(biomeName);
            name.Add(biomeName);
	        biomeRegistery.Add(biomeName, false);
	        BiomeList.Add(biomeName, new BiomeSkeleton(minTileRequired, mod));
	    }

	    public static void addHallowAltBiome(String biomeName, string message = null)
	    {
	        if (name.Contains(biomeName))
	        {
                hallowAltList.Add(biomeName);
                if (message != null) {
                    BiomeList[biomeName].setMessage(message);
                }
	        }
	    }

	    public static bool InBiome(String biomeName)
	    {
	        return BiomeList.ContainsKey(biomeName) && BiomeList[biomeName].isValid() && Main.player[Main.myPlayer].active;
        }

	    public static void AddBlockInBiome(String biomeName, String[] blockName)
	    {
	        if (world == null)
	        {
	            world = instance.GetModWorld<BiomeWorld>();
	        }
	        world.addBlock(biomeName, blockName);
	    }

        public static void AddBlockInBiomeByID(String biomeName, int[] blockID)
        {
            if (world == null)
            {
                world = instance.GetModWorld<BiomeWorld>();
            }
            world.addBlock(biomeName, blockID);
        }

        public static void setBlockMin(String biomeName, int limit) {
            if (name.Contains(biomeName))
            {
                BiomeList[biomeName].SetMinTile(limit);
            }
        }

        public static void SetCondition(String biomeName, Func<bool> condition)
        {
            
            if (name.Contains(biomeName))
            {
                BiomeList[biomeName].SetCondition(condition);
            }
        }
    }
}
