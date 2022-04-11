# Portfolio-Platformer

## Controls:
Move: WASD

Attack: Left click

A platformer based around killing enemies to collect score.

## Strengths

* Extensible code: The game is written to be very easy to extend. The player interacts with an abstract enemy class that all enemies inherit from and recieves damage from a generic Hazard script. This means new enemy types and environmental hazards can be added very easily and the player will immediately be able to interact with them.

* Minimal duplication of code: The code is written to avoid as much duplication of code as possible. For example, the player and all enemies inherit from an abstract Actor class that contains the take damage, knockback and die functions. This makes actor behaviour consistent, makes changes to the code easier and reduces the chances of errors hidden in redundant code.

* Readable code: The code is written to be clear and easy to understand. The variable and function names are clear and self-documenting and the code has annotation to make the purpose of various aspects of the code clear.

* Good game feel. The player uses a momentum-based movement system that is responsive but gives a slight feeling of weight. Jumps are made to feel more weighty by having gravity act slightly stronger when the player is already moving down. The player is able to press space slightly before landing or press space slightly after leaving a platform and still be able to jump as players often feel these actions were done at the right time regardless of if that's actually true. The player is able to hold space for a higher jump. Knockback on attacks give an amount of weight behind attacks, even without animations.


## Areas for future development

* The game is currently completely lacking in art and animation.

* More enemy variety. Could include different movement and speed, different attacks and various unique effects.

* Environmental hazards such as spikes.

* Pickups that enhance the player. Simple stuff such as heals or movement buffs would be easy and the code is written in such a way that weapons with different stats are viable to add as well.

* Larger and more interesting level.
