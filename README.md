# EnhancedEffectRemover

A MelonLoader mod for A Dance of Fire and Ice (ADOFAI) that strips level effects on load.
Continued with the permission of the original author, [WsbiMango](https://github.com/WsbiMango).

## Hotkey

- `Alt` + `'` toggles the settings window (rebindable from the window itself).

## Build

- .NET SDK, `netstandard2.1`
- Copy `Directory.Build.example.props` to `Directory.Build.props` and set your game path (`GamePath` / `GameData`).

```bash
cp Directory.Build.example.props Directory.Build.props
# edit GamePath/GameData, then
dotnet build -c Release
# output is copied to $(GamePath)/Mods automatically
dotnet test
```

## License

GNU General Public License v3.0 — see [`LICENCE.md`](./LICENCE.md).

## Library

- [O5Kit](https://github.com/modlist-org/O5Kit) (LGPLv3)
