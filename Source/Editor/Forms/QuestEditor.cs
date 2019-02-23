using SabertoothServer;
using System;
using System.Windows.Forms;
using static System.Convert;
using System.Data.SqlClient;
using static SabertoothServer.Globals;

namespace Editor.Forms
{
    public partial class QuestEditor : Form
    {
        Quests e_Quest = new Quests();
        int SelectedIndex;
        int SelectedItem;
        int SelectedReward;
        int SelectedNpc;
        bool UnModSave;

        public QuestEditor()
        {
            InitializeComponent();
            LoadItemList();
            scrlPreQuest.Maximum = MAX_QUESTS;
            scrlNpc.Maximum = MAX_QUEST_NPCS_REQ;
            scrlItem.Maximum = MAX_QUEST_ITEMS_REQ;
            scrlReward.Maximum = MAX_QUEST_REWARDS;
        }

        private void LoadItemList()
        {
            string connection = "Data Source=" + Server.sqlServer + ";Initial Catalog=" + Server.sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command = "SELECT COUNT(*) FROM QUESTS";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    object count = cmd.ExecuteScalar();
                    int result = ToInt32(count);
                    lstIndex.Items.Clear();
                    for (int i = 0; i < result; i++)
                    {
                        e_Quest.LoadQuestFromDatabase(i + 1);
                        lstIndex.Items.Add(e_Quest.Name);
                    }
                }
            }
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            e_Quest.CreateQuestInDatabase();
            lstIndex.Items.Add(e_Quest.Name);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            e_Quest.SaveQuestInDatabase(SelectedIndex);
            LoadItemList();
            UnModSave = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void lstIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UnModSave == true)
            {
                string w_Message = "Are you sure you want to load a different item? All unsaved progress will be lost.";
                string w_Caption = "Unsaved data";
                MessageBoxButtons w_Buttons = MessageBoxButtons.YesNo;
                DialogResult w_Result;
                w_Result = MessageBox.Show(w_Message, w_Caption, w_Buttons);
                if (w_Result == DialogResult.No) { return; }
            }
            SelectedIndex = (lstIndex.SelectedIndex + 1);
            if (SelectedIndex == 0) { return; }
            e_Quest.LoadQuestFromDatabase(SelectedIndex);
            txtName.Text = e_Quest.Name;
            txtStartMessage.Text = e_Quest.StartMessage;
            txtInprogressMessage.Text = e_Quest.InProgressMessage;
            txtCompleteMessage.Text = e_Quest.CompleteMessage;
            txtDesc.Text = e_Quest.Description;
            cmbType.SelectedIndex = e_Quest.Type;
            scrlPreQuest.Value = e_Quest.PrerequisiteQuest;
            scrlLevelRequired.Value = e_Quest.LevelRequired;
            scrlItem.Value = 1;
            scrlNpc.Value = 1;
            scrlReward.Value = 1;
            SelectedItem = 1;
            SelectedNpc = 1;
            SelectedReward = 1;
            scrlItemNum.Value = e_Quest.ItemNum[0];
            scrlItemValue.Value = e_Quest.ItemValue[0];
            scrlNpcNum.Value = e_Quest.NpcNum[0];
            scrlNpcValue.Value = e_Quest.NpcValue[0];
            scrlRewardNum.Value = e_Quest.RewardItem[0];
            scrlRewardValue.Value = e_Quest.RewardValue[0];
            scrlExperience.Value = e_Quest.Experience;
            scrlMoney.Value = e_Quest.Money;
            lblPreReq.Text = "Prereq Quest: " + scrlPreQuest.Value;
            lblLevelReq.Text = "Level Required: " + scrlLevelRequired.Value;
            lblItem.Text = "Item: " + scrlItem.Value;
            lblItemNum.Text = "Item Num: " + scrlItemNum.Value;
            lblItemValue.Text = "Value: " + scrlItemValue.Value;
            lblNpc.Text = "Npc: " + scrlNpc.Value;
            lblNpcNum.Text = "Npc Num: " + scrlNpcNum.Value;
            lblNpcValue.Text = "Value: " + scrlNpcValue.Value;
            lblReward.Text = "Reward: " + scrlReward.Value;
            lblRewardNum.Text = "Reward Num: " + scrlRewardNum.Value;
            lblRewardValue.Text = "Value: " + scrlRewardValue.Value;
            lblExperience.Text = "Experience: " + scrlExperience.Value;
            lblMoney.Text = "Money: " + scrlMoney.Value;
            if (pnlProperties.Visible == false) { pnlProperties.Visible = true; }
            if (pnlReq.Visible == false) { pnlReq.Visible = true; }
            if (pnlReqItems.Visible == false) { pnlReqItems.Visible = true; }
            if (pnlReqNpcs.Visible == false) { pnlReqNpcs.Visible = true; }
            if (pnlRewards.Visible == false) { pnlRewards.Visible = true; }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            e_Quest.Name = txtName.Text;
        }

        private void txtStartMessage_TextChanged(object sender, EventArgs e)
        {
            e_Quest.StartMessage = txtStartMessage.Text;
        }

        private void txtInprogressMessage_TextChanged(object sender, EventArgs e)
        {
            e_Quest.InProgressMessage = txtInprogressMessage.Text;
        }

        private void txtCompleteMessage_TextChanged(object sender, EventArgs e)
        {
            e_Quest.CompleteMessage = txtCompleteMessage.Text;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Quest.Type = (cmbType.SelectedIndex);            
        }

        private void scrlPreQuest_Scroll(object sender, ScrollEventArgs e)
        {
            lblPreReq.Text = "Prereq Quest: " + scrlPreQuest.Value;
            e_Quest.PrerequisiteQuest = scrlPreQuest.Value;
        }

        private void scrlLevelRequired_Scroll(object sender, ScrollEventArgs e)
        {
            lblLevelReq.Text = "Level Required: " + scrlLevelRequired.Value;
            e_Quest.LevelRequired = scrlLevelRequired.Value;
        }

        private void scrlItem_Scroll(object sender, ScrollEventArgs e)
        {
            lblItem.Text = "Item: " + scrlItem.Value;
            SelectedItem = (scrlItem.Value - 1);
            scrlItemNum.Value = e_Quest.ItemNum[SelectedItem];
            lblItemNum.Text = "Item Num: " + scrlItemNum.Value;
            scrlItemValue.Value = e_Quest.ItemValue[SelectedItem];
            lblItemValue.Text = "Value: " + scrlItemValue.Value;
        }

        private void scrlItemNum_Scroll(object sender, ScrollEventArgs e)
        {
            lblItemNum.Text = "Item Num: " + scrlItemNum.Value;
            e_Quest.ItemNum[SelectedItem] = scrlItemNum.Value;
        }

        private void scrlItemValue_Scroll(object sender, ScrollEventArgs e)
        {
            lblItemValue.Text = "Value: " + scrlItemValue.Value;
            e_Quest.ItemValue[SelectedItem] = scrlItemValue.Value;
        }

        private void scrlNpc_Scroll(object sender, ScrollEventArgs e)
        {
            lblNpc.Text = "Npc: " + scrlNpc.Value;
            SelectedNpc = (scrlNpc.Value - 1);
            scrlNpcNum.Value = e_Quest.NpcNum[SelectedNpc];
            lblNpcNum.Text = "Npc Num: " + scrlNpcNum.Value;
            scrlNpcValue.Value = e_Quest.NpcValue[SelectedNpc];
            lblNpcValue.Text = "Value: " + scrlNpcValue.Value;
        }

        private void scrlNpcNum_Scroll(object sender, ScrollEventArgs e)
        {
            lblNpcNum.Text = "Npc Num: " + scrlNpcNum.Value;
            e_Quest.NpcNum[SelectedNpc] = scrlNpcNum.Value;
        }

        private void scrlNpcValue_Scroll(object sender, ScrollEventArgs e)
        {
            lblNpcValue.Text = "Value: " + scrlNpcValue.Value;
            e_Quest.NpcValue[SelectedNpc] = scrlNpcValue.Value;
        }

        private void scrlReward_Scroll(object sender, ScrollEventArgs e)
        {
            lblReward.Text = "Reward: " + scrlReward.Value;
            SelectedReward = (scrlReward.Value - 1);
            scrlRewardNum.Value = e_Quest.RewardItem[SelectedReward];
            lblRewardNum.Text = "Reward Num: " + scrlRewardNum.Value;
            scrlRewardValue.Value = e_Quest.RewardValue[SelectedReward];
            lblRewardValue.Text = "Value: " + scrlRewardValue.Value;
        }

        private void scrlRewardNum_Scroll(object sender, ScrollEventArgs e)
        {
            lblRewardNum.Text = "Reward Num: " + scrlRewardNum.Value;
            e_Quest.RewardItem[SelectedReward] = scrlRewardNum.Value;
        }

        private void scrlRewardValue_Scroll(object sender, ScrollEventArgs e)
        {
            lblRewardValue.Text = "Value: " + scrlRewardValue.Value;
            e_Quest.RewardValue[SelectedReward] = scrlRewardValue.Value;
        }

        private void scrlExperience_Scroll(object sender, ScrollEventArgs e)
        {
            lblExperience.Text = "Experience: " + scrlExperience.Value;
            e_Quest.Experience = scrlExperience.Value;
        }

        private void scrlMoney_Scroll(object sender, ScrollEventArgs e)
        {
            lblMoney.Text = "Money: " + scrlMoney.Value;
            e_Quest.Money = scrlMoney.Value;
        }

        private void txtDesc_TextChanged(object sender, EventArgs e)
        {
            e_Quest.Description = txtDesc.Text;
        }
    }
}
