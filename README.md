# <div align="center">EnhancedEffectRemover</div>

<div align="center">
    <img src="eer_logo.png" height="256" alt="EnhancedEffectRemover Logo">
</div>

<div align="center">

### ⟡ Lightly, Feather ⟡

</div>

<div align="center">

A MelonLoader mod for A Dance of Fire and Ice that strips level effects on load.
Continued with the permission of the original author, [WsbiMango](https://github.com/WsbiMango).

</div>

# 🛠️ Installation

## 🍉 MelonLoader

> [!IMPORTANT]
> You must extract the contents of the zip file **directly into your game's root directory** (where the game `.exe` lives). Ensure the internal folders line up perfectly with your existing game directory structure.

1. Download the `EnhancedEffectRemover_ML.zip` from the [releases page](https://github.com/KkitutLab/EnhancedEffectRemover/releases).
2. Open your game's installation root folder.
3. Extract all contents of the downloaded zip file directly into that folder.

# ⌨️ Usage

- Press **`Alt` + `'`** to toggle the settings window (rebindable from the window itself).

# 🛠️ Development

You need the .NET SDK. Copy `Directory.Build.example.props` to `Directory.Build.props` and set `GamePath` + `GameData`.

```bash
dotnet build -c Release   # output is copied to $(GamePath)/Mods automatically
dotnet test
```

# ⚖️ Licenses

This project is licensed under [GPLv3](./LICENCE.md).

### Library

- [O5Kit](https://github.com/modlist-org/O5Kit) (LGPLv3)
