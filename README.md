# FightingGame.ArtificialIntelligence
The Fighting Game Artificial Intelligence (AI) system consists of two parts, a Finite State Machine (FSM)
 and a Utility Based (UB) system. The FSM is responsible for high-level strategic choices, while the UB
 system makes the more specific, tactical choices.


# Instructions

To view the code, download or clone the repository and open the solution file FightingGame.ArtificialIntelligence.sln in Visual Studio.

# Description

Concepts:

* **TacticalMind** - The main class in the Utility-Based portion of the AI system. A high-level object making decisions about what actions the AI controlled character should take, based on a number of factors and appraisals (evaluation of possible actions). The Tactical Mind has a list of Actions.

* **Action** - are all the possible things the AI controlled character can do at any given point in the game.  AN ACtion can be a state, a step in a plan, etc. Each Action has a list of Factors. Usually an Action is a fighting technique, but it can also be other things the character could do during a fight, such as move forward or backward, jump, crouch, taunt the opponent, meditate, heal himself, etc (currently, actions are limited to fighting techniques).

* **Factor** - an atomic piece of logic. It needs to be easily added and removed. Factors evaluate one aspect of the game situation. They are parameterized for the particular character and decision involved in the situation.
