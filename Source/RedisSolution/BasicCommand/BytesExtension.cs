using System;
using System.Linq;
using System.Text;

using ServiceStack.Common.Extensions;

namespace BasicCommand
{
    public static class BytesExtension

    {
        public static void ToConsole(this byte[][] source)
        {
            source
                .Select(e => Encoding.UTF8.GetString(e))
                .ForEach(Console.WriteLine);
        }

        public static void ToConsole(this byte[] source)
        {
            var valor = source == null
                ? "notFound"
                : Encoding.UTF8.GetString(source);

            Console.WriteLine(valor);
        }
    }
}