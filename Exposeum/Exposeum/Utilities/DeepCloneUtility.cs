using System;
using Newtonsoft.Json;

namespace Exposeum.Utilities
{
	//This class is static and is designed as a utility class 
	//because the Clone method forces the class to be static
	public static class DeepCloneUtility
	{
		/// <summary>
		/// Perform a deep Copy of the object, using Json as a serialisation method.
		/// </summary>
		/// <typeparam name="T">The type of object being copied.</typeparam>
		/// <param name="source">The object instance to copy.</param>
		/// <returns>The copied object.</returns>
		public static T Clone<T>(this T source){            
			// Don't serialize a null object, simply return the default for that object
			if (Object.ReferenceEquals(source, null))
			{
				return default(T);
			}

			// initialize inner objects individually
			// for example in default constructor some list property initialized with some values,
			// but in 'source' these items are cleaned -
			// without ObjectCreationHandling.Replace default constructor values will be added to result
			var deserializeSettings = new JsonSerializerSettings {ObjectCreationHandling = ObjectCreationHandling.Replace};
			var serializedObject = JsonConvert.SerializeObject (source);

			return JsonConvert.DeserializeObject<T>(serializedObject, deserializeSettings);
		}

	}
}

