using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using ServiceStack.Common.Extensions;
using ServiceStack.Redis;

namespace Intersection
{
    class Program
    {
        

        static void Main(string[] args)
        {

            var usuariosOnline = new UsuariosOnLine();
            usuariosOnline.Launch();

            Console.ReadLine();
        }

       
    }
}
