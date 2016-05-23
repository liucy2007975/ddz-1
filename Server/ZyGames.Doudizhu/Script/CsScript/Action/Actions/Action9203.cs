﻿using System;
using System.Collections.Generic;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace ZyGames.Doudizhu.Script.CsScript.Action
{
   
    /// <summary>
    /// 公告列表接口
    /// </summary>
    /// <remarks>继续BaseStruct类:允许无身份认证的请求;AuthorizeAction:需要身份认证的请求</remarks>
    public class Action9203 : BaseAction
    {
        
        #region class object
        /// <summary>
        /// 
        /// </summary>
        class Class_1
        {
            /// <summary>
            /// 公告标题
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 公告内容
            /// </summary>
            public string Content { get; set; }
            /// <summary>
            /// 发送时间
            /// </summary>
            public string SendDate { get; set; }
            
        }

        /// <summary>
        /// Main Body
        /// </summary>
        class ResponsePacket
        {
            /// <summary>
            /// 总页数
            /// </summary>
            public int PageCount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Class_1> _dsItemList_1 { get; set; }
            
        }
        #endregion

        /// <summary>
        /// 响应数据包
        /// </summary>
        private ResponsePacket _packet = new ResponsePacket();
        /// <summary>
        /// 内部参数
        /// </summary>
        private int _gameType;
        /// <summary>
        /// 内部参数
        /// </summary>
        private int _serverID;
        /// <summary>
        /// 当前页
        /// </summary>
        private int _pageIndex;
        /// <summary>
        /// 每页条数
        /// </summary>
        private int _pageSize;
                

        public Action9203(HttpGet httpGet)
            : base(ActionIDDefine.Cst_Action9203, httpGet)
        {
            
        }

        /// <summary>
        /// 检查的Action是否需要授权访问
        /// </summary>
        protected override bool IgnoreActionId
        {
            get{ return true; }
        }

        /// <summary>
        /// 客户端请求的参数较验
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool GetUrlElement()
        {
            if (httpGet.GetInt("GameType", ref _gameType)            
                && httpGet.GetInt("ServerID", ref _serverID)            
                && httpGet.GetInt("PageIndex", ref _pageIndex)            
                && httpGet.GetInt("PageSize", ref _pageSize))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 业务逻辑处理
        /// </summary>
        /// <returns>false:中断后面的方式执行并返回Error</returns>
        public override bool TakeAction()
        {
            return true;
        }

        /// <summary>
        /// 下发给客户的包结构数据
        /// </summary>
        public override void BuildPacket()
        {
            this.PushIntoStack(_packet.PageCount);
            this.PushIntoStack(_packet._dsItemList_1.Count);
            foreach (var item in _packet._dsItemList_1)
            {
                DataStruct dsItem = new DataStruct();
                dsItem.PushIntoStack(item.Title);
                dsItem.PushIntoStack(item.Content);
                dsItem.PushIntoStack(item.SendDate);

                this.PushIntoStack(dsItem);
            }

        }

    }
}
