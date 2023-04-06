# Modding Playground: Minesweeper

![Unity 2022.2.11](https://img.shields.io/badge/Unity-2022.2.11-blue) [![Itch Io](https://img.shields.io/badge/-Itch%20IO-red)](https://theashenwolf.itch.io/modding-playground-minesweeper)

This project was created as a practice playground for Harmony Patch. It is a simple Minesweeper clone. 
Why should you pick this implementation instead of any other?

- Game contains a Release build, but is open source, so you can check the original source, in case you get lost
- It is made with modding in mind, meaning it contains no `private` or `protected` methods / variables, so you can easily patch them
- All buttons are bound from code, so you can freely edit and clone them without issues
- The code is simple

## How to use

- Download the game from the releases, or, if you don't believe the exe, clone the repository and build it yourself
- [Optional] Play the game a bit and try to understand its core mechanics and concepts
- Decompile the game with dotPeek, dnSpy, ILSpy or any other decompiler of your choice
- Mod away!

## Tasks

In case you struggle with ideas what to mod, here are some ideas:
- Add a "Custom" game mode, where you can import your own numbers for width, height and the bomb count
- Add a detailed display of ones victory
- Add a button which will lead back to the main menu
- As custom game mode is still limited by the camera, add zoom functionality and panning
