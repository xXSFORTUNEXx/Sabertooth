        //Create some default ones for testing
        //public Item mainWeapon = new Item("Assult Rifle", 1, 100, 0, (int)ItemType.RangedWeapon, 250, 0, 0, 0, 0, 0, 0, 0, 30, 30, (int)AmmoType.AssaultRifle);

		        /*public void RemoveBulletFromClip(NetServer s_Server, Player[] s_Player, int index)
        {
            if (mainWeapon.Clip > 0)
            {
                mainWeapon.Clip -= 1;
                attackTick = TickCount;
            }

            if (mainWeapon.Clip == 0)
            {
                ReloadClip();
                reloadTick = TickCount;
            }

            SendUpdateAmmo(s_Server, s_Player, index);
        }*/

        /*public void ReloadClip()
        {
            if (mainWeapon.Clip == 0)
            {
                switch (mainWeapon.ammoType)
                {
                    case (int)AmmoType.None:
                        return;
                    case (int)AmmoType.Pistol:
                        if (PistolAmmo > mainWeapon.maxClip)
                        {
                            mainWeapon.Clip = mainWeapon.maxClip;
                            PistolAmmo -= mainWeapon.maxClip;                            
                        }
                        else
                        {
                            mainWeapon.Clip = PistolAmmo;
                            PistolAmmo = 0;
                        }
                        break;
                    case (int)AmmoType.AssaultRifle:
                        if (AssaultAmmo > mainWeapon.maxClip)
                        {
                            mainWeapon.Clip = mainWeapon.maxClip;
                            AssaultAmmo -= mainWeapon.maxClip;
                        }
                        else
                        {
                            mainWeapon.Clip = AssaultAmmo;
                            AssaultAmmo = 0;
                        }
                        break;
                    case (int)AmmoType.Rocket:
                        if (RocketAmmo > mainWeapon.maxClip)
                        {
                            mainWeapon.Clip = mainWeapon.maxClip;
                            RocketAmmo = -mainWeapon.maxClip;
                        }
                        else
                        {
                            mainWeapon.Clip = RocketAmmo;
                            RocketAmmo = 0;
                        }
                        break;
                    case (int)AmmoType.Grenade:
                        if (GrenadeAmmo > mainWeapon.maxClip)
                        {
                            mainWeapon.Clip = mainWeapon.maxClip;
                            GrenadeAmmo = -mainWeapon.maxClip;
                        }
                        else
                        {
                            mainWeapon.Clip = GrenadeAmmo;
                            GrenadeAmmo = 0;
                        }
                        break;
                }
            }
            else
            {
                switch (mainWeapon.ammoType)
                {
                    case (int)AmmoType.None:
                        return;
                    case (int)AmmoType.Pistol:
                        if (PistolAmmo > (mainWeapon.maxClip - mainWeapon.Clip))
                        {
                            PistolAmmo -= (mainWeapon.maxClip - mainWeapon.Clip);
                            mainWeapon.Clip = mainWeapon.maxClip;
                            reloadTick = TickCount;
                        }
                        else
                        {
                            mainWeapon.Clip += PistolAmmo;
                            PistolAmmo = 0;
                            reloadTick = TickCount;
                        }
                        break;
                    case (int)AmmoType.AssaultRifle:
                        if (AssaultAmmo > (mainWeapon.maxClip - mainWeapon.Clip))
                        {
                            AssaultAmmo -= (mainWeapon.maxClip - mainWeapon.Clip);
                            mainWeapon.Clip = mainWeapon.maxClip;
                        }
                        else
                        {
                            mainWeapon.Clip += AssaultAmmo;
                            AssaultAmmo = 0;
                        }
                        break;
                    case (int)AmmoType.Rocket:
                        if (AssaultAmmo > (mainWeapon.maxClip - mainWeapon.Clip))
                        {
                            AssaultAmmo -= (mainWeapon.maxClip - mainWeapon.Clip);
                            mainWeapon.Clip = mainWeapon.maxClip;
                        }
                        else
                        {
                            mainWeapon.Clip += AssaultAmmo;
                            AssaultAmmo = 0;
                        }
                        break;
                    case (int)AmmoType.Grenade:
                        if (GrenadeAmmo > (mainWeapon.maxClip - mainWeapon.Clip))
                        {
                            GrenadeAmmo -= (mainWeapon.maxClip - mainWeapon.Clip);
                            mainWeapon.Clip = mainWeapon.maxClip;
                        }
                        else
                        {
                            mainWeapon.Clip += GrenadeAmmo;
                            GrenadeAmmo = 0;
                        }
                        break;
                }
            }
        }*/

        //Send updated ammo reserve and clip
        /*public void SendUpdateAmmo(NetServer s_Server, Player[] s_Player, int index)
        {
            NetOutgoingMessage outMSG = s_Server.CreateMessage();
            if (s_Player[index].Connection != null)
            {
                outMSG.Write((byte)PacketTypes.UpdateAmmo);
                outMSG.WriteVariableInt32(index);
                outMSG.WriteVariableInt32(s_Player[index].mainWeapon.Clip);
                outMSG.WriteVariableInt32(s_Player[index].PistolAmmo);
                outMSG.WriteVariableInt32(s_Player[index].AssaultAmmo);
                outMSG.WriteVariableInt32(s_Player[index].RocketAmmo);
                outMSG.WriteVariableInt32(s_Player[index].GrenadeAmmo);
                s_Server.SendMessage(outMSG, s_Player[index].Connection, NetDeliveryMethod.ReliableSequenced, 4);
            }
        }*/

		        /*void SendAttackData(NetClient c_Client, int index, bool melee)
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.Attack);
            outMSG.WriteVariableInt32(index);
            outMSG.WriteVariableInt32(Direction);
            outMSG.WriteVariableInt32(AimDirection);
            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableSequenced, 3);
        }*/
		
		        /*public void CheckAttack(NetClient c_Client, GUI c_GUI, Window c_Window, int index)
        {
            if (Attacking == true) { return; }
            if (c_GUI.inputChat.HasFocus == true) { return; }
            if (!c_Window.HasFocus()) { return; }

            bool isMelee = false;

            //Melee
            if (Keyboard.IsKeyPressed(Keyboard.Key.F))
            {
                AimDirection = Direction;
                Attacking = true;
                isMelee = true;
            }
            //Direction Up
            if (Keyboard.IsKeyPressed(Keyboard.Key.I))
            {
                AimDirection = (int)Directions.Up;
                Attacking = true;
            }
            //Direction Down
            if (Keyboard.IsKeyPressed(Keyboard.Key.K))
            {
                AimDirection = (int)Directions.Down;
                Attacking = true;
            }
            //Direction Left
            if (Keyboard.IsKeyPressed(Keyboard.Key.J))
            {
                AimDirection = (int)Directions.Left;
                Attacking = true;
            }
            //Direction Right
            if (Keyboard.IsKeyPressed(Keyboard.Key.L))
            {
                AimDirection = (int)Directions.Right;
                Attacking = true;
            }

            if (Attacking == true)
            {
                SendUpdateDirection(c_Client, index);
                SendAttackData(c_Client, index, isMelee);
                SendMovementData(c_Client, index);
                Attacking = false;
            }
        } */

		        /*void SendUpdateProj(NetClient c_Client, int slot, bool valid)
        {
            NetOutgoingMessage outMSG = c_Client.CreateMessage();
            outMSG.Write((byte)PacketTypes.UpdateProj);
            outMSG.WriteVariableInt32(slot);
            outMSG.Write(valid);
            outMSG.WriteVariableInt32(Owner);
            outMSG.WriteVariableInt32(X);
            outMSG.WriteVariableInt32(Y);
            outMSG.WriteVariableInt32(Direction);

            c_Client.SendMessage(outMSG, c_Client.ServerConnection, NetDeliveryMethod.ReliableSequenced, 5);
        }*/

        //Handle projectiledata
        /*void HandleUpdateProjectileData(NetIncomingMessage incMSG, NetServer s_Server,  Map[] s_Map, Player[] s_Player)
        {
            int slot = incMSG.ReadVariableInt32();
            bool valid = incMSG.ReadBoolean();
            int owner = incMSG.ReadVariableInt32();
            int cMap = s_Player[owner].Map;

            if (s_Map[cMap].mapProj[slot] == null) { return; }
            if (valid == true)
            {
                s_Map[cMap].mapProj[slot].X = incMSG.ReadVariableInt32();
                s_Map[cMap].mapProj[slot].Y = incMSG.ReadVariableInt32();
                s_Map[cMap].mapProj[slot].Direction = incMSG.ReadVariableInt32();
            }
            else
            {
                s_Map[cMap].ClearProjSlot(s_Server, s_Map, cMap, slot);
            }
        }*/

        //Handles when one of the players attacks
        /*void HandleAttackData(NetIncomingMessage incMSG, NetServer s_Server, Player[] s_Player, Map[] s_Map)
        {
            int index = incMSG.ReadVariableInt32();
            int direction = incMSG.ReadVariableInt32();
            int aimdirection = incMSG.ReadVariableInt32();

            int cMap = s_Player[index].Map;
            s_Player[index].Direction = direction;
            s_Player[index].AimDirection = aimdirection;

            if (s_Player[index].mainWeapon.Type == (int)ItemType.MeleeWeapon)
            {
                //Do a melee attack and update everyone
                return;
            }

            if (s_Player[index].mainWeapon.Type == (int)ItemType.RangedWeapon)
            {
                if (TickCount - s_Player[index].reloadTick < s_Player[index].mainWeapon.ReloadSpeed) { return; }
                
                if (TickCount - s_Player[index].attackTick < s_Player[index].mainWeapon.AttackSpeed) { return; }

                switch (s_Player[index].mainWeapon.ammoType)
                {
                    case (int)AmmoType.Pistol:
                        if (s_Player[index].PistolAmmo == 0 && s_Player[index].mainWeapon.Clip == 0) { return; }
                        break;

                    case (int)AmmoType.AssaultRifle:
                        if (s_Player[index].AssaultAmmo == 0 && s_Player[index].mainWeapon.Clip == 0) { return; }
                        break;

                    case (int)AmmoType.Rocket:
                        if (s_Player[index].RocketAmmo == 0 && s_Player[index].mainWeapon.Clip == 0) { return; }
                        break;

                    case (int)AmmoType.Grenade:
                        if (s_Player[index].GrenadeAmmo == 0 && s_Player[index].mainWeapon.Clip == 0) { return; }
                        break;

                    default:
                        return;
                }

                s_Map[cMap].CreateProjectile(s_Server, s_Player, index);
            }

            //update direction
            for (int i = 0; i < 5; i++)
            {
                if (s_Player[i].Connection != null && s_Player[i].Map == s_Player[index].Map)
                {
                    SendUpdateDirection(s_Server, s_Player[i].Connection, index, direction, aimdirection);
                }
            }
        }*/

		            //s_Player[playerIndex].RemoveBulletFromClip(s_Server, s_Player, playerIndex);
