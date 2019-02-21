using SabertoothServer;
using System;
using System.Windows.Forms;
using static System.Convert;
using System.Data.SqlClient;
using static SabertoothServer.Globals;

namespace Editor.Forms
{
    public partial class ChatEditor : Form
    {
        Chat e_Chat = new Chat();
        int SelectedIndex;
        int SelectedItem;
        bool UnModSave;

        public ChatEditor()
        {
            InitializeComponent();
            LoadItemList();
        }

        private void LoadItemList()
        {
            string connection = "Data Source=" + Server.sqlServer + ";Initial Catalog=" + Server.sqlDatabase + ";Integrated Security=True";
            using (var sql = new SqlConnection(connection))
            {
                sql.Open();
                string command = "SELECT COUNT(*) FROM CHAT";
                using (SqlCommand cmd = new SqlCommand(command, sql))
                {
                    object count = cmd.ExecuteScalar();
                    int result = ToInt32(count);
                    lstIndex.Items.Clear();
                    for (int i = 0; i < result; i++)
                    {
                        e_Chat.LoadChatNameFromDatabase(i + 1);
                        lstIndex.Items.Add(e_Chat.Name);
                    }
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            e_Chat.Name = txtName.Text;
        }

        private void txtMainMessage_TextChanged(object sender, EventArgs e)
        {
            e_Chat.MainMessage = txtMainMessage.Text;
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            e_Chat.CreateChatInDatabase();
            lstIndex.Items.Add(e_Chat.Name);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            e_Chat.SaveChatInDatabase(SelectedIndex);
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
            e_Chat.LoadChatFromDatabase(SelectedIndex);
            txtName.Text = e_Chat.Name;
            txtMainMessage.Text = e_Chat.MainMessage;
            txtOptA.Text = e_Chat.Option[0];
            txtOptB.Text = e_Chat.Option[1];
            txtOptC.Text = e_Chat.Option[2];
            txtOptD.Text = e_Chat.Option[3];
            cmbType.SelectedIndex = e_Chat.Type;
            scrlNextChatA.Value = e_Chat.NextChat[0];
            scrlNextChatB.Value = e_Chat.NextChat[1];
            scrlNextChatC.Value = e_Chat.NextChat[2];
            scrlNextChatD.Value = e_Chat.NextChat[3];
            scrlShopNum.Value = e_Chat.ShopNum;
            scrlQuestNum.Value = e_Chat.QuestNum;
            scrlMoney.Value = e_Chat.Money;
            scrlItem.Value = 1;
            SelectedItem = 1;
            scrlItemNum.Value = e_Chat.ItemNum[0];
            scrlValue.Value = e_Chat.ItemVal[0];
            lblNextChatA.Text = "Next Chat: " + scrlNextChatA.Value;
            lblNextChatB.Text = "Next Chat: " + scrlNextChatB.Value;
            lblNextChatC.Text = "Next Chat: " + scrlNextChatC.Value;
            lblNextChatD.Text = "Next Chat: " + scrlNextChatD.Value;
            lblShopNum.Text = "Shop Number: " + scrlShopNum.Value;
            lblQuestNum.Text = "Mission Number: " + scrlQuestNum.Value;
            lblMoney.Text = "Money: " + scrlMoney.Value;
            lblItem.Text = "Item: " + scrlItem.Value;
            lblItemNum.Text = "Item Number: " + scrlItemNum.Value;
            lblItemVal.Text = "Value: " + scrlValue.Value;
            UnModSave = false;
            if (pnlMain.Visible == false) { pnlMain.Visible = true; }
            if (pnlOptions.Visible == false) { pnlOptions.Visible = true; }
        }

        private void txtOptA_TextChanged(object sender, EventArgs e)
        {
            e_Chat.Option[0] = txtOptA.Text;
        }

        private void txtOptB_TextChanged(object sender, EventArgs e)
        {
            e_Chat.Option[1] = txtOptB.Text;
        }

        private void txtOptC_TextChanged(object sender, EventArgs e)
        {
            e_Chat.Option[2] = txtOptC.Text;
        }

        private void txtOptD_TextChanged(object sender, EventArgs e)
        {
            e_Chat.Option[3] = txtOptD.Text;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            e_Chat.Type = cmbType.SelectedIndex;
        }

        private void scrlShopNum_Scroll(object sender, ScrollEventArgs e)
        {
            lblShopNum.Text = "Shop Number: " + scrlShopNum.Value;
            e_Chat.ShopNum = scrlShopNum.Value;
        }

        private void scrlQuestNum_Scroll(object sender, ScrollEventArgs e)
        {
            lblQuestNum.Text = "Quest Number: " + scrlQuestNum.Value;
            e_Chat.QuestNum = scrlQuestNum.Value;
        }

        private void scrlMoney_Scroll(object sender, ScrollEventArgs e)
        {
            lblMoney.Text = "Money: " + scrlMoney.Value;
            e_Chat.Money = scrlMoney.Value;
        }

        private void scrlItem_Scroll(object sender, ScrollEventArgs e)
        {
            lblItem.Text = "Item: " + scrlItem.Value;
            SelectedItem = (scrlItem.Value - 1);
            scrlItemNum.Value = e_Chat.ItemNum[SelectedItem];
            lblItemNum.Text = "Item Number: " + scrlItemNum.Value;
            scrlValue.Value = e_Chat.ItemVal[SelectedItem];
            lblItemVal.Text = "Value: " + scrlValue.Value;
        }

        private void scrlItemNum_Scroll(object sender, ScrollEventArgs e)
        {
            lblItemNum.Text = "Item Number: " + scrlItemNum.Value;
            e_Chat.ItemNum[SelectedItem] = scrlItemNum.Value;
        }

        private void scrlValue_Scroll(object sender, ScrollEventArgs e)
        {
            lblItemVal.Text = "Value: " + scrlValue.Value;
            e_Chat.ItemVal[SelectedItem] = scrlValue.Value;
        }

        private void scrlNextChatA_Scroll(object sender, ScrollEventArgs e)
        {
            lblNextChatA.Text = "Next Chat: " + scrlNextChatA.Value;
            e_Chat.NextChat[0] = scrlNextChatA.Value;
        }

        private void scrlNextChatB_Scroll(object sender, ScrollEventArgs e)
        {
            lblNextChatB.Text = "Next Chat: " + scrlNextChatB.Value;
            e_Chat.NextChat[1] = scrlNextChatB.Value;
        }

        private void scrlNextChatC_Scroll(object sender, ScrollEventArgs e)
        {
            lblNextChatC.Text = "Next Chat: " + scrlNextChatC.Value;
            e_Chat.NextChat[2] = scrlNextChatC.Value;
        }

        private void scrlNextChatD_Scroll(object sender, ScrollEventArgs e)
        {
            lblNextChatD.Text = "Next Chat: " + scrlNextChatD.Value;
            e_Chat.NextChat[3] = scrlNextChatD.Value;
        }
    }
}
