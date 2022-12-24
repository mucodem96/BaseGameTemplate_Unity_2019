﻿using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Scripts.BaseGameScripts.Helper
{
    public class AssemblyManager
    {
        public static List<Type> GetSubClassesOfType(Type parentClass)
        {
            List<Type> listOfTypes = new List<Type>();
            
            Type[] allTypes = Assembly.GetAssembly(parentClass).GetTypes();
            Type typeOfBaseClass = Type.GetType(parentClass.ToString());

            for (int i = 0; i < allTypes.Length; i++)
            {
                Type currentType = allTypes[i];

                if (currentType.IsSubclassOf(typeOfBaseClass ?? throw new InvalidOperationException()))
                {
                    listOfTypes.Add(currentType);
                }

                if (i == allTypes.Length - 1)
                {
                    return listOfTypes;
                }
            }

            Debug.LogError("THERE IS NO SUBCLASS OF " +  parentClass.Name + " TYPE");
            return null;
        }
    }
}