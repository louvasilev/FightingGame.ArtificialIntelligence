
using AI.Support.Abstractions;
using System.Collections.Generic;

namespace AI.Support
{
    /// Copyright © 2016 Lubomir Vasilev - All rights reserved.
    ///
    /// <summary>
    /// Provides methods and properties for searching through actions.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 06-04-2016
    /// </date>
    public static class ActionSupport
    {

        #region Public methods

        /// <summary>
        /// Calculates how many turns have elapsed since a given
        /// action was last used.
        /// </summary>
        /// <returns>The number of turns since the last time the given
        /// action was used by the character.</returns>
        /// <param name="action">The action in question.</param>
        /// <param name="actionsHistory">A list of actions to search for the given
        /// action through.</param> 
        public static int TurnsSinceLastUse(AIAction action, List<TurnAction> actionsHistory)
        {
            if (action == null)
            {
                throw new System.ArgumentException("Action reference not found.");
            }

            if (actionsHistory == null)
            {
                throw new System.ArgumentException("List<TurnAction> reference not found.");
            }

            int previousTurn = 0;
            int turnsSinceLastUse = 0;
            bool actionFound = false;

            // Iterate through the actions history in reverse
            // order and count the turns, if any, until the given
            // action is found.
            int actionCount = actionsHistory.Count;
            for (int i = actionCount - 1; i >= 0; i--)
            {
                int currentTurn = actionsHistory[i].turn;

                if (previousTurn == 0 || currentTurn != previousTurn)
                {
                    turnsSinceLastUse++;
                }

                if (action.IsIdenticalTo(actionsHistory[i].action))
                {
                    actionFound = true;
                    // If an action equal to the one we are looking for
                    // is found, exit the loop. Since we are only interested
                    // in the last time the action was used, there is no
                    // need to keep looking.
                    break;
                }

                previousTurn = currentTurn;
            } // End for

            if (!actionFound)
            {
                turnsSinceLastUse = 0;
            }

            return turnsSinceLastUse;
        } // End TurnsSinceLastUse

        #endregion

    }
}