using System;

namespace EchoEvent
{
    [Serializable]
    public class EchoEventConfig
    {
        public string name;
        public string message;
        public string prefabName;
        public string uiName;

        public EchoEventConfig() { }

        public EchoEventConfig(string _name, string msg)
        {
            name = _name;
            message = msg;
        }

        public EchoEventConfig(string name, string message, string prefabName, string uiName) : this(name, message)
        {
            this.prefabName = prefabName;
            this.uiName = uiName;
        }
    }
}
