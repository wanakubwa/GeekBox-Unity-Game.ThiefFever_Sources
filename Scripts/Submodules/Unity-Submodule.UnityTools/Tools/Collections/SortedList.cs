using System;
using System.Collections.Generic;
using UnityEngine;

namespace GeekBox.Collections
{
    [Serializable]
    public class SortedList<TValue, TKeyType> where TValue : IComparable<TKeyType>, IEquatable<TKeyType>
    {
        #region Fields

        [SerializeField]
        private List<TValue> collection = new List<TValue>();

        #endregion

        #region Propeties

        public List<TValue> Collection { get => collection; }

        #endregion

        #region Methods

        public void Clear()
        {
            Collection.Clear();
        }

        public void Remove(TValue item)
        {
            Collection.Remove(item);
        }

        /// <summary>
        /// Dodawanie elementu w kolejnosci rosnacej.
        /// </summary>
        public void Add(TValue item, TKeyType key)
        {
            if (Collection.Count == 0)
            {
                Collection.Add(item);
                return;
            }

            // First edge case when searched element is greater that last element in collection.
            if (Collection[Collection.Count - 1].CompareTo(key) <= 0)
            {
                Collection.Add(item);
                return;
            }

            // Second edge case when searched element is smaller than smallest element in collection.
            if (Collection[0].CompareTo(key) > 0)
            {
                Collection.Insert(0, item);
                return;
            }

            // BinarySearch explain:
            // https://docs.microsoft.com/pl-pl/dotnet/api/system.array.binarysearch?view=net-5.0
            int index = Collection.BinarySearch(item);
            if (index < 0)
            {
                index = ~index;
            }
            Collection.Insert(index, item);
        }

        public TValue BinarySearch(TKeyType key)
        {
            int index = 0;
            int maxIndex = Collection.Count - 1;

            while (index <= maxIndex)
            {
                int middleIndex = index + (maxIndex - index) / 2;

                // Check if x is present at mid
                if (Collection[middleIndex].Equals(key))
                {
                    return Collection[middleIndex];
                }

                // If x greater, ignore left half
                if (Collection[middleIndex].CompareTo(key) < 0)
                {
                    index = middleIndex + 1;
                }
                else // If x is smaller, ignore right half
                {
                    maxIndex = middleIndex - 1;
                }
            }

            Debug.LogFormat("Can't find element for key: {0}", key);
            return default;
        }

        #endregion

        #region Enums



        #endregion
    }
}
