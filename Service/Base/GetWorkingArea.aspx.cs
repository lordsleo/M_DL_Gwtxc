﻿//
//文件名：    GetWorkingArea.aspx.cs
//功能描述：  获取作业数据列表数据（基础数据）
//创建时间：  2015/09/18
//作者：      
//修改时间：  暂无
//修改描述：  暂无
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Leo;
using ServiceInterface.Common;

namespace M_DL_Gwtxc.Service.Base
{
    public partial class GetWorkingArea : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strSql =
                    string.Format("select distinct code_workingarea,workingarea,logogram from TB_CODE_WORKINGAREA order by logogram");
                var dt = new Leo.Oracle.DataAccess(RegistryKey.KeyPathNewBase).ExecuteTable(strSql);
                if (dt.Rows.Count <= 0)
                {
                    Json = JsonConvert.SerializeObject(new DicPackage(false, null, "暂无数据！").DicInfo());
                    return;
                }

                string[,] strArray = new string[dt.Rows.Count, 3];
                for (int iRow = 0; iRow < dt.Rows.Count; iRow++)
                {
                    strArray[iRow, 0] = Convert.ToString(dt.Rows[iRow]["code_workingarea"]);
                    strArray[iRow, 1] = Convert.ToString(dt.Rows[iRow]["workingarea"]);
                    strArray[iRow, 2] = Convert.ToString(dt.Rows[iRow]["logogram"]);
                }

                Json = JsonConvert.SerializeObject(new DicPackage(true, strArray, null).DicInfo());
            }
            catch (Exception ex)
            {
                Json = JsonConvert.SerializeObject(new DicPackage(false, null, string.Format("{0}：获取数据发生异常。{1}", ex.Source, ex.Message)).DicInfo());
            }
        }
        protected string Json;
    }
}