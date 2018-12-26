class DebugWindow
{
	    Label d_Stats;
        Label d_Level;
        Label d_Health;
        Label d_Hunger;
        Label d_Hydration;
        Label d_Exp;
        Label d_Money;
        Label d_Armor;
        Label d_Strength;
        Label d_Agility;
        Label d_Endurance;
        Label d_Stamina;
        Label d_MainClip;
        Label d_PistolAmmo;
        Label d_AssaultAmmo;
        Label d_AttackSpeed;
        Label d_ReloadSpeed;
        Label d_Points;
		
		public void CreateDebugWindow()
		{
			d_Stats = new Label(d_Window);
			d_Stats.SetPosition(10, 130);
			d_Stats.Text = "Stats:";

			d_Level = new Label(d_Window);
			d_Level.SetPosition(10, 140);
			d_Level.Text = "Level: ?";

			d_Health = new Label(d_Window);
			d_Health.SetPosition(10, 150);
			d_Health.Text = "Health: ?";

			d_Hunger = new Label(d_Window);
			d_Hunger.SetPosition(10, 160);
			d_Hunger.Text = "Hunger: ?";

			d_Hydration = new Label(d_Window);
			d_Hydration.SetPosition(10, 170);
			d_Hydration.Text = "Hydration: ?";

			d_Exp = new Label(d_Window);
			d_Exp.SetPosition(10, 180);
			d_Exp.Text = "Experience: ?";

			d_Money = new Label(d_Window);
			d_Money.SetPosition(10, 190);
			d_Money.Text = "Money: ?";

			d_Armor = new Label(d_Window);
			d_Armor.SetPosition(10, 200);
			d_Armor.Text = "Armor: ?";

			d_Strength = new Label(d_Window);
			d_Strength.SetPosition(10, 210);
			d_Strength.Text = "Strength: ?";

			d_Agility = new Label(d_Window);
			d_Agility.SetPosition(10, 220);
			d_Agility.Text = "Agility: ?";

			d_Endurance = new Label(d_Window);
			d_Endurance.SetPosition(10, 230);
			d_Endurance.Text = "Endurance: ?";

			d_Stamina = new Label(d_Window);
			d_Stamina.SetPosition(10, 240);
			d_Stamina.Text = "Stamina: ?";

			d_MainClip = new Label(d_Window);
			d_MainClip.SetPosition(10, 250);
			d_MainClip.Text = "Clip: ?";

			d_PistolAmmo = new Label(d_Window);
			d_PistolAmmo.SetPosition(10, 260);
			d_PistolAmmo.Text = "Pistol Ammo: ?";

			d_AssaultAmmo = new Label(d_Window);
			d_AssaultAmmo.SetPosition(10, 270);
			d_AssaultAmmo.Text = "Assault Ammo: ?";

			d_AttackSpeed = new Label(d_Window);
			d_AttackSpeed.SetPosition(10, 280);
			d_AttackSpeed.Text = "Attack Speed: ?";

			d_ReloadSpeed = new Label(d_Window);
			d_ReloadSpeed.SetPosition(10, 290);
			d_ReloadSpeed.Text = "Reload Speed: ?";

			d_Points = new Label(d_Window);
			d_Points.SetPosition(10, 300);
			d_Points.Text = "Points: ?";
		}
		
		public void UpdateDebugWindow()
		{
			d_Level.Text = "Level: " + c_Player[drawIndex].Level;
			d_Health.Text = "Health: " + c_Player[drawIndex].Health + " / " + c_Player[drawIndex].MaxHealth;
			d_Hunger.Text = "Hunger: " + c_Player[drawIndex].Hunger + " / 100";
			d_Hydration.Text = "Hydration: " + c_Player[drawIndex].Hydration + " / 100";
			d_Exp.Text = "Experience: " + c_Player[drawIndex].Experience + " / " + (p_level * 1000);
			d_Money.Text = "Money: " + c_Player[drawIndex].Money;
			d_Armor.Text = "Armor: " + c_Player[drawIndex].Armor;
			d_Strength.Text = "Strength: " + c_Player[drawIndex].Strength;
			d_Agility.Text = "Agility: " + c_Player[drawIndex].Agility;
			d_Endurance.Text = "Endurance: " + c_Player[drawIndex].Endurance;
			d_Stamina.Text = "Stamina: " + c_Player[drawIndex].Stamina;
			d_MainClip.Text = "Clip: " + c_Player[drawIndex].mainWeapon.Clip + " / " + c_Player[drawIndex].mainWeapon.maxClip;
			d_PistolAmmo.Text = "Pistol Ammo: " + c_Player[drawIndex].PistolAmmo;
			d_AssaultAmmo.Text = "Assault Ammo: " + c_Player[drawIndex].AssaultAmmo;
			d_AttackSpeed.Text = "Attack Speed: " + c_Player[drawIndex].mainWeapon.AttackSpeed;
			d_ReloadSpeed.Text = "Reload Speed: " + c_Player[drawIndex].mainWeapon.ReloadSpeed;
			d_Points.Text = "Points: " + c_Player[drawIndex].Points;
		}
}