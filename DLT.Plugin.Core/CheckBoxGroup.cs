using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DLT.Plugin.Core
{
    public class CheckBoxGroup
    {
        public CheckBox Visible { get; }
        public List<CheckBox> Members { get; } = new List<CheckBox>();

        public CheckBoxGroup(CheckBox visible, List<CheckBox> members)
        {
            this.Visible = visible;
            this.Members = members;
            this.BindEventsVisibleCheckBox();
        }

        public bool Contains(CheckBox ckb)
            => ckb == Visible || Members.Contains(ckb);

        private void BindEventsVisibleCheckBox()
        {
            this.Visible.Click += Visible_Click;
        }

        public void CheckedAllMember()
        {
            this.Visible.Checked = true;
            this.Members.ForEach(x => x.Checked = true);
        }

        private void Visible_Click(object sender, EventArgs e)
        {
            bool isChecked = this.Visible.Checked;
            foreach (var ckb in Members)
            {
                ckb.Checked = isChecked;
            }
        }
    }
}
