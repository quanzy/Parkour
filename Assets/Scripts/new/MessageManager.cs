using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class MessageManager
    {
        public static MessageManager _instance;
        public bool isShow = true;
        public string context = "test";
        public static MessageManager Instance
        {
            get { return _instance; }
        }

        void OnGUI() 
        {
            if (isShow)
            {

            }
        }
    }
}
