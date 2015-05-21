using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace 主控端
{
    /// <summary>
    /// 此类用于泛型
    /// 主要记录了上线主机的属性
    /// 还有用于通讯的基础套接字
    /// </summary>
    class Type_Client
    {
        String ip;
        /// <summary>
        /// 被控端主机的IP地址
        /// </summary>
        public String Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        String software;
        /// <summary>
        /// 被控端主机的软件版本
        /// </summary>
        public String Software
        {
            get { return software; }
            set { software = value; }
        }

        String computer_Name;
        /// <summary>
        /// 被控端主机的计算机名
        /// </summary>
        public String Computer_Name
        {
            get { return computer_Name; }
            set { computer_Name = value; }
        }

        String customer;
        /// <summary>
        /// 被控端主机的备注
        /// </summary>
        public String Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        String system_Info;
        /// <summary>
        /// 被控端主机的操作系统
        /// </summary>
        public String System_Info
        {
            get { return system_Info; }
            set { system_Info = value; }
        }

        String cpu;
        /// <summary>
        /// 被控端主机的CPU频率
        /// </summary>
        public String Cpu
        {
            get { return cpu; }
            set { cpu = value; }
        }

        String memory;
        /// <summary>
        /// 被控端主机的内存容量
        /// </summary>
        public String Memory
        {
            get { return memory; }
            set { memory = value; }
        }

        Socket socket;
        /// <summary>
        /// 被控端主机的套接字
        /// </summary>
        public Socket Socket
        {
            get { return socket; }
            set { socket = value; }
        }
    }
}
