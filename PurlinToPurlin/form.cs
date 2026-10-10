using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Plugins;
using Tekla.Structures.Dialog;

public partial class My_Vip_pro_PurlinConnection : PluginFormBase
{
    public My_Vip_pro_PurlinConnection()
    {
        InitializeComponent();
    }

    private void okApplyModifyGetOnOffCancel1_ApplyClicked(object sender, EventArgs e)
    {
        this.Apply();
    }

    private void okApplyModifyGetOnOffCancel1_CancelClicked(object sender, EventArgs e)
    {
        this.Close();
    }

    private void okApplyModifyGetOnOffCancel1_GetClicked(object sender, EventArgs e)
    {
        this.Get();
    }

    private void okApplyModifyGetOnOffCancel1_ModifyClicked(object sender, EventArgs e)
    {
        this.Modify();
    }

    private void okApplyModifyGetOnOffCancel1_OkClicked(object sender, EventArgs e)
    {
        this.Apply();
        this.Close();
    }

    private void okApplyModifyGetOnOffCancel1_OnOffClicked(object sender, EventArgs e)
    {
        this.ToggleSelection();
    }
}

