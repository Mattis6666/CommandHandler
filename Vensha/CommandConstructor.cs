using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Vensha
{
    public class CommandConstructor
    {
        public string name;
        public string usage;
        public IEnumerable<string> aliases;
        private MethodInfo method;
        private Type type;

        public CommandConstructor(MethodInfo method, Type type)
        {
            this.method = method;
            this.type = type;

            var attrs = method.GetCustomAttributes();
            if (attrs.FirstOrDefault(x => x is Command) as Command == null) throw new Exception("How did this happen?");

            foreach (var attr in attrs)
            {
                switch (attr)
                {
                    case Command c:
                        this.name = c.val;
                        break;
                    case Usage u:
                        this.usage = u.val;
                        break;
                    case Aliases a:
                        this.aliases = a.val;
                        break;
                }
            }
        }
        public Task callback(params object[] args)
        {
            var constructor = this.type.GetConstructor(Type.EmptyTypes);
            var cmd = constructor.Invoke(new object[] { });

            return this.method.Invoke(cmd, args) as Task;
        }
    }

    public class VenshaCommand { }
}