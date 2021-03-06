        public void DepositItem(int index, int slot, int value)
        {
            if (players[index].Backpack[slot].Name != "None")
            {
                if (players[index].Backpack[slot].Stackable)
                {
                    int bankSlot = FindSameBankItem(players[index].Bank, players[index].Backpack[slot]);

                    if (bankSlot < MAX_BANK_SLOTS)
                    {
                        if (players[index].Backpack[slot].Value == value)
                        {
                            players[index].Bank[bankSlot].Value += value;
                            players[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, false);
                        }
                        else
                        {
                            Logging.WriteMessageLog("Before Values:DSlot1=" + slot + ",Value=" + value +
                                                                                    ", Bank=" + players[index].Bank[bankSlot].Value +
                                                                                    ", Backpack=" + players[index].Backpack[slot].Value +
                                                                                    ", Index=" + index +
                                                                                    ", ExtSlot=" + bankSlot, "MoveItem");

                            players[index].Backpack[slot].Value -= value;
                            players[index].Bank[bankSlot].Value += value;

                            Logging.WriteMessageLog("After Values:DSlot1=" + slot + ",Value=" + value + 
                                                                                    ", Bank=" + players[index].Bank[bankSlot].Value + 
                                                                                    ", Backpack=" + players[index].Backpack[slot].Value +
                                                                                    ", Index=" + index +
                                                                                    ", ExtSlot=" + bankSlot, "MoveItem");
                        }
                        HandleData.SendPlayerBank(index);
                        HandleData.SendPlayerInv(index);
                    }
                    else
                    {
                        int newSlot = FindOpenBankSlot(players[index].Bank);

                        if (newSlot < MAX_BANK_SLOTS)
                        {
                            if (players[index].Backpack[slot].Value > value)
                            {
                                Logging.WriteMessageLog("Before Values:DSlot2=" + slot + ",Value=" + value +
                                                                                            ", Bank=" + players[index].Bank[newSlot].Value +
                                                                                            ", Backpack=" + players[index].Backpack[slot].Value +
                                                                                            ", Index=" + index +
                                                                                            ", NewSlot=" + newSlot, "MoveItem");

                                players[index].Backpack[slot].Value -= value;
                                players[index].Bank[newSlot] = players[index].Backpack[slot];
                                players[index].Bank[newSlot].Value = value;

                                Logging.WriteMessageLog("After Values:DSlot2=" + slot + ",Value=" + value +
                                                                                        ", Bank=" + players[index].Bank[newSlot].Value +
                                                                                        ", Backpack=" + players[index].Backpack[slot].Value +
                                                                                        ", Index=" + index +
                                                                                        ", NewSlot=" + newSlot, "MoveItem");
                            }
                            else
                            {
                                players[index].Bank[newSlot] = players[index].Backpack[slot];
                                players[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, false);
                            }
                            HandleData.SendPlayerBank(index);
                            HandleData.SendPlayerInv(index);
                        }
                        else
                        {
                            HandleData.SendServerMessageTo(Connection, "You bank is full!");
                        }
                    }
                }
                else
                {
                    int newSlot = FindOpenBankSlot(players[index].Bank);

                    if (newSlot < MAX_BANK_SLOTS)
                    {
                        if (players[index].Backpack[slot].Value > value)
                        {
                            Logging.WriteMessageLog("Before Values:DSlot3=" + slot + ",Value=" + value +
                                                                                        ", Bank=" + players[index].Bank[newSlot].Value +
                                                                                        ", Backpack=" + players[index].Backpack[slot].Value +
                                                                                        ", Index=" + index +
                                                                                        ", NewSlot=" + newSlot, "MoveItem");

                            players[index].Bank[newSlot] = players[index].Backpack[slot];
                            players[index].Bank[newSlot].Value = value;
                            players[index].Backpack[slot].Value -= value;

                            Logging.WriteMessageLog("After Values:DSlot3=" + slot + ",Value=" + value +
                                                                                    ", Bank=" + players[index].Bank[newSlot].Value +
                                                                                    ", Backpack=" + players[index].Backpack[slot].Value +
                                                                                    ", Index=" + index +
                                                                                    ", NewSlot=" + newSlot, "MoveItem");
                        }
                        else
                        {
                            players[index].Bank[newSlot] = players[index].Backpack[slot];
                            players[index].Backpack[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, false);
                        }
                        HandleData.SendPlayerBank(index);
                        HandleData.SendPlayerInv(index);
                    }
                    else
                    {
                        HandleData.SendServerMessageTo(Connection, "You bank is full!");
                    }
                }
            }
        }

        public void WithdrawItem(int index, int slot, int value)
        {
            if (players[index].Bank[slot].Name != "None")
            {
                if (players[index].Bank[slot].Stackable)
                {
                    int extSlot = FindSameInvItem(players[index].Backpack, players[index].Bank[slot]);

                    if (extSlot < MAX_INV_SLOTS)
                    {
                        if (players[index].Bank[slot].Value == value)
                        {
                            players[index].Backpack[extSlot].Value += value;
                            players[index].Bank[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, false);
                        }
                        else
                        {
                            Logging.WriteMessageLog("Before Values:WSlot1=" + slot + ",Value=" + value +
                                                                                    ", Bank=" + players[index].Bank[slot].Value +
                                                                                    ", Backpack=" + players[index].Backpack[extSlot].Value +
                                                                                    ", Index=" + index +
                                                                                    ", ExtSlot=" + extSlot, "MoveItem");
                            players[index].Bank[slot].Value -= value;
                            players[index].Backpack[extSlot].Value += value;

                            Logging.WriteMessageLog("After Values:WSlot1=" + slot + ",Value=" + value +
                                                                                   ", Bank=" + players[index].Bank[slot].Value +
                                                                                   ", Backpack=" + players[index].Backpack[extSlot].Value +
                                                                                   ", Index=" + index +
                                                                                   ", ExtSlot=" + extSlot, "MoveItem");
                        }
                        HandleData.SendPlayerBank(index);
                        HandleData.SendPlayerInv(index);
                    }
                    else
                    {
                        int newSlot = FindOpenInvSlot(players[index].Backpack);

                        if (newSlot < MAX_INV_SLOTS)
                        {
                            if (players[index].Bank[slot].Value > value)
                            {
                                Logging.WriteMessageLog("Before Values:WSlot2=" + slot + ",Value=" + value +
                                                                                            ", Bank=" + players[index].Bank[slot].Value +
                                                                                            ", Backpack=" + players[index].Backpack[newSlot].Value +
                                                                                            ", Index=" + index +
                                                                                            ", NewSlot=" + newSlot, "MoveItem");

                                players[index].Backpack[newSlot] = players[index].Bank[slot];
                                players[index].Backpack[newSlot].Value = value;
                                players[index].Bank[slot].Value -= value;

                                Logging.WriteMessageLog("After Values:WSlot2=" + slot + ",Value=" + value +
                                                                                        ", Bank=" + players[index].Bank[slot].Value +
                                                                                        ", Backpack=" + players[index].Backpack[newSlot].Value +
                                                                                        ", Index=" + index +
                                                                                        ", NewSlot=" + newSlot, "MoveItem");
                            }
                            else
                            {
                                players[index].Backpack[newSlot] = players[index].Bank[slot];
                                players[index].Bank[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, false);
                            }
                            HandleData.SendPlayerBank(index);
                            HandleData.SendPlayerInv(index);
                        }
                        else
                        {
                            HandleData.SendServerMessageTo(Connection, "You inventory is full!");
                        }
                    }
                }
                else
                {
                    int newSlot = FindOpenInvSlot(players[index].Backpack);

                    if (newSlot < MAX_INV_SLOTS)
                    {
                        if (players[index].Bank[slot].Value > value)
                        {
                            Logging.WriteMessageLog("Before Values:WSlot3=" + slot + ",Value=" + value +
                                                                                        ", Bank=" + players[index].Bank[slot].Value +
                                                                                        ", Backpack=" + players[index].Backpack[newSlot].Value +
                                                                                        ", Index=" + index +
                                                                                        ", NewSlot=" + newSlot, "MoveItem");

                            players[index].Bank[slot].Value -= value;
                            players[index].Backpack[newSlot] = players[index].Bank[slot];
                            players[index].Backpack[newSlot].Value = value;

                            Logging.WriteMessageLog("After Values:WSlot3=" + slot + ",Value=" + value +
                                                                                    ", Bank=" + players[index].Bank[slot].Value +
                                                                                    ", Backpack=" + players[index].Backpack[newSlot].Value +
                                                                                    ", Index=" + index +
                                                                                    ", NewSlot=" + newSlot, "MoveItem");
                        }
                        else
                        {
                            players[index].Backpack[newSlot] = players[index].Bank[slot];
                            players[index].Bank[slot] = new Item("None", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, false);
                        }
                        HandleData.SendPlayerBank(index);
                        HandleData.SendPlayerInv(index);
                    }
                    else
                    {
                        HandleData.SendServerMessageTo(Connection, "You inventory is full!");
                    }
                }
            }
        }