using System;
using System.Collections.Generic;
using System.Text;

namespace Control_Client
{
    class Globle
    {
        public static int Port = 9999;                      //默认上线端口
        public static int UDP_Port = 9999;                      //默认上线端口
        //public static String Host = "admin002.f3322.org";            //默认主控端地址
        public static String Host = "127.0.0.1";            //默认主控端地址
        public static String Software = "专用版本";         //软件版本
        public static String Customer = "无";               //客户端注释
        public static bool _IsListen_Port = true;           //是否监听端口
        public static bool _IsResvice_Message = true;       //是否接收消息
        public static int Lis_Port = 6666;                  //自身监听端口
    }
}
