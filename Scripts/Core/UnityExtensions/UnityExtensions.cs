using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class UnityExtensions
{

    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
            action(item);
    }

    /// <summary>
    /// Przykład formatowania: "mm:ss:fff" - minuty:Sekundy:mili s.
    /// </summary>
    public static string ToTimeFormatt(this float value, string format)
    {
        return DateTime.FromBinary(599266080000000000).AddMilliseconds(value).ToString(format);
    } 

    public static List<T> Clone<T>(this List<T> collection) where T : IObjectCloneable<T>
    {
        List<T> output = new List<T>();

        if(collection.IsNullOrEmpty() == true)
        {
            return output;
        }

        for (int i = 0; i < collection.Count; i++)
        {
            output.Add(collection[i].Clone());
        }

        return output;
    }

    //public static string ToStringKiloFormat(this int value)
    //{
    //    return value >= 1000 ? (value / 1000f).ToString("0.#K") : value.ToString();
    //}

    public static string ToStringKiloFormat(this int num)
    {
        if (num >= 100_0000_00)
        {
            return (num / 1000000D).ToString("0.#M");
        }
        if (num >= 1_000_000)
        {
            return (num / 1000000D).ToString("0.##M");
        }
        if (num >= 100_000)
        {
            return (num / 1000D).ToString("0.#k");
        }
        if (num >= 1_0000)
        {
            return (num / 1000D).ToString("0.##k");
        }

        return num.ToString("#,0");
    }

    public static string ToShortDecimal(this float value, int minDecimal)
    {
        string text = value.ToString();

        string output = string.Empty;

        char zeroSymbol = '0';
        bool wasDivider = false;
        bool wasNotZero = false;
        int decimalCounter = 0;

        if(text.IsNullOrWhitespace() == true)
        {
            return "0.00";
        }

        char[] characters = text.ToCharArray();
        for(int i = 0; i < characters.Length; i++)
        {
            if(wasDivider == false)
            {
                if (characters[i] == ',' || characters[i] == '.')
                {
                    characters[i] = '.';
                    wasDivider = true;
                }
            }
            else
            {
                if(wasNotZero == true)
                {
                    decimalCounter++;
                }
                else
                {
                    if (characters[i] != zeroSymbol)
                    {
                        wasNotZero = true;
                        decimalCounter++;
                    }
                }
            }

            output += characters[i];
            if (decimalCounter == minDecimal)
            {
                break;
            }
        }

        return output;
    }

    public static void ClearDestroy<T>(this IList<T> objects) where T: Component {
        if(objects.IsNullOrEmpty() == true) {
            return;
        }

        for(int i=0; i<objects.Count; i++) {
            if(objects[i] != null) {
                UnityEngine.Object.Destroy(objects[i].gameObject);
            }
        }

        objects.Clear();
    }

    // source: https://answers.unity.com/questions/13840/how-to-detect-if-a-gameobject-has-been-destroyed.html
    public static bool IsDestroyed(this GameObject gameObject)
    {
        // UnityEngine overloads the == opeator for the GameObject type
        // and returns null when the object has been destroyed, but 
        // actually the object is still there but has not been cleaned up yet
        // if we test both we can determine if the object has been destroyed.
        return gameObject == null && !ReferenceEquals(gameObject, null);
    }

    public static string GetElementWithTags(this string[] data, string tag0, string tag1)
    {
        if(data.IsNullOrEmpty() == true)
        {
            return string.Empty;
        }

        for(int i=0; i<data.Length; i++)
        {
            if(data[i].Contains(tag0) == true && data[i].Contains(tag1) == true)
            {
                return data[i];
            }
        }

        return string.Empty;
    }

    public static T[] SubArray<T>(this T[] data, int index, int length)
    {
        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }

    public static string ToRGBHex(this Color c)
    {
        return string.Format("#{0:X2}{1:X2}{2:X2}", ToByte(c.r), ToByte(c.g), ToByte(c.b));
    }

    private static byte ToByte(float f)
    {
        f = Mathf.Clamp01(f);
        return (byte)(f * 255);
    }

    public static string SetColor(this string text, Color color)
    {
        string output;
        output = string.Format("<color={0}>{1}</color>", color.ToRGBHex(), text);
        return output;
    }

    // Setting first letter in string to upper case.
    public static string UppercaseFirst(this string text)
    {
        if (string.IsNullOrEmpty(text) == true)
        {
            return string.Empty;
        }

        char[] a = text.ToCharArray();
        a[0] = char.ToUpper(a[0]);

        return new string(a);
    }

    // Source: https://nickstips.wordpress.com/2010/08/28/c-extension-method-get-a-random-element-from-a-collection/
    public static T GetRandomElement<T>(this IEnumerable<T> list)
    {
        // If there are no elements in the collection, return the default value of T
        if (list.Count() == 0)
            return default(T);

        // Guids as well as the hash code for a guid will be unique and thus random        
        int hashCode = Math.Abs(Guid.NewGuid().GetHashCode());
        return list.ElementAt(hashCode % list.Count());
    }

    public static string ReplaceDolarCodes(this string text)
    {
        //todo;
        return text;
    }

    public static string FixCommaUnicode(this string text)
    {
        if (string.IsNullOrEmpty(text) == true)
        {
            return string.Empty;
        }

        return text.Replace("\\u002C", "\u002C");
    }

    public static string FixNewlineUnicode(this string text)
    {
        if (string.IsNullOrEmpty(text) == true)
        {
            return string.Empty;
        }

        return text.Replace("\\n", "\n");
    }

    public static string Truncate(this string text, int charLimit)
    {
        if(text.IsNullOrWhitespace() == true)
        {
            return text;
        }

        if(text.Length > charLimit)
        {
            text = text.Substring(0, charLimit);
        }

        return text;
    }

    //public static string Localize(this string text)
    //{
    //    if (text.IsNullOrWhitespace() == true)
    //    {
    //        return string.Empty;
    //    }

    //    text.Trim();

    //    string output = LanguageManager.Instance?.GetTextByKey(text);
    //    return output;
    //}

    public static void ResetParent(this Transform transform, Transform parent, bool isActive = true)
    {
        if(transform != null && parent != null)
        {
            transform.SetParent(parent, false);
        }

        transform.gameObject.SetActive(isActive);
    }

    // REFLECTION
    public static bool IsInheritedFrom(this Type type, Type Lookup)
    {
        var baseType = type.BaseType;
        if (baseType == null)
            return false;

        if (baseType.IsGenericType
                && baseType.GetGenericTypeDefinition() == Lookup)
            return true;

        return baseType.IsInheritedFrom(Lookup);
    }

    public static IEnumerable<Type> GetTypesWithAttribute(this Assembly assembly, Type attributeType)
    {
        List<Type> output = new List<Type>();
        foreach (Type type in assembly.GetTypes())
        {
            if (type.GetCustomAttributes(attributeType, true).Length > 0)
            {
                output.Add(type);
            }
        }

        return output;
    }

    public static int ParseToInt(this string text)
    {
        text = text.Trim();

        int output;
        if(int.TryParse(text, out output) == true)
        {
            return output;
        }

        return Constants.DEFAULT_VALUE;
    }

    public static float ParseToFloat(this string text)
    {
        text = text.Trim();
        text = text.Replace('.', ',');

        float output;
        if (float.TryParse(text, out output) == true)
        {
            return output;
        }

        return Constants.DEFAULT_VALUE;
    }

    public static string[] ToStringArray(this int[] array)
    {
        if(array.IsNullOrEmpty() == true)
        {
            return null;
        }

        string[] output = new string[array.Length];
        for(int i = 0; i < array.Length; i++)
        {
            output[i] = array[i].ToString();
        }

        return output;
    }

    public static T GetElementAtIndexSafe<T>(this ICollection<T> collection, int index)
    {
        if(index >= 0 && index < collection.Count)
        {
            return collection.ElementAt(index);
        }

        return default;
    }

    public static T PopElement<T>(this ICollection<T> collection)
    {
        if (0 < collection.Count)
        {
            T element = collection.ElementAt(0);
            collection.Remove(element);

            return element;
        }

        return default;
    }

    public static T GetElementByID<T>(this ICollection<T> collection, int id) where T : IIDEquatable
    {
        if(collection.IsNullOrEmpty() == true)
        {
            return default;
        }

        for(int i = 0; i < collection.Count; i++)
        {
            if(collection.ElementAt(i).IDEqual(id) == true)
            {
                return collection.ElementAt(i);
            }
        }

        return default;
    }

    public static bool ContainsElementByID<T>(this ICollection<T> collection, int id) where T : IIDEquatable
    {
        if (collection.IsNullOrEmpty() == true)
        {
            return false;
        }

        for (int i = 0; i < collection.Count; i++)
        {
            if (collection.ElementAt(i).IDEqual(id) == true)
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
    {
        return collection == null || collection.Count() < 1;
    }

    public static void RemoveElementByID<T>(this ICollection<T> collection, int id) where T : IIDEquatable
    {
        for (int i = 0; i < collection.Count; i++)
        {
            if (collection.ElementAt(i).IDEqual(id) == true)
            {
                collection.Remove(collection.ElementAt(i));
                return;
            }
        }
    }

    public static void RemoveElementsByID<T>(this ICollection<T> collection, int id) where T : IIDEquatable
    {
        if(collection.IsNullOrEmpty() == true)
        {
            return;
        }

        for (int i = collection.Count - 1; i >=0; i--)
        {
            if (collection.ElementAt(i).IDEqual(id) == true)
            {
                collection.Remove(collection.ElementAt(i));
            }
        }
    }

    public static void DestroyElementByID<T>(this IList<T> collection, int id) where T : MonoBehaviour, IIDEquatable
    {
        if (collection.IsNullOrEmpty() == true)
        {
            return;
        }

        for (int i = collection.Count - 1; i >= 0; i--)
        {
            if (collection[i].IDEqual(id) == true)
            {
                GameObject.Destroy(collection[i].gameObject);
                collection.RemoveAt(i);
                break;
            }
        }
    }

    public static int IndexOf(this int[] array, int value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == value)
            {
                return i;
            }
        }

        return Constants.DEFAULT_INDEX;
    }

    public static int IndexOf(this string[] array, string value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == value)
            {
                return i;
            }
        }

        return Constants.DEFAULT_INDEX;
    }

    public static void ClearDestroy<T>(this ICollection<T> collection) where T : IDestroyable
    {
        for (int i = collection.Count - 1; i >= 0; i--)
        {
            collection.ElementAt(i).Destroy();
        }

        collection.Clear();
    }

    public static void ClearClean<T>(this ICollection<T> collection) where T : ICleanable
    {
        for (int i = collection.Count -1; i >= 0; i--)
        {
            collection.ElementAt(i).CleanData();
        }

        collection.Clear();
    }

    public static void ClearClean<TKey, TValue>(this ICollection<KeyValuePair<TKey, TValue>> collection) where TValue : ICleanable
    {
        for (int i = collection.Count - 1; i >= 0; i--)
        {
            collection.ElementAt(i).Value.CleanData();
        }

        collection.Clear();
    }

    public static List<T> GetRandomElements<T>(this ICollection<T> collection, int rollAmmount)
    {
        List<T> randomElements = new List<T>();
        List<int> indexCollection = new List<int>();

        for(int i = 0; i < collection.Count; i++)
        {
            indexCollection.Add(i);
        }

        int randomIndex;
        for (int i =0; i< rollAmmount; i++)
        {
            randomIndex = indexCollection.GetRandomElement();
            if(indexCollection.Count > 0)
            {
                randomElements.Add(collection.ElementAt(randomIndex));
                indexCollection.Remove(randomIndex);
            }
            else
            {
                break;
            }
        }

        return randomElements;
    }

    /// <summary>
    /// Return element from ascending sorted collection. Using binary search collection.
    /// </summary>
    /// <typeparam name="T">Searched object type.</typeparam>
    /// <param name="collection">Sorted collection by ASCENDING.</param>
    public static T BinarySearchByID<T>(this ICollection<T> collection, int id) where T : IIDEquatable
    {
        int index = 0;
        int maxIndex = collection.Count - 1;

        while (index <= maxIndex)
        {
            int middleIndex = index + (maxIndex - index) / 2;

            // Check if x is present at mid
            if (collection.ElementAt(middleIndex).IDEqual(id))
            {
                return collection.ElementAt(middleIndex);
            }

            // If x greater, ignore left half
            if (collection.ElementAt(middleIndex).ID < id)
            {
                index = middleIndex + 1;
            }
            else // If x is smaller, ignore right half
            {
                maxIndex = middleIndex - 1;
            }
        }

        Debug.LogFormat("Can't find element for id: {0}", id);
        return default;
    }

    public static void AddAscending<T>(this List<T> collection, T item) where T : IComparable<T>
    {
        if(collection.Count == 0)
        {
            collection.Add(item);
            return;
        }

        // First edge case when searched element is greater that last element in collection.
        if(collection[collection.Count -1].CompareTo(item) <= 0)
        {
            collection.Add(item);
            return;
        }

        // Second edge case when searched element is smaller than smallest element in collection.
        if(collection[0].CompareTo(item) > 0)
        {
            collection.Insert(0, item);
            return;
        }

        // BinarySearch explain:
        // https://docs.microsoft.com/pl-pl/dotnet/api/system.array.binarysearch?view=net-5.0
        int index = collection.BinarySearch(item);
        if (index < 0)
        {
            index = ~index;
        }
        collection.Insert(index, item);
    }

    public static List<T> Shuffle<T>(this IList<T> list)
    {
        List<T> output = new List<T>(list);

        int n = list.Count;
        while (n >= 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, output.Count);
            T value = output[k];
            output[k] = output[n];
            output[n] = value;
        }

        return output;
    }

    public static Vector3 RandomPointInBounds(this Bounds bounds)
    {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public static Vector3 Abs(this Vector3 vector)
    {
        return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }

    public static Vector3 Mul(this Vector3 vector, Vector3 scaler)
    {
        return Vector3.Scale(vector, scaler);
    }

    #endregion

    #region Handlers


    #endregion
}
