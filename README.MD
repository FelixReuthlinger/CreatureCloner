# Creature Cloner

This is a Valheim mod to clone and configure creature objects, like the Greydwarfs.

For details, see the mod [README](CreatureCloner).

## Building requires

BepInEx installation at your vanilla Valheim folder, the build will publish the dll to deploy folder automatically.

Add file ```Environment.props``` at root folder with content (modify path if used different install path):
```
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
    <PropertyGroup>
        <!-- Needs to be your path to the base Valheim folder -->
        <VALHEIM_INSTALL>C:\Program Files (x86)\Steam\steamapps\common\Valheim</VALHEIM_INSTALL>
        <!-- This is the folder where your build gets copied to when using the post-build automations -->
        <MOD_DEPLOYPATH>C:\Users\felix\AppData\Roaming\Thunderstore Mod Manager\DataFolder\Valheim\profiles\...\BepInEx\plugins</MOD_DEPLOYPATH>
    </PropertyGroup>
</Project>
```
