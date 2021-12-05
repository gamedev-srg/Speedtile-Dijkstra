# This is an implentation of the Dijkastra algoriithm and speed tile.


*BUG FIXED - player now finds the shortest path (instead of longest ;) )

to play the game just point and click the screen !
please note that the deep ocean and the mountains are not walkable so dont try them.

each tile has a speed of his own, as can be seen in the source code.
the algorithm then find th e shortest path( the path which has the lowest movment speed time)
and points the charecter to that direction.


please go and play our game on Itch.io !

### The files I changed:
* [Djikstra](https://github.com/gamedev-srg/Speedtile-Dijkstra/blob/master/Assets/Scripts/0-bfs/BFS.cs) Despite the name of the file, we implemented several interfaces and allowed the algorithm to work on a weighted graph,including a complete implmentation Priority queues, and more. 
* [Target Mover](https://github.com/gamedev-srg/Speedtile-Dijkstra/blob/master/Assets/Scripts/2-player/TargetMover.cs) Added different movment speed for different tiles

https://g-r-s.itch.io/speed-tile-dijkastra
