using System;

namespace NSBus
{
    public static class Guard
    {
        /// <summary>Checks an argument to ensure that it isn't null.</summary>
        /// <param name="argumentValue">The argument value to check.</param>
        /// <param name="argumentName">The name of the argument.</param>
      public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw new ArgumentNullException(argumentName);
        }
    }
}