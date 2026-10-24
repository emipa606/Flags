## GitHub Copilot Instructions for Flags (Continued) Mod

### Mod Overview and Purpose
The Flags (Continued) mod is an update to the cuproPandas mod, which adds decorative flags for marking areas or displaying heraldry in the game RimWorld. These flags are animated and their animation speed is influenced by the current wind speed. Flags can also be enjoyed as a joy activity.

### Key Features and Systems
1. **Flags**: 14 dyed flags, 1 undyed flag which takes the color of the material it's made from, 6 undyed logo flags, and 1 dyed logo flag (Pirate).
2. **Animation**: Flags are animated, and their speed changes with the wind speed.
3. **Joy Activity**: Flags can be viewed as a joy activity for colonists.
4. **Save Compatibility**: The mod can be added to existing saves. Always back up your saves before adding new mods.
5. **Mod Compatibility**: No known issues yet, but any issues should be reported.

### Coding Patterns and Conventions
1. **C# Code**: The mod uses C# for defining the behavior of the flags.
   - **Building_Flag.cs**: Contains the `Building_Flag` class, which handles the behavior of the flags.
     - **Color**: Represents the color of the flag.
     - **DrawAt**: Handles the rendering of the flag.
     - **getGraphicArray**: Retrieves the graphic array for the flag.
     - **Graphic**: Represents the graphic of the flag.
     - **SpawnSetup**: Sets up the flag when it is spawned.
     - **Tick**: Updates the flag every game tick.
2. **XML Integration**: The mod uses XML for defining translations and other game data.
   - **DesignationCategories.xml**: Contains definitions for designation categories.
   - **Jobs_Joy.xml**: Contains definitions for jobs related to joy activities.
   - **ThingDefs.xml**: Contains definitions for things, including the flags.

### Harmony Patching
The mod uses Harmony patching to integrate with the game's existing systems. Harmony is a popular library for modding RimWorld that allows for the modification of existing game code.

### Suggestions for Copilot
1. **C# Code**: When generating code, suggest using the existing `Building_Flag` class as a template and focus on implementing any new features or behaviors.
2. **XML Integration**: When generating XML code, suggest using the existing XML files as templates and focus on adding new definitions for flags or other game elements.
3. **Harmony Patching**: When generating Harmony patches, suggest using the existing patches in the mod as templates and focus on implementing any new patches needed to integrate with the game's existing systems.

### Example Code Snippets
1. **C# Code**:
csharp
using Verse;

namespace CuproFlags
{
    public class Building_Flag : Building
    {
        public Color color;

        public override void DrawAt(Vector3 loc, Rot4 rot, ThingDef def = null)
        {
            // Custom drawing logic for the flag
        }

        public Graphic[] getGraphicArray()
        {
            // Custom logic for retrieving the graphic array
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            // Custom setup logic for the flag
        }

        public override void Tick()
        {
            base.Tick();
            // Custom logic for updating the flag
        }
    }
}


2. **XML Code**:
xml
<ThingDef ParentName="Base_Furniture">
  <defName>Flag_Pirate</defName>
  <label>Flag (Pirate)</label>
  <description>A large pirate flag for display.</description>
  <thingClass>Building_Flag</thingClass>
  <graphicData>
    <texPath>Things/Building/Flags/Pirate</texPath>
    <shaderType>Base</shaderType>
  </graphicData>
  <costList>
    <Steel>1</Steel>
    <Silicone>1</Silicone>
  </costList>
  <statBases>
    <MarketValue>2.5</MarketValue>
  </statBases>
</ThingDef>


3. **Harmony Patch**:
csharp
using HarmonyLib;
using Verse;

namespace CuproFlags
{
    [HarmonyPatch(typeof(WeatherManager), "WindSpeed")]
    public static class WeatherManager_WindSpeed_Patch
    {
        public static void Postfix(ref float __result)
        {
            // Custom logic for modifying the wind speed
        }
    }
}


By following these guidelines, Copilot can generate code that is consistent with the mod's existing patterns and conventions, and integrate seamlessly with the game's systems.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

