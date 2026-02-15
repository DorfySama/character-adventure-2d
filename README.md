🎮 Character Adventure 2D
📌 About The Project

Character Adventure 2D is a 2D action game developed in Unity 6.3 as a personal portfolio project.

The purpose of this project is to demonstrate my gameplay programming skills, system architecture understanding, enemy AI implementation, and save/load mechanics.

This project focuses primarily on code quality and system logic rather than art production.

🕹 Gameplay Overview

Play as a frog warrior equipped with a sword

Two combat attacks:

⚔️ Upward attack

⚔️ Downward attack

Skeleton enemies with dynamic behavior:

Patrol movement

Acceleration when player gets closer

Pathfinding using NavMesh

Game Over system

Main Menu (Play / Exit)

🧠 Enemy AI

Enemies:

Move across the map

Detect player proximity

Increase movement speed when close

Use NavMesh pathfinding to reach the player

Deal damage on contact

This demonstrates practical AI state logic and navigation setup.

💾 Save System

The project includes a working save/load system:

Automatic save when:

Exiting the game

Returning to Main Menu

Save data stored using JSON serialization

Save file automatically deleted on Game Over

This shows player state persistence and file handling logic.

🛠 Technical Implementation

Developed using:

Unity 6.3

SceneManager

JSON serialization

ScriptableObjects

New Input System

NavMesh pathfinding

Modern Unity UI system

Custom GameManager architecture

Project structure includes:

Main Menu Scene

Game Scene

Game Over Scene

Central GameManager

Player Save Data handling

📦 Assets & Credits

All visual sprites and graphical assets used in this project were downloaded from external free asset sources.

My responsibility in this project includes:

Full gameplay programming

Combat logic

Enemy AI behavior

Save/Load system

Scene management

UI logic and flow

Overall architecture

AI tools were occasionally used for learning assistance and problem-solving, but all systems were implemented, debugged, and fully understood by me.

🎯 Project Purpose

This project is part of my developer portfolio and demonstrates:

Game architecture understanding

System design

Scene management

Player state persistence

AI behavior logic

Clean and structured gameplay programming
