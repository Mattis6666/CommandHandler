using System;

namespace CommandHandler
{

    public class Command : Attribute
    {
        public string val;
        public Command(string val) => this.val = val;

    }
    public class Usage : Attribute
    {
        public string val;
        public Usage(string val) => this.val = val;

    }
    public class Aliases : Attribute
    {
        public string[] val;
        public Aliases(params string[] val) => this.val = val;

    }

}