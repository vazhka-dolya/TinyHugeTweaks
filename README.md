[Прочитать эту страницу по-русски](https://github.com/vazhka-dolya/TinyHugeTweaks/blob/main/README.ru.md) | **Read this page in English**
# Tiny-Huge Tweaks
This is an add-on for [Mario 64 Movie Maker 3](https://github.com/projectcomet64/M64MM) that offers numerous tweaks, which can save time and slightly improve the quality of your SM64 machinimas.
<p align="center">
  <img src="https://github.com/vazhka-dolya/TinyHugeTweaks/blob/main/GitHubImg/ReadmeImage2_eng.png" width="666"/>
</p>

# Installing and using
1. Make sure you have the [latest version](https://github.com/projectcomet64/M64MM/releases/latest) of M64MM3 installed.
2. Download the [latest version](https://github.com/vazhka-dolya/TinyHugeTweaks/releases/latest) of the add-on. It will be in a `.zip` archive.
3. Extract the downloaded archive's contents[^1] into the root folder[^2] of M64MM3. If it prompts you to replace files, then do it.
4. That's all.
# Currently available functionality
<details>
  <summary>Click here to view</summary>

## Fix Cam Freeze
Implements [sm64rise's and integerbang's GameShark code](https://www.youtube.com/watch?v=FBRHespARdY) that fixes the camera being zoomed out. Intended to be a shortcut so that you don't have to reenter the code every time you use a new ROM.
## No Head Rotations
Replaces the normal standing animations with the Reading/C-Up animation, which does not have the potentially unwanted head rotations. Can be toggled. Intended as a shortcut.
## No Black Bars
Implements a SM64 ROM Manager tweak that removes the black bars that can be seen in SM64 and a lot of ROM Hacks.

Marked as Work-In-Progress since I'm not completely sure if it will work everywhere with the way it's currently implemented. It did work for me perfectly fine any time I tried to use it.
## No Shadow (Simple)
Implements a simple GameShark code that removes Mario's shadow. Most likely won't work on custom models. Intended as a shortcut.
## Fix Smoke Texture
Implements a SM64 ROM Manager tweak that fixes the smoke texture being mistakingly set to be RGBA16[^3] instead of IA16[^4].
## Advanced Texture Remover
Straight-up erases textures, turning them into blank, completely transparent images. Right now supports removing shadows, dust, sparkles, bubbles, water effects, and water splashes.
## Fix Black Textures
Implements [SM64 Save State Fixer](https://github.com/vazhka-dolya/sm64_save_state_fixer), which fixes the textures being black in older ROM hacks when using newer graphics plugins like GLideN64.
## Stars' Appearance
Allows you to change the stars' models from collected to uncollected and vice versa. You need to pause the game before using it, otherwise it's likely to crash SM64.
### Model Addresses
Since ROM hacks often have different RAM addresses for storing these models, Tiny-Huge Tweaks allows you to add your own addresses for the star models in different ROM hacks (see `TinyHugeTweaks/starAddresses.config`). You can find these addresses by using a tool like [STROOP](https://github.com/SM64-TAS-ABC/STROOP) (I recommend using Mupen64 with that).
## Show/Hide Body Parts
Implements a GameShark code that makes Mario's body invisible, but in a way that allows you to toggle it for each body part.

Marked as Work-In-Progress, because it doesn't support other body states (open hands, Wing Cap's wings, Metal Mario etc.), custom models, and LOD Mario models right now.

</details>

# Building prerequisites
<details>
  <summary>Click here to view</summary>
  
- Visual Studio 2022.
- M64MM3's repository in a folder called `M64MM` outside of where this repository is.
  - Example: if the `.sln` for BodyStates is in `C:/projects/TinyHugeTweaks/TinyHugeTweaks.sln`, the whole M64MM3 repository must be in `C:/projects/M64MM`.
- If you're on Windows, then, before extracting the archives, make sure to right-click the archive, open **Properties** and see if you have an **Unblock** checkbox. If you do, tick it and press **Apply**. If you don't do this and the archive(s) remain blocked, you may run into issues.
- *Depending on the circumstances*, you *may* have to do the following: go to **Menu** > **Tools** > **NuGet Package Manager** > **Package Manager Console** and enter `Install-Package HtmlRenderer.WinForms`. After that, go to **Menu** > **Project** > **Manage NuGet Packages…**, and make sure that both `HtmlRenderer.Core` and `HtmlRenderer.WinForms` are up-to-date.

</details>

# Credits
- GlitchyPSI ([his website](https://glitchypsi.xyz)) — a lot of help with how to make an add-on.
  - This add-on is also based on [XStudio MiNi](https://github.com/projectcomet64/xstudio-mini), which is made by him.
- ToxicBalloonKid ([YouTube](https://www.youtube.com/channel/UCbHbB9MXZYw4WgCeVXbic_Q)) — icon for the add-on.
- LiquoricePie ([Bluesky](https://bsky.app/profile/liquoricepie.bsky.social)) — alternative add-on icon (hold Shift in the About window).
### “If I use Tiny-Huge Tweaks for my work, do I have to credit you?”
Credit is highly appreciated, but completely optional!
[^1]: That means *all* the contents, including the `deps` folder. If it crashes when opening the About window, make sure that you have `HtmlRenderer.dll` and `HtmlRenderer.WinForms.dll` in M64MM's `deps` folder.
[^2]: That's the same folder where `M64MM.exe` is located.
[^3]: RGBA16 is a texture format where each pixel takes up sixteen bits (two bytes): one bit is used for the transparency (you can either have it opaque or fully transparent), while red, green, and blue use five bits each.
[^4]: IA16 is a texture format where each pixel takes up sixteen bits (two bytes): the first eight bits are used for grayscale intensity, and the other eight are for transparency (allowing partial transparency, unlike RGBA16).
