/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Reflection;

namespace PaintDotNet.Effects
{
    public sealed class EffectsCollection
    {
        private Assembly[] assemblies;
        private List<Type> effects;

        public EffectsCollection(List<Assembly> assemblies)
        {
            this.assemblies = assemblies.ToArray();
            this.effects = null;
        }

        public EffectsCollection(List<Type> effects)
        {
            this.assemblies = null;
            this.effects = new List<Type>(effects);
        }

        public Type[] Effects
        {
            get
            {
                if (this.effects == null)
                {
                    this.effects = GetEffectsFromAssemblies(this.assemblies);
                }

                return this.effects.ToArray();
            }
        }

        private static List<Type> GetEffectsFromAssemblies(Assembly[] assemblies)
        {
            List<Type> effects = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                GetEffectsFromAssembly(effects, assembly);
            }

            return effects;
        }

        private static void GetEffectsFromAssembly(List<Type> effectsAccumulator, Assembly assembly)
        {
            try
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(Effect)) && !type.IsAbstract)
                    {
                        effectsAccumulator.Add(type);
                    }
                }
            }

            catch (ReflectionTypeLoadException)
            {
            }
        }
    }
}
