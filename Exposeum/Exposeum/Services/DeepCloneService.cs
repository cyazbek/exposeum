using System;

namespace Exposeum
{
	public abstract class DeepCloneService
	{
		/// <summary>
		/// Perform a deep Copy of the object, using Json as a serialisation method.
		/// </summary>
		/// <typeparam name="T">The type of object being copied.</typeparam>
		/// <param name="source">The object instance to copy.</param>
		/// <returns>The copied object.</returns>
		public static T Clone<T>(this T source);

	}
}

