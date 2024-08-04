# Game_Learning

Project Brief:
Gamified learning can be used to introduce or reinforce teaching material.  This can take the form of puzzles or a game with many levels, during which a player is rewarded for understanding the concepts that are included.

A game should be constructed that teaches a participant ideas or concepts.  The participant could be a school pupil, student or adult.  The game should be written using a game framework, rather than a gamified learning platform.  For example, the game could be constructed using Pygame (python), Godot (GDScript, C#, C++), jMonkeyEngine (Java) or Unity (C#, C++).

Overview:
Gamified learning has been proven by research to have benefits at primary level education, due to interactivity and score-based applications. When looking to applications geared towards more mature audiences, it falls under simplified templates of interactive quizzes which rely on memorisation and daily reinforcement to have an effect. This project attempts to apply learning techniques to a 2D platformer so as to fill that niche. 

After interviewing participants about their education and game habits, these were refined into user requirements and user stories with informed the development of the software. This included an emphasis on game design for immersion and interactiviy to weave the learning more organcally within the core mechanics of the software.

This project works as a minimum viable product with room for variance of game objects due to the flexiblity of the components.

-- Folder Structure --

Assets --
This folder contains all of the assets that make up the project. There are seperate folders wihtin to contain and organise specific assets such as:
Art - Contains all the sprites used.
Dialogue - Contains all the Dialogue related JSON files for use in the project and the INK editor.
Ink - All the files related to the INK script editor and plugin for unity.
Presets - The container for the pixel font used in the project.
Resources - Holds all prefabricated game objects along with specific componenets - Structured this way for resource loading in unit tests.
Scenes - Contains the game scenes or levels.
Scripts - Holds all the application programming code files.
Tests - Contains the tests for play mode and edit mode, along with their assembly definitions.
TextMesh Pro - All the files related to text used within the UI areas.
UI - Contains the assets used in the UI game objects such as Audio.

++ Scripts ++

Combat --
The combat folder contains the scripts related to specific combat mechanics which are referenced within
the application.

Scripts:
Attack - Deals with damage and knockback of attacked game objects
Detection Zones - Handles detecting specific game objects within colliders
Projectiles - Consists of all behaviours relating to projectiles: damage, knockback. life and amount of hits.
Projectile Spawners - Set the location projectiles will spawn from.

Dialogue --
The dialogue folder contains the scripts related to dialogue and interaction mechanics which are referenced within
the application.

Scripts:
DialogueManager - Enables dialogue through UI elements and INK plugin. Allows for choices within dialogue and advances it forward.
DialogueTrigger - Handles direct interaction with NPC's and shows a visual cue when an NPC can be interacted with. Set dialogue JSON files for specific NPCs

Enemy --
The Enemies folder contains the scripts related to enemy NPC's which are referenced within
the application.

Scripts:
Skeleton - Contains behaviour related to ground type enemys such as the skeleton game object. Includes movement and animation states.
FlyingDemon - Handles the behaviour concerning flying enemy types such as the flying demon game object. Includes movement through an array of waypoint locations and animation state triggers.

Events -- 
Contains the game events script that handles event triggers related to player health changes within the application

Movement -- 
The Movement folder contains the scripts related to player and NPC movement which are referenced within
the application.

Scripts:
PlayerController - Contains code that is set up to interpret player input through the unity input manager. Sets up the standard movement capabilites within a 2D platformer and allows for setting animation and state conditions or triggers.
TouchingDirections - Handles game objects facing direction and collsions with obstacles. For the player this can create smoother gameplay through movement contraints. NPC Game Objects require this script to be able to turn and move consistently.

Pickups --
The Pickups folder contains the scripts related to health and knowledge pickups which are referenced within
the application.

Scripts:
Health Pickup - All the behaviour set for the health game object such as amount healed, its contstraints on pickup and its minimal animation.
Knowledge Pickup - Holds the scripts repalted to the game object such as interaction, dialogue and animations.

The State Machine folder contains the scripts related to setting constraints during animation states which are referenced within
the application.

State Machine --
Scripts:
FadeRemove - Fades out and destroys the game object after reaching the death state.
SetBoolBehaviour - Allows for setting boolean values related to animation states, such as contstraining movements during attacking
SetFloatBehaviour - Allows setting particular float values when entering/exiting an animation state, such as setting player movement speed to 0 after being hit.
PlaySingleUseBehaviour - Allows a single use componenet to be generated during specific states and destroyed after completion, used primarily for creating sfx audio for actions.

UI --
The UI folder contains the scripts related to User Interface componenets which are referenced within
the application.

Scripts:
HealthBar - Creates and manages the health bar UI item created in Unity. Manages the health bar to reflect the player current health at all times.
HealthText - Creates instances of text that showcase health lost or restored during gameplay.

World--
The World folder contains the scripts related to componenets that operate with the game world which are referenced within
the application.

Scripts:
MovingPlatform - Handles behaviour for platforms within the game world, such as the activation, speed and array of waypoints they move between.
MusicPlayer - Contains the script foe managing the setting and transtioning between music tracks for the game world.
ParralaxEffects - Scripts the background to have an illusion of 3D movement using parallax scrolling.