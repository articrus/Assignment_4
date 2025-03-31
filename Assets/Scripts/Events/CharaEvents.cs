using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 08-12-2024
 * This script is used for characterevents, for displaying damage and tutorials
 */
namespace Assets.Scripts.Events
{
    internal class CharaEvents
    {
        //Character and amount of damage taken/healed
        public static UnityAction<GameObject, int> charaDamaged;
        public static UnityAction<GameObject, int> charaHealed;
        public static UnityAction<GameObject, int> passedTutorial;
    }
}