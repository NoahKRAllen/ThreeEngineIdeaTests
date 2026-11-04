# ThreeEngineIdeaTests
Game ideas/concepts built across Unity, Unreal, and Godot to test which works better for me.

Total Systems:
-Player click-to-move
-Camera follow in top-down perspective
-Wave of Units spawning
-Units tracking and following player
-Attacking from both player and units
-Removal of units/player on 0 health
-End game, either win or lose


Breakdown:

-Player Click-to-move:
--Simplest version:
---Right click to move character
--Later version:
--Left Click Interaction
---Used for selecting any unit in game
---Unit has to be selected for player to attempt control
--Right Click move
---Only activates when player is selecting an unit that player controls
---Only works on units that can move

-Camera follow
--Simplest version:
---Follows player character as it moves, ignoring player character rotation
--Later version:
---Togglable option to follow selected unit, if player owned

-Waves of units spawning:
--Simplest version:
---Constant spawning of units from edge of map
--Later version:
---Set spawnpoints that can be cycled between for spawns
---Different units spawning based on wave data

-Unit Tracking:
--Simplest version:
---Upon spawn, target player and move towards using navmesh or equivalent
--Later version:
---Checking for different player units/other targetable units, targeting closest
---Potentially a max range of searching based on unit vision

-Attacking:
--Simplest version:
---All units have base attack range
---Once in range, start dealing damage to target, limited by attack speed
--Laer version:
---Animations and protectiles instead of just instant damage dealing
---Potentially avoidable AOE attacks for player and enemy Units

-Unit death:
--Simplest version:
---Unit disappears once hitting 0 health
---Turns off all actions from that unit, and remove it from any tracking
--Later version:
---Animations for unit death
---Tracking unit deaths and granting rewards

-End game:
--Simplest version:
---Timer for player to survive
---If player is alive at end of timer, they win, otherwise they lose
--Later version:
---Waves limited, win only comes at end of all spawned Units
---Much later different 'missions' that have different paths of victory



Unity Progress:
-Camera Follow, using basic smooth follow script
-Player Movement, right click to move around on a navmesh
-Enemy Movement, chasing player using same navmesh
-Enemy targeting
-Player targeting
