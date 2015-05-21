using System;
using System.Collections.Generic;
using System.Text;

namespace 主控端
{
    class Globle
    {
        public static int Online_Number = 0;               //上线主机数量
        public static int Port = 9999;                     //默认上线端口
        public static int UDP_Port = 9999;                 //默认上线端口
        public static int ClientPort = 6666;               //默认上线端口
        public static bool _IsListen_Port = true;          //是否监听端口
        public static bool _IsResvice_Message = true;      //是否接收消息
        //泛型，用于保存上线主机集合
        public static List<Type_Client> Online_Computer_Attr = new List<Type_Client>();
    }
}
