using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GanzAdmin.Utils
{
    /// <summary>
    /// Agnosztikus módon kezel egy ideiglenes objektumot az eredeti minden értékével, amibe a változtatások után vissza lehet tölteni az ideiglenes objektum értékeit.
    /// </summary>
    /// <typeparam name="T">A használt objektum típusa.</typeparam>
    public class TemporalObject<T> where T : class, new()
    {
        private interface IGetSet
        {
            public Type ValueType { get; }
            public bool Init(PropertyInfo info);
        }

        private class GetSet<K> : IGetSet
        {
            public Func<T, K> Getter { get; private set; }
            public Action<T, K> Setter { get; private set; }

            public Type ValueType { get; }

            public GetSet()
            {
                this.ValueType = typeof(K);
            }

            public GetSet(PropertyInfo info): this()
            {
                this.Init(info);
            }

            public bool Init(PropertyInfo info)
            {
                bool valid = false;

                MethodInfo setMethod = info.GetSetMethod();
                MethodInfo getMethod = info.GetGetMethod();

                if(setMethod != null && getMethod != null)
                {
                    this.Setter = (Action<T, K>)Delegate.CreateDelegate(typeof(Action<T, K>), null, setMethod);
                    this.Getter = (Func<T, K>)Delegate.CreateDelegate(typeof(Func<T, K>), null, getMethod);
                    valid = true;
                }

                return valid;
            }
        }

        private static Dictionary<string, IGetSet> PropertyData { get; } = new Dictionary<string, IGetSet>();
        private static bool Mapped = false;

        /// <summary>
        /// Az eredeti objektum.
        /// </summary>
        public T Original { get; }

        /// <summary>
        /// A lemásolt ideilenes objektum.
        /// </summary>
        public T Temporal { get; }

        /// <summary>
        /// Létrehoz egy új ideiglenes objektumot.
        /// </summary>
        /// <param name="obj">Az eredeti objektum.</param>
        public TemporalObject(T obj)
        {
            this.Original = obj;
            this.Temporal = new T();

            if(!Mapped)
            {
                CreateMap();
            }

            foreach(var prop in PropertyData)
            {
                dynamic getSet = prop.Value;

                dynamic value = getSet.Getter(this.Original);
                getSet.Setter(this.Temporal, value);
            }
        }

        /// <summary>
        /// Visszatölti az ideiglenes objektum értékeit az eredetibe.
        /// </summary>
        /// <returns>A módosított eredeti objektum.</returns>
        public T FoldBack()
        {
            foreach (var prop in PropertyData)
            {
                dynamic getSet = prop.Value;

                dynamic value = getSet.Getter(this.Temporal);
                getSet.Setter(this.Original, value);
            }

            return this.Original;
        }

        private static void CreateMap()
        {
            PropertyInfo[] infos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty);
            foreach(PropertyInfo info in infos)
            {
                Type dynamicType = typeof(GetSet<>).MakeGenericType(typeof(T), info.PropertyType);
                dynamic getSet = Activator.CreateInstance(dynamicType);

                bool valid = (getSet as IGetSet).Init(info);
                if (valid)
                {
                    PropertyData.Add(info.Name, getSet);
                }
            }

            Mapped = true;
        }
    }
}
